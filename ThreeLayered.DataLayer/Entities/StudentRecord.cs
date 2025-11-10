namespace ThreeLayered.DataLayer.Entities
{
    using System;

    public class StudentRecord
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid CourseId { get; set; }
    }
}

