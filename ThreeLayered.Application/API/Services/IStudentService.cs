namespace ThreeLayered.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ThreeLayered.Application.Models;

    public interface IStudentService
    {
        Task<Student?> GetStudent(int studentId);
        Task<IReadOnlyList<Student>> GetStudentsForCourse(int courseId);
        Task<CourseAttendanceStats> CalculateAttendanceRates(int courseId);
    }
}

