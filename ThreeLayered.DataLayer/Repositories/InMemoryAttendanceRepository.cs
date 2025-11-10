namespace ThreeLayered.DataLayer.Repositories;

using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ThreeLayered.Application.Interfaces;
using ThreeLayered.Application.Models;

public class InMemoryAttendanceRepository : IAttendanceRepository
{
    private readonly ConcurrentDictionary<int, AttendanceSession> sessions = new();
    private int nextSessionId;
    private int nextRecordId;

    public Task Add(AttendanceSession session)
    {
        var id = Interlocked.Increment(ref nextSessionId);
        session.Id = id;
        EnsureRecordIds(session);
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
            .Where(session => session.CourseId == courseId)
            .OrderByDescending(session => session.StartTime)
            .ToList();

        return Task.FromResult(result);
    }

    public Task Save()
    {
        foreach (var session in sessions.Values)
        {
            EnsureRecordIds(session);
        }

        return Task.CompletedTask;
    }

    private void EnsureRecordIds(AttendanceSession session)
    {
        foreach (var record in session.Records.Where(record => record.Id == 0))
        {
            var recordId = Interlocked.Increment(ref nextRecordId);
            record.Id = recordId;
        }
    }
}

