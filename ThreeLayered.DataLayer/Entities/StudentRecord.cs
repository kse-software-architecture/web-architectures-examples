namespace ThreeLayered.DataLayer.Entities
{
    using System;

    public class StudentRecord
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int CourseId { get; set; }
    }
}

