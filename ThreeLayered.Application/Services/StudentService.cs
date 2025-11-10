namespace ThreeLayered.Application.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces;
    using Models;

    public class StudentService(IStudentRepository studentRepository, IAttendanceRepository attendanceRepository)
        : IStudentService
    {

        public async Task<Student?> GetStudent(Guid studentId)
        {
            var student = await studentRepository.GetById(studentId);
            return student;
        }

        public async Task<IReadOnlyList<Student>> GetStudentsForCourse(Guid courseId)
        {
            var students = await studentRepository.GetByCourse(courseId);
            return students;
        }

        public async Task<CourseAttendanceStats> CalculateAttendanceRates(Guid courseId)
        {
            var students = await studentRepository.GetByCourse(courseId);
            var sessions = await attendanceRepository.GetByCourse(courseId);

            var stats = new CourseAttendanceStats
            {
                CourseId = courseId,
                TotalSessions = sessions.Count
            };

            foreach (var student in students)
            {
                var present = sessions.Sum(session => session.Records.Count(r => r.StudentId == student.Id && r.Status == AttendanceStatus.Present));
                var late = sessions.Sum(session => session.Records.Count(r => r.StudentId == student.Id && r.Status == AttendanceStatus.Late));
                var attended = present + late;
                var rate = sessions.Count == 0 ? 0 : Math.Round(attended / (double)sessions.Count, 2);

                stats.Students.Add(new StudentAttendanceStats
                {
                    StudentId = student.Id,
                    StudentName = student.Name,
                    PresentCount = present,
                    LateCount = late,
                    TotalSessions = sessions.Count,
                    AttendanceRate = rate
                });
            }

            return stats;
        }
    }
}

