namespace ThreeLayered.Application.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ThreeLayered.Application.Models;

    public interface IAttendanceService
    {
        Task<AttendanceSession> StartSessionAsync(Guid courseId, int durationMinutes);
        Task<AttendanceEntry> MarkAttendanceAsync(Guid sessionId, Guid studentId);
        Task<SessionSummary> GetSummaryAsync(Guid sessionId);
        Task<IReadOnlyList<AttendanceSession>> GetSessionsByCourse(Guid courseId);
        Task NotifyStudents(Guid courseId, IEnumerable<Student> students, AttendanceSession session);
    }
}

