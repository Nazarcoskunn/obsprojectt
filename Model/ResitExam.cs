using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ObsBackend.Model
{
public class ResitExam
{
    public int Id { get; set; }
    
    [Column("courseCode")]
    public string CourseCode { get; set; }

    [ForeignKey("CourseCode")]
    public Course Course { get; set; }

    // Bu alana dikkat edin, burada LecturerId olmalı
    [Column("lecturerId")]
    public int LecturerId { get; set; }

    [ForeignKey("LecturerId")]
    public Instructor Instructor { get; set; }
}

}
