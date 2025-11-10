namespace WebArchitecturesExamples.Clean.Presentation.Infrastructure;

using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using WebArchitecturesExamples.Clean.Application.Interfaces;

public class InMemoryAttendanceRepository : IAttendanceRepository
{
    private readonly ConcurrentDictionary<int, AttendanceSession> sessions = new();
    private int nextSessionId = 0;

    public Task Add(AttendanceSession session)
    {
        var id = Interlocked.Increment(ref nextSessionId);
        session.Id = id;
        sessions[id] = session;
        return Task.CompletedTask;
    }

    public Task<AttendanceSession?> GetById(int sessionId)
    {
        sessions.TryGetValue(sessionId, out var session);
        return Task.FromResult(session);
    }

    public Task<List<AttendanceSession>> GetByCourse(int courseId)
    {
        var result = sessions.Values
            .Where(s => s.CourseId == courseId)
            .OrderByDescending(s => s.StartTime)
            .ToList();
        return Task.FromResult(result);
    }

    public Task Save()
    {
        return Task.CompletedTask;
    }
}

