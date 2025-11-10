namespace ThreeLayered.Application.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ThreeLayered.Application.Models;

    public interface IStudentRepository
    {
        Task<Student?> GetById(int studentId);
        Task<List<Student>> GetByCourse(int courseId);
    }
}

