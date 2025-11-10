using System;

namespace WebArchitecturesExamples.ThreeLayered.Models
{
    public class MarkAttendanceResponse
    {
        public int SessionId { get; set; }
        public int StudentId { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
    }
}

