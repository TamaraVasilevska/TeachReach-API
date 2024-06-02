using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TeachReach.TeachReach.Domain.Entities;

namespace TeachReach.TeachReach.Infrastructure
{
    public class TeachReachDbContext : DbContext
    {
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Student> Students { get; set; }

        public TeachReachDbContext(DbContextOptions<TeachReachDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //// Configure relationships and other entity settings
            //modelBuilder.Entity<Teacher>()
            //    .HasMany(t => t.Subjects)
            //    .WithMany(s => s.Teachers);

            //modelBuilder.Entity<Review>()
            //    .HasOne(r => r.Student)
            //    .WithMany()
            //    .HasForeignKey(r => r.StudentId)
            //    .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
