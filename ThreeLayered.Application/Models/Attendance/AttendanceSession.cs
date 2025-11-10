namespace ThreeLayered.Application.Models
{
    using System;
    using System.Collections.Generic;

    public class AttendanceSession
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid CourseId { get; set; }
        public string Code { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public List<AttendanceEntry> Records { get; set; } = new List<AttendanceEntry>();
    }
}

