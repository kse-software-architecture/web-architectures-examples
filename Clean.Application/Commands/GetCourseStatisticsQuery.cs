namespace WebArchitecturesExamples.Clean.Application.Commands;

using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Interfaces;
using Utils;

public class GetCourseStatisticsQuery
    : IRequest<GetCourseStatisticsQuery.Response>
{
    public int CourseId { get; init; }

    public enum Error
    {
        CourseNotFound
    }

    public class Response : ResultResponse<CourseAttendanceStats, Error, Response>
    {
    }

}

public class GetCourseStatisticsQueryHandler(
    IAttendanceRepository attendanceRepository,
    IStudentRepository studentRepository)
    : IRequestHandler<GetCourseStatisticsQuery, GetCourseStatisticsQuery.Response>
{

    public async Task<GetCourseStatisticsQuery.Response> Handle(GetCourseStatisticsQuery request, CancellationToken cancellationToken)
    {
        var students = await studentRepository.GetByCourse(request.CourseId);
        if (students.Count == 0)
        {
            return GetCourseStatisticsQuery.Response.Error(GetCourseStatisticsQuery.Error.CourseNotFound);
        }

        var sessions = await attendanceRepository.GetByCourse(request.CourseId);

        var stats = new CourseAttendanceStats
        {
            CourseId = request.CourseId,
            TotalSessions = sessions.Count
        };

        foreach (var student in students)
        {
            var present = sessions.Sum(s => s.Records.Count(r => r.StudentId == student.Id && r.Status == AttendanceStatus.Present));
            var late = sessions.Sum(s => s.Records.Count(r => r.StudentId == student.Id && r.Status == AttendanceStatus.Late));
            var totalAttended = present + late;
            var rate = sessions.Count == 0 ? 0 : Math.Round(totalAttended / (double)sessions.Count, 2);

            stats.Students.Add(new StudentAttendanceStats
            {
                StudentId = student.Id,
                StudentName = student.Name,
                PresentCount = present,
                LateCount = late,
                TotalSessions = sessions.Count,
                AttendanceRate = rate
            });
        }

        return GetCourseStatisticsQuery.Response.Ok(stats);
    }
}

