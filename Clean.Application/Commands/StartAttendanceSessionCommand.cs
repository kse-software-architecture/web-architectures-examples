namespace WebArchitecturesExamples.Clean.Application.Commands;

using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using Interfaces;
using MediatR;
using Utils;

public class StartAttendanceSessionCommand
    : IRequest<StartAttendanceSessionCommand.Response>
{
    public int CourseId { get; init; } 

    public int DurationMinutes { get; init; } 
    
    public enum Error
    {
        InvalidDuration
    }

    public record Result(int SessionId, string Code, DateTime StartTime, DateTime EndTime, int NotifiedStudentCount);

    public class Response : ResultResponse<Result, Error, Response>
    {
    }
}

public class StartAttendanceSessionCommandHandler : IRequestHandler<StartAttendanceSessionCommand, StartAttendanceSessionCommand.Response>
{
    private static readonly char[] CodeCharacters = "ABCDEFGHJKLMNPQRSTUVWXYZ23456789".ToCharArray();
    private readonly IAttendanceRepository attendanceRepository;
    private readonly IStudentRepository studentRepository;
    private readonly IStudentNotifier studentNotifier;
    private readonly Random random = new();

    public StartAttendanceSessionCommandHandler(
        IAttendanceRepository attendanceRepository,
        IStudentRepository studentRepository,
        IStudentNotifier studentNotifier)
    {
        this.attendanceRepository = attendanceRepository;
        this.studentRepository = studentRepository;
        this.studentNotifier = studentNotifier;
    }

    public async Task<StartAttendanceSessionCommand.Response> Handle(StartAttendanceSessionCommand request,
        CancellationToken cancellationToken)
    {
        if (request.DurationMinutes <= 0)
        {
            return StartAttendanceSessionCommand.Response.Error(StartAttendanceSessionCommand.Error.InvalidDuration);
        }

        var now = DateTime.UtcNow;
        var session = new AttendanceSession
        {
            CourseId = request.CourseId,
            Code = GenerateCode(),
            StartTime = now,
            EndTime = now.AddMinutes(request.DurationMinutes)
        };

        await attendanceRepository.Add(session);

        var students = await studentRepository.GetByCourse(request.CourseId);
        await studentNotifier.NotifyAsync(request.CourseId, students, session);

        var result = new StartAttendanceSessionCommand.Result(
            session.Id,
            session.Code,
            session.StartTime,
            session.EndTime,
            students.Count);

        return StartAttendanceSessionCommand.Response.Ok(result);
    }

    private string GenerateCode()
    {
        var buffer = new char[6];
        for (var i = 0; i < buffer.Length; i++)
        {
            var index = random.Next(CodeCharacters.Length);
            buffer[i] = CodeCharacters[index];
        }

        return new string(buffer);
    }
}
