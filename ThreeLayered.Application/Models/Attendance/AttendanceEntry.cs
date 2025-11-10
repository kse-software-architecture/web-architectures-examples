namespace ThreeLayered.Application.Models
{
    using System;

    public class AttendanceEntry
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid StudentId { get; set; }
        public DateTime Timestamp { get; set; }
        public AttendanceStatus Status { get; set; }
    }
}

