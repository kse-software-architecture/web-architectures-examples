namespace WebArchitecturesExamples.Clean.Domain
{
    using System;

    public class SessionSummary
    {
        public int SessionId { get; set; }
        public int PresentCount { get; set; }
        public int LateCount { get; set; }
        public int Total => PresentCount + LateCount;
    }
}

