using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ObsBackend.Model
{
    public class Kurs
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("lectureName")]
        public string LectureName { get; set; }

        [Column("instructorId")]
        public int InstructorId { get; set; }

        [ForeignKey("InstructorId")]
        public Instructor Instructor { get; set; } = null!;
        
        public ICollection<ResitExam> ResitExams { get; set; } = new List<ResitExam>();
    }
}
