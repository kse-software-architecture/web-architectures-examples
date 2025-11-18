namespace WebArchitecturesExamples.Clean.Application.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Domain;

    public interface IAttendanceRepository
    {
        Task Add(AttendanceSession session);
        Task<AttendanceSession?> GetById(int sessionId);
        Task<List<AttendanceSession>> GetByCourse(int courseId);
        Task Update(AttendanceSession session);
    }
}

