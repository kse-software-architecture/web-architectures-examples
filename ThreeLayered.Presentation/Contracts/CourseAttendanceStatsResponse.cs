namespace WebArchitecturesExamples.ThreeLayered.Models
{
    public class CourseAttendanceStatsResponse
    {
        public Guid CourseId { get; set; }
        public int TotalSessions { get; set; }
        public List<CourseAttendanceStudentResponse> Students { get; set; } = new List<CourseAttendanceStudentResponse>();
    }

    public class CourseAttendanceStudentResponse
    {
        public Guid StudentId { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public int PresentCount { get; set; }
        public int LateCount { get; set; }
        public int TotalSessions { get; set; }
        public double AttendanceRate { get; set; }
    }
}

