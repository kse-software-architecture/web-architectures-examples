namespace WebArchitecturesExamples.Clean.Application.Commands;

using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using Interfaces;
using MediatR;
using Utils;

public record MarkAttendanceCommand
    : IRequest<MarkAttendanceCommand.Response>
{
    
    public int SessionId { get; init; }

    public int StudentId { get; init; } 
    
    
    public record Result(int SessionId, int StudentId, AttendanceStatus Status, DateTime Timestamp);

    public class Response : ResultResponse<Result, AttendanceError, Response>
    {
    }
}

public enum AttendanceError
{
    SessionNotFound,
    SessionClosed
}

public class MarkAttendanceCommandHandler(IAttendanceRepository attendanceRepository)
    : IRequestHandler<MarkAttendanceCommand, MarkAttendanceCommand.Response>
{

    public async Task<MarkAttendanceCommand.Response> Handle(MarkAttendanceCommand request,
        CancellationToken cancellationToken)
    {
        var session = await attendanceRepository.GetById(request.SessionId);
        if (session == null)
        {
            return MarkAttendanceCommand.Response.Error(AttendanceError.SessionNotFound);
        }

        var now = DateTime.UtcNow;
        if (now > session.EndTime)
        {
            return MarkAttendanceCommand.Response.Error(AttendanceError.SessionClosed);
        }

        var existing = session.Records.FirstOrDefault(r => r.StudentId == request.StudentId);
        var status = now <= session.StartTime.AddMinutes(5)
            ? AttendanceStatus.Present
            : AttendanceStatus.Late;

        if (existing != null)
        {
            existing.Timestamp = now;
            existing.Status = status;
        }
        else
        {
            var record = new AttendanceEntry
            {
                Id = session.Records.Count == 0 ? 1 : session.Records.Max(r => r.Id) + 1,
                StudentId = request.StudentId,
                Timestamp = now,
                Status = status
            };
            session.Records.Add(record);
            existing = record;
        }

        await attendanceRepository.Save();

        return MarkAttendanceCommand.Response.Ok(new MarkAttendanceCommand.Result(session.Id, request.StudentId,
            existing.Status, existing.Timestamp));
    }
}