namespace ThreeLayered.DataLayer.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using ThreeLayered.Application.Interfaces;
    using ThreeLayered.Application.Models;
    using ThreeLayered.DataLayer.Data;

    public class AttendanceRepository(AppDbContext context) : IAttendanceRepository
    {

        public async Task Add(AttendanceSession session)
        {
            await context.AttendanceSessions.AddAsync(session);
        }

        public async Task<AttendanceSession?> GetById(int sessionId)
        {
            var session = await context.AttendanceSessions
                .Include(s => s.Records)
                .FirstOrDefaultAsync(s => s.Id == sessionId);

            return session;
        }

        public async Task<List<AttendanceSession>> GetByCourse(int courseId)
        {
            var sessions = await context.AttendanceSessions
                .Include(s => s.Records)
                .Where(s => s.CourseId == courseId)
                .OrderByDescending(s => s.StartTime)
                .ToListAsync();

            return sessions;
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }
    }
}

