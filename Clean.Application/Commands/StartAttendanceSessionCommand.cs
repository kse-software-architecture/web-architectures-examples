namespace WebArchitecturesExamples.Clean.Commands;

using MediatR;

public class StartAttendanceSessionCommand : IRequest<StartAttendanceSessionCommand.Response>
{
    public class Response
    {

    }
}

public class
    StartAttendanceSessionCommandHandler : IRequestHandler<StartAttendanceSessionCommand,
    StartAttendanceSessionCommand.Response>
{
    public async Task<StartAttendanceSessionCommand.Response> Handle(StartAttendanceSessionCommand request,
        CancellationToken cancellationToken)
    {
        
        return new StartAttendanceSessionCommand.Response();
    }
}
