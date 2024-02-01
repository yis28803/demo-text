using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Duanbaimot_lan2.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Class> Classes { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Fee> Fees { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Student> Students { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configure relationships

            // Mối quan hệ giữa Student và ApplicationUser
            builder.Entity<Student>()
                .HasOne(s => s.User)
                .WithMany() // Không có thuộc tính ngược lại ở ApplicationUser
                .HasForeignKey(s => s.UserId);

            // Mối quan hệ giữa AspNetUsers và Student
            builder.Entity<Student>()
                .HasOne(s => s.User)
                .WithOne()
                .HasForeignKey<Student>(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade);



            // Mối quan hệ giữa Enrollment và Student
            builder.Entity<Enrollment>()
                .HasOne(e => e.Student)
                .WithMany()
                .HasForeignKey(e => e.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            // Mối quan hệ giữa Enrollment và Class
            builder.Entity<Enrollment>()
                .HasOne(e => e.Class)
                .WithMany()
                .HasForeignKey(e => e.ClassId);

            // Mối quan hệ giữa Fee và Student
            builder.Entity<Fee>()
                .HasOne(f => f.Student)
               .WithMany()
                .HasForeignKey(f => f.StudentId)
                 .OnDelete(DeleteBehavior.Cascade);

            // Mối quan hệ giữa Fee và Class
            builder.Entity<Fee>()
                .HasOne(f => f.Class)
                .WithMany()
                .HasForeignKey(f => f.ClassId);

            // Mối quan hệ giữa Schedule và Class
            builder.Entity<Schedule>()
                .HasOne(s => s.Class)
                .WithMany()
                .HasForeignKey(s => s.ClassId);

            // Mối quan hệ giữa Schedule và Student
            builder.Entity<Schedule>()
                .HasOne(s => s.Student)
                .WithMany()
                .HasForeignKey(s => s.StudentId)
                .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
