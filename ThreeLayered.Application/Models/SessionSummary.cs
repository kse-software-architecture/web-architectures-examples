namespace ThreeLayered.Application.Models
{
    using System;

    public class SessionSummary
    {
        public Guid SessionId { get; set; }
        public int PresentCount { get; set; }
        public int LateCount { get; set; }
        public int Total => PresentCount + LateCount;
    }
}

