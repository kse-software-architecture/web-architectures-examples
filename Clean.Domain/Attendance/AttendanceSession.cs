namespace WebArchitecturesExamples.Clean.Domain
{
    using System;
    using System.Collections.Generic;

    public class AttendanceSession
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Code { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public List<AttendanceEntry> Records { get; set; } = new List<AttendanceEntry>();
    }
}

