namespace WebArchitecturesExamples.Clean.Application.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Domain;

    public interface IStudentRepository
    {
        Task<Student?> GetById(int studentId);
        Task<List<Student>> GetByCourse(int courseId);
    }
}

