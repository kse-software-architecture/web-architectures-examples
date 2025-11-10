using System;

namespace WebArchitecturesExamples.ThreeLayered.Models
{
    public class MarkAttendanceRequest
    {
        public Guid SessionId { get; set; }
        public Guid StudentId { get; set; }
    }
}

