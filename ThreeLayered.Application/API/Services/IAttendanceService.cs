namespace ThreeLayered.Application.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ThreeLayered.Application.Models;

    public interface IAttendanceService
    {
        Task<AttendanceSession> StartSessionAsync(int courseId, int durationMinutes);
        Task<AttendanceEntry> MarkAttendanceAsync(int sessionId, int studentId);
        Task<SessionSummary> GetSummaryAsync(int sessionId);
        Task<IReadOnlyList<AttendanceSession>> GetSessionsByCourse(int courseId);
        Task NotifyStudents(int courseId, IEnumerable<Student> students, AttendanceSession session);
    }
}

