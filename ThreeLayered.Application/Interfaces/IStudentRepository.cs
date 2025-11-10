namespace ThreeLayered.Application.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ThreeLayered.Application.Models;

    public interface IStudentRepository
    {
        Task<Student?> GetById(Guid studentId);
        Task<List<Student>> GetByCourse(Guid courseId);
    }
}

