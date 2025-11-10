namespace WebArchitecturesExamples.Clean.Models
{
    public class CourseAttendanceStatsResponse
    {
        public int CourseId { get; set; }
        public int TotalSessions { get; set; }
        public List<CourseAttendanceStudentResponse> Students { get; set; } = new List<CourseAttendanceStudentResponse>();
    }

    public class CourseAttendanceStudentResponse
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public int PresentCount { get; set; }
        public int LateCount { get; set; }
        public int TotalSessions { get; set; }
        public double AttendanceRate { get; set; }
    }
}

