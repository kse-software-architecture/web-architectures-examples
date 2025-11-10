namespace ThreeLayered.DataLayer.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using ThreeLayered.Application.Interfaces;
    using ThreeLayered.Application.Models;
    using ThreeLayered.DataLayer.Data;
    using ThreeLayered.DataLayer.Entities;

    public class StudentRepository(AppDbContext context) : IStudentRepository
    {

        public async Task<Student?> GetById(Guid studentId)
        {
            var record = await context.Students.FirstOrDefaultAsync(s => s.Id == studentId);
            if (record == null)
            {
                return null;
            }

            var student = MapToModel(record);
            return student;
        }

        public async Task<List<Student>> GetByCourse(Guid courseId)
        {
            var records = await context.Students
                .Where(s => s.CourseId == courseId)
                .OrderBy(s => s.Name)
                .ToListAsync();

            var students = records.Select(MapToModel).ToList();
            return students;
        }

        // Special frameworks can do that, here we keep it simple
        private static Student MapToModel(StudentRecord record)
        {
            var student = new Student
            {
                Id = record.Id,
                Name = record.Name,
                CourseId = record.CourseId
            };
            return student;
        }
    }
}