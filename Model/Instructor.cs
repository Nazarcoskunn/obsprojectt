using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ObsBackend.Model
{
public class Instructor
{
    [Key]
    public int Id { get; set; }

    [Column("name")]
    public string Name { get; set; }

public ICollection<Kurs> Kurslar { get; set; } = new List<Kurs>();

    public ICollection<Course> Courses { get; set; } = new List<Course>();
}

}