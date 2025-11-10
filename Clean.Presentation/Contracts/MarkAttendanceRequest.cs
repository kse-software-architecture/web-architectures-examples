using System;

namespace WebArchitecturesExamples.ThreeLayered.Models
{
    public class MarkAttendanceRequest
    {
        public int SessionId { get; set; }
        public int StudentId { get; set; }
    }
}

