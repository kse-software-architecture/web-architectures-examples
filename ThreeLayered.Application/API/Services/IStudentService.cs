namespace ThreeLayered.Application.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface IStudentService
    {
        Task<Student?> GetStudent(Guid studentId);
        Task<IReadOnlyList<Student>> GetStudentsForCourse(Guid courseId);
        Task<CourseAttendanceStats> CalculateAttendanceRates(Guid courseId);
    }
}

