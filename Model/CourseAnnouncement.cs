public class CourseAnnouncement
{
    public int Id { get; set; }
    public string CourseCode { get; set; }
    public string InstructorName { get; set; }
    public string ExamMessage { get; set; }
    
    // UTC olmasına dikkat edelim
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;  // UTC olarak başlatabilirsiniz
}
