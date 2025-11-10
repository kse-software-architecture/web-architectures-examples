namespace WebArchitecturesExamples.Clean.Presentation.Infrastructure;

using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebArchitecturesExamples.Clean.Application.Interfaces;
using WebArchitecturesExamples.Clean.Domain;

public class InMemoryStudentRepository : IStudentRepository
{
    private readonly ConcurrentDictionary<int, Student> students = new();

    public InMemoryStudentRepository()
    {
        Seed();
    }

    public Task<Student?> GetById(int studentId)
    {
        students.TryGetValue(studentId, out var student);
        return Task.FromResult(student);
    }

    public Task<List<Student>> GetByCourse(int courseId)
    {
        var result = students.Values
            .Where(student => student.CourseId == courseId)
            .OrderBy(student => student.Name)
            .ToList();

        return Task.FromResult(result);
    }

    private void Seed()
    {
        AddStudent(new Student { Id = 1, Name = "Alice Johnson", CourseId = 1001 });
        AddStudent(new Student { Id = 2, Name = "Brian Smith", CourseId = 1001 });
        AddStudent(new Student { Id = 3, Name = "Carla Ruiz", CourseId = 1001 });
        AddStudent(new Student { Id = 4, Name = "Diego Martinez", CourseId = 1002 });
        AddStudent(new Student { Id = 5, Name = "Ella Brown", CourseId = 1002 });
    }

    private void AddStudent(Student student)
    {
        students[student.Id] = student;
    }
}

