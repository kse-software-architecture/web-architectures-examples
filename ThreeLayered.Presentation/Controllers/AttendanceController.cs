
using Microsoft.AspNetCore.Mvc;
using ThreeLayered.Application.Interfaces;
using ThreeLayered.Application.Models;

namespace WebArchitecturesExamples.ThreeLayered.Controllers
{
    using Models;

    [ApiController]
    [Route("api/attendance")]
    public class AttendanceController(IAttendanceService attendanceService, IStudentService studentService)
        : ControllerBase
    {

        [HttpPost("start")]
        public async Task<ActionResult<StartSessionResponse>> StartSession([FromBody] StartSessionRequest request)
        {
            if (request.DurationMinutes <= 0)
            {
                return BadRequest("Duration must be positive.");
            }

            var session = await attendanceService.StartSessionAsync(request.CourseId, request.DurationMinutes);

            var students = await studentService.GetStudentsForCourse(request.CourseId);
            var participantCount = students.Count;
            Response.Headers.Append("X-Estimated-Participants", participantCount.ToString());

            // It is easy to leak business logic into controllers, here is small example
            var shouldNotify = participantCount > 0 && session.EndTime.Subtract(session.StartTime) > TimeSpan.FromMinutes(10);
            if (shouldNotify)
            {
                await attendanceService.NotifyStudents(request.CourseId, students, session);
            }

            var response = new StartSessionResponse
            {
                SessionId = session.Id,
                Code = session.Code,
                StartTime = session.StartTime,
                EndTime = session.EndTime
            };

            return CreatedAtAction(nameof(GetSummary), new { sessionId = session.Id }, response);
        }

        [HttpPost("mark")]
        public async Task<ActionResult<MarkAttendanceResponse>> MarkAttendance([FromBody] MarkAttendanceRequest request)
        {
            try
            {
                var record = await attendanceService.MarkAttendanceAsync(request.SessionId, request.StudentId);
                var response = new MarkAttendanceResponse
                {
                    SessionId = request.SessionId,
                    StudentId = request.StudentId,
                    Status = record.Status.ToString(),
                    Timestamp = record.Timestamp
                };

                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{sessionId:guid}/summary")]
        public async Task<ActionResult<SessionSummary>> GetSummary(Guid sessionId)
        {
            try
            {
                var summary = await attendanceService.GetSummaryAsync(sessionId);
                return Ok(summary);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{courseId:guid}/stats")]
        public async Task<ActionResult<CourseAttendanceStatsResponse>> GetCourseStats(Guid courseId)
        {
            var stats = await studentService.CalculateAttendanceRates(courseId);

            var response = new CourseAttendanceStatsResponse
            {
                CourseId = stats.CourseId,
                TotalSessions = stats.TotalSessions,
                Students = stats.Students
                    .Select(s => new CourseAttendanceStudentResponse
                    {
                        StudentId = s.StudentId,
                        StudentName = s.StudentName,
                        PresentCount = s.PresentCount,
                        LateCount = s.LateCount,
                        TotalSessions = s.TotalSessions,
                        AttendanceRate = s.AttendanceRate
                    })
                    .ToList()
            };

            return Ok(response);
        }
    }
}

