namespace SchoolGradebook.Models;

public class Attendance : BaseEntity 
{
    public DateTime Date { get; set; } = DateTime.Now;
    public bool IsExcused { get; set; }
    
    public int StudentId { get; set; }
    public Student? Student { get; set; }
    
    public int SubjectId { get; set; }
    public Subject? Subject { get; set; }
}