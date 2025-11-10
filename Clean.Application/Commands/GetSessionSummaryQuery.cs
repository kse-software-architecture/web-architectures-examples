namespace WebArchitecturesExamples.Clean.Application.Commands;

using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Interfaces;
using Domain;
using Utils;

public record GetSessionSummaryQuery(int SessionId)
    : IRequest<GetSessionSummaryQuery.Response>
{
    public enum Error
    {
        SessionNotFound
    }

    public class Response : ResultResponse<SessionSummary, Error, Response>
    {
    }
}

public class GetSessionSummaryQueryHandler(IAttendanceRepository attendanceRepository)
    : IRequestHandler<GetSessionSummaryQuery, GetSessionSummaryQuery.Response>
{

    public async Task<GetSessionSummaryQuery.Response> Handle(GetSessionSummaryQuery request, CancellationToken cancellationToken)
    {
        var session = await attendanceRepository.GetById(request.SessionId);
        if (session == null)
        {
            return GetSessionSummaryQuery.Response.Error(GetSessionSummaryQuery.Error.SessionNotFound);
        }

        var summary = new SessionSummary
        {
            SessionId = session.Id,
            PresentCount = session.Records.Count(r => r.Status == AttendanceStatus.Present),
            LateCount = session.Records.Count(r => r.Status == AttendanceStatus.Late)
        };

        return GetSessionSummaryQuery.Response.Ok(summary);
    }
}

