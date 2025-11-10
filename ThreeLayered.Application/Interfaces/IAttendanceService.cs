namespace ThreeLayered.Application.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ThreeLayered.Application.Models;

    public interface IAttendanceService
    {
        Task<AttendanceSession> StartSessionAsync(Guid courseId, int durationMinutes);
        Task<AttendanceRecord> MarkAttendanceAsync(Guid sessionId, Guid studentId);
        Task<SessionSummary> GetSummaryAsync(Guid sessionId);
        Task<IReadOnlyList<AttendanceSession>> GetSessionsByCourseAsync(Guid courseId);
        Task NotifyStudentsAsync(Guid courseId, IEnumerable<Student> students, AttendanceSession session);
    }
}

