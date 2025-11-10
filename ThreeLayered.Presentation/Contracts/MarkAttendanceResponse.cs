using System;

namespace WebArchitecturesExamples.ThreeLayered.Models
{
    public class MarkAttendanceResponse
    {
        public Guid SessionId { get; set; }
        public Guid StudentId { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
    }
}

