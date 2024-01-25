using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Duanbaimot_lan2.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Các DbSet cho các bảng
        public DbSet<Student> Students { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<TeachingSchedule> TeachingSchedules { get; set; }
        public DbSet<AcademicYear> AcademicYears { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<DepartmentsSubjects> DepartmentsSubjects { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<GradeType> GradeTypes { get; set; }
        public DbSet<Holiday> Holidays { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Các cấu hình bổ sung nếu cần
            // ...

            // Cấu hình mối quan hệ giữa các bảng
            modelBuilder.Entity<Enrollment>()
        .HasKey(e => e.EnrollmentID);
            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Student)
                .WithMany(s => s.Enrollments)
                .HasForeignKey(e => e.StudentID)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Class)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.ClassID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<TeachingSchedule>()
                .HasKey(ts => ts.ScheduleID);
            modelBuilder.Entity<TeachingSchedule>()
                .HasOne(ts => ts.Instructor)
                .WithMany(i => i.TeachingSchedules)
                .HasForeignKey(ts => ts.InstructorID)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<TeachingSchedule>()
                .HasOne(ts => ts.Class)
                .WithMany(c => c.TeachingSchedules)
                .HasForeignKey(ts => ts.ClassID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Grade>()
                .HasKey(g => g.GradeID);
            modelBuilder.Entity<Grade>()
                .HasOne(g => g.Student)
                .WithMany(s => s.Grades)
                .HasForeignKey(g => g.StudentID)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Grade>()
                .HasOne(g => g.Class)
                .WithMany(c => c.Grades)
                .HasForeignKey(g => g.ClassID)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Grade>()
                .HasOne(g => g.Subject)
                .WithMany(sub => sub.Grades)
                .HasForeignKey(g => g.SubjectID)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Grade>()
                .HasOne(g => g.GradeType)
                .WithMany(gt => gt.Grades)
                .HasForeignKey(g => g.GradeTypeID)
                .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<DepartmentsSubjects>()
    .HasKey(ds => ds.DepartmentSubjectID);
            modelBuilder.Entity<DepartmentsSubjects>()
                .HasOne(ds => ds.Subject)
                .WithMany(sub => sub.DepartmentsSubjects)
                .HasForeignKey(ds => ds.SubjectID)
                .OnDelete(DeleteBehavior.NoAction);



            // Cấu hình mối quan hệ giữa ApplicationUser và các bảng khác nếu cần
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.Students)
                .WithOne(s => s.ApplicationUser)
                .HasForeignKey(s => s.ApplicationUserId)
                .OnDelete(DeleteBehavior.Cascade); // Hoặc NoAction tùy thuộc vào yêu cầu


            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.Classes)
                .WithOne(c => c.ApplicationUser)
                .HasForeignKey(c => c.ApplicationUserId);

            // Các cấu hình mối quan hệ khác...
        }
    }
}
