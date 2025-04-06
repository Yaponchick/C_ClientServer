using C_ClientServer.sqlLite;
using Microsoft.EntityFrameworkCore;

public class ApplicationContext : DbContext
{
    public DbSet<Student> Students { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }

    public ApplicationContext() => Database.EnsureCreated();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=BD1.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Enrollment>()
            .HasKey(e => e.EnrollmentId);

        modelBuilder.Entity<Enrollment>()
            .HasOne(e => e.Student)
            .WithMany(s => s.Enrollments)
            .HasForeignKey(e => e.StudentId);

        modelBuilder.Entity<Enrollment>()
            .HasOne(e => e.Course)
            .WithMany(c => c.Enrollments)
            .HasForeignKey(e => e.CourseId);

        modelBuilder.Entity<Teacher>()
            .HasMany(t => t.Courses)
            .WithMany(c => c.Teachers)
            .UsingEntity(j => j.ToTable("TeacherCourses"));
    }

    public void SeedData()
    {
        if (!Students.Any())
        {
            var bodya = new Student { FirstName = "Бодя", LastName = "Петров", Age = 33 };
            var alice = new Student { FirstName = "Алиса", LastName = "Иванова", Age = 26 };

            var rus = new Course { CourseName = "Русский" };
            var physics = new Course { CourseName = "Физика" };

            var enrollment1 = new Enrollment { Student = bodya, Course = rus };
            var enrollment2 = new Enrollment { Student = alice, Course = physics };

            Students.AddRange(bodya, alice);
            Courses.AddRange(rus, physics);
            Enrollments.AddRange(enrollment1, enrollment2);

            SaveChanges();
        }
    }
}