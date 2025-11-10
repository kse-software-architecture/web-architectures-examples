namespace WebArchitecturesExamples.Clean.Presentation.Infrastructure;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain;
using Microsoft.Extensions.Logging;

public class InMemoryStudentNotifier(ILogger<InMemoryStudentNotifier> logger) : IStudentNotifier
{

    public async Task NotifyAsync(int courseId, IReadOnlyList<Student> students, AttendanceSession session, CancellationToken cancellationToken)
    {
        if (students.Count == 0)
        {
            return;
        }

        foreach (var student in students)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await Task.Delay(10, cancellationToken);
            logger.LogInformation("Notified {Student} for course {Course} with code {Code}", student.Name, courseId, session.Code);
        }
    }
}

