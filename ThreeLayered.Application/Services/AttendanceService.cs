namespace ThreeLayered.Application.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;
    using Interfaces;
    using Models;

    public class AttendanceService(IAttendanceRepository attendanceRepository, ILogger<AttendanceService> logger)
        : IAttendanceService
    {
        private static readonly char[] CodeCharacters = "ABCDEFGHJKLMNPQRSTUVWXYZ23456789".ToCharArray();
        private readonly Random _random = new Random();

        public async Task<AttendanceSession> StartSessionAsync(int courseId, int durationMinutes)
        {
            var now = DateTime.UtcNow;
            var session = new AttendanceSession
            {
                CourseId = courseId,
                Code = GenerateCode(),
                StartTime = now,
                EndTime = now.AddMinutes(durationMinutes)
            };

            await attendanceRepository.Add(session);
            await attendanceRepository.Save();

            return session;
        }

        public async Task<AttendanceEntry> MarkAttendanceAsync(int sessionId, int studentId)
        {
            var session = await attendanceRepository.GetById(sessionId);
            if (session == null)
            {
                throw new InvalidOperationException("Session not found.");
            }

            var now = DateTime.UtcNow;
            if (now > session.EndTime)
            {
                throw new InvalidOperationException("Session has already ended.");
            }

            var existing = session.Records.FirstOrDefault(r => r.StudentId == studentId);
            var status = now <= session.StartTime.AddMinutes(5)
                ? AttendanceStatus.Present
                : AttendanceStatus.Late;

            if (existing != null)
            {
                existing.Timestamp = now;
                existing.Status = status;
                await attendanceRepository.Save();
                return existing;
            }

            var record = new AttendanceEntry
            {
                StudentId = studentId,
                Timestamp = now,
                Status = status
            };

            session.Records.Add(record);
            await attendanceRepository.Save();

            return record;
        }

        public async Task<SessionSummary> GetSummaryAsync(int sessionId)
        {
            var session = await attendanceRepository.GetById(sessionId);
            if (session == null)
            {
                throw new InvalidOperationException("Session not found.");
            }

            var present = session.Records.Count(r => r.Status == AttendanceStatus.Present);
            var late = session.Records.Count(r => r.Status == AttendanceStatus.Late);

            return new SessionSummary
            {
                SessionId = sessionId,
                PresentCount = present,
                LateCount = late
            };
        }

        public async Task<IReadOnlyList<AttendanceSession>> GetSessionsByCourse(int courseId)
        {
            var sessions = await attendanceRepository.GetByCourse(courseId);
            return sessions;
        }

        public async Task NotifyStudents(int courseId, IEnumerable<Student> students, AttendanceSession session)
        {
            var notifications = new List<Task>();

            foreach (var student in students)
            {
                notifications.Add(SendNotificationAsync(courseId, student, session));
            }

            await Task.WhenAll(notifications);
        }

        private async Task SendNotificationAsync(int courseId, Student student, AttendanceSession session)
        {
            await Task.Delay(50);
            logger.LogInformation("Notified {Student} for course {Course} with code {Code}.", student.Name, courseId, session.Code);
        }

        private string GenerateCode()
        {
            var buffer = new char[6];
            for (var i = 0; i < buffer.Length; i++)
            {
                var index = _random.Next(CodeCharacters.Length);
                buffer[i] = CodeCharacters[index];
            }

            return new string(buffer);
        }
    }
}

