namespace WebArchitecturesExamples.Clean.Application.Interfaces;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;

public interface IStudentNotifier
{
    Task NotifyAsync(int courseId, IReadOnlyList<Student> students, AttendanceSession session);
}

