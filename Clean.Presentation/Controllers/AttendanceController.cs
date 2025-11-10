namespace WebArchitecturesExamples.Clean.Presentation.Controllers;

using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Application.Commands;

[ApiController, Route("api/clean/attendance")]
public class AttendanceController(IMediator mediator) : ControllerBase
{
    [HttpPost("start")]
    public async Task<ActionResult<StartSessionResponse>> StartSession([FromBody] StartSessionRequest request)
    {
        var response = await mediator.Send(new StartAttendanceSessionCommand
        {
            CourseId = request.CourseId,
            DurationMinutes = request.DurationMinutes
        });

        if (response.Result.IsError)
        {
            return Problem(
                title: "Unable to start session",
                detail: response.Result.GetError().ToString(),
                statusCode: StatusCodes.Status400BadRequest);
        }

        var value = response.Result.GetValue()!;

        return CreatedAtAction(nameof(GetSummary), new { sessionId = value.SessionId }, new StartSessionResponse
        {
            SessionId = value.SessionId,
            Code = value.Code,
            StartTime = value.StartTime,
            EndTime = value.EndTime,
        });
    }

    [HttpPost("mark")]
    public async Task<ActionResult<MarkAttendanceResponse>> MarkAttendance([FromBody] MarkAttendanceRequest request)
    {
        var response = await mediator.Send(new MarkAttendanceCommand
        {
            SessionId = request.SessionId,
            StudentId = request.StudentId
        });

        if (response.Result.IsError)
        {
            var error = response.Result.GetError();
            var status = error switch
            {
                AttendanceError.SessionNotFound => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status400BadRequest
            };

            return Problem(
                title: "Unable to mark attendance",
                detail: error.ToString(),
                statusCode: status);
        }

        var value = response.Result.GetValue()!;

        return Ok(new MarkAttendanceResponse
        {
            SessionId = value.SessionId,
            StudentId = value.StudentId,
            Status = value.Status.ToString(),
            Timestamp = value.Timestamp
        });
    }

    [HttpGet("{sessionId:int}/summary")]
    public async Task<ActionResult<SessionSummary>> GetSummary(int sessionId)
    {
        var response = await mediator.Send(new GetSessionSummaryQuery
        {
            SessionId = sessionId
        });

        if (response.Result.IsError)
        {
            return NotFound(response.Result.GetError().ToString());
        }

        return Ok(response.Result.GetValue());
    }

    [HttpGet("{courseId:int}/stats")]
    public async Task<ActionResult<CourseAttendanceStats>> GetCourseStatistics(int courseId)
    {
        var response = await mediator.Send(new GetCourseStatisticsQuery
        {
            CourseId = courseId
        });

        if (response.Result.IsError)
        {
            return NotFound(response.Result.GetError().ToString());
        }

        return Ok(response.Result.GetValue());
    }
}