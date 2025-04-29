using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ObsBackend.Model
{
public class ExamAnnouncement
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    
    [ForeignKey("ResitExam")] // ResitExam tablosu ile ilişkili
    [Column("examId")]
    public int ExamId { get; set; }

    [Column("message")]
    public string? Message { get; set; }

    public ResitExam ResitExam { get; set; } = null!;
}
}