using System;

namespace WebArchitecturesExamples.ThreeLayered.Models
{
    public class StartSessionResponse
    {
        public Guid SessionId { get; set; }
        public string Code { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}

