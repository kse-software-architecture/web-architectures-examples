namespace ThreeLayered.Application.Models
{
    using System;

    public class AttendanceEntry
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public DateTime Timestamp { get; set; }
        public AttendanceStatus Status { get; set; }
    }
}

