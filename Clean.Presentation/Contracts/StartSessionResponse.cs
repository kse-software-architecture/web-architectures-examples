using System;

namespace WebArchitecturesExamples.Clean.Models
{
    public class StartSessionResponse
    {
        public int SessionId { get; set; }
        public string Code { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}

