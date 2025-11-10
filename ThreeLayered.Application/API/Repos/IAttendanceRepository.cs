namespace ThreeLayered.Application.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ThreeLayered.Application.Models;

    public interface IAttendanceRepository
    {
        Task Add(AttendanceSession session);
        Task<AttendanceSession?> GetById(int sessionId);
        Task<List<AttendanceSession>> GetByCourse(int courseId);
        Task Save();
    }
}

