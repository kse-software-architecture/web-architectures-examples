namespace WebArchitecturesExamples.Clean.Domain
{
    using System;
    using System.Collections.Generic;

    public class CourseAttendanceStats
    {
        public int CourseId { get; set; }
        public int TotalSessions { get; set; }
        public List<StudentAttendanceStats> Students { get; set; } = new List<StudentAttendanceStats>();
    }

    public class StudentAttendanceStats
    {
        public StudentAttendanceStats(List<AttendanceSession> sessions, Student student)
        {
            StudentId = student.Id;
            StudentName = student.Name;
                
            PresentCount = sessions.Sum(s => s.Records.Count(r => r.StudentId == student.Id && r.Status == AttendanceStatus.Present));
            LateCount = sessions.Sum(s => s.Records.Count(r => r.StudentId == student.Id && r.Status == AttendanceStatus.Late));
            TotalSessions = PresentCount + LateCount;
            AttendanceRate = sessions.Count == 0 ? 0 : Math.Round(TotalSessions / (double)sessions.Count, 2);
        }

        public int StudentId { get;  }
        public string StudentName { get;  } 
        public int PresentCount { get; }
        public int LateCount { get;  }
        public int TotalSessions { get;  }
        public double AttendanceRate { get;  }
    }
}

