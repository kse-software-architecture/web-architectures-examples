using System;

namespace WebArchitecturesExamples.Clean.Models
{
    public class MarkAttendanceRequest
    {
        public int SessionId { get; set; }
        public int StudentId { get; set; }
    }
}

