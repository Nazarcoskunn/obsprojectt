
using Microsoft.EntityFrameworkCore;
using ObsBackend.Model;

namespace ObsBackend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Secretary> Secretaries { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<ResitExam> ResitExams { get; set; }
        public DbSet<ExamAnnouncement> ExamAnnouncements { get; set; }
        public DbSet<LetterGrade> LetterGrades { get; set; }

        public DbSet<UploadResitExam> UploadResitExams { get; set; }

        public DbSet<UploadGradeRequest> UploadGradeRequests { get; set; }
        public DbSet<UploadExamSchedule> UploadExamSchedules { get; set; }

        public DbSet<CourseAnnouncement> CourseAnnouncements { get; set; }

public DbSet<Kurs> Kurslar { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Tablo isimlerini belirleyin
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Instructor>().ToTable("Instructor");

            // İlişkiler
            modelBuilder.Entity<Course>()
                .HasOne(c => c.Instructor) // Bir Course bir Instructor'a sahip
                .WithMany(i => i.Courses)  // Bir Instructor'ın birden fazla Course'u olabilir
                .HasForeignKey(c => c.InstructorId);  // Course'un foreign key'i InstructorId

            modelBuilder.Entity<ResitExam>()
                .HasOne(r => r.Course)  // Bir ResitExam bir Course'a sahip
                .WithMany(c => c.ResitExams) // Bir Course'un birden fazla ResitExam'ı olabilir
                .HasForeignKey(r => r.CourseCode) // ResitExam'ın CourseCode foreign key'i
                .HasPrincipalKey(c => c.Code);  // Course'daki Code anahtarına bağlan

          modelBuilder.Entity<ResitExam>()
    .HasOne(r => r.Instructor)  // Bir ResitExam bir Instructor'a sahip
    .WithMany()  // Bir Instructor'ın birden fazla ResitExam'ı olabilir
    .HasForeignKey(r => r.LecturerId)  // ResitExam'ın LecturerId foreign key'i
    .HasPrincipalKey(i => i.Id); // Instructor'daki Id anahtarına bağlan


modelBuilder.Entity<Kurs>()
    .HasOne(k => k.Instructor)
    .WithMany(i => i.Kurslar)  // Instructor içinde de ICollection<Kurs> eklemen lazım
    .HasForeignKey(k => k.InstructorId);



            base.OnModelCreating(modelBuilder);
        }
    }
}
