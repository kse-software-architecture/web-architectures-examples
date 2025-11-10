using System;

namespace WebArchitecturesExamples.Clean.Models
{
    public class StartSessionRequest
    {
        public int CourseId { get; set; }
        public int DurationMinutes { get; set; }
    }
}

