namespace ThreeLayered.DataLayer.Data
{
    using Microsoft.EntityFrameworkCore;
    using ThreeLayered.Application.Models;
    using ThreeLayered.DataLayer.Entities;

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<AttendanceSession> AttendanceSessions => Set<AttendanceSession>();
        public DbSet<StudentRecord> Students => Set<StudentRecord>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AttendanceSession>(entity =>
            {
                entity.HasKey(s => s.Id);
                entity.Property(s => s.Code)
                    .IsRequired()
                    .HasMaxLength(6);

                entity.OwnsMany(s => s.Records, builder =>
                {
                    builder.WithOwner().HasForeignKey("AttendanceSessionId");
                    builder.Property(record => record.Id).ValueGeneratedNever();
                    builder.HasKey(record => record.Id);
                    builder.Property(record => record.Timestamp).IsRequired();
                    builder.Property(record => record.Status).IsRequired();
                    builder.Property(record => record.StudentId).IsRequired();
                });
            });

            modelBuilder.Entity<StudentRecord>(entity =>
            {
                entity.HasKey(s => s.Id);
                entity.Property(s => s.Name)
                    .IsRequired()
                    .HasMaxLength(200);
                entity.Property(s => s.CourseId).IsRequired();
            });
        }
    }
}

