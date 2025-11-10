namespace ThreeLayered.Application.Models
{
    using System;
    using System.Collections.Generic;

    public class CourseAttendanceStats
    {
        public int CourseId { get; set; }
        public int TotalSessions { get; set; }
        public List<StudentAttendanceStats> Students { get; set; } = new List<StudentAttendanceStats>();
    }

    public class StudentAttendanceStats
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public int PresentCount { get; set; }
        public int LateCount { get; set; }
        public int TotalSessions { get; set; }
        public double AttendanceRate { get; set; }
    }
}

