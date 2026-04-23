namespace SchoolGradebook.Models;

public class Attendance : BaseEntity 
{
    public DateTime Date { get; set; } = DateTime.Now;
    public bool IsExcused { get; set; } // true = извинено, false = неизвинено
    
    public int StudentId { get; set; }
    public Student? Student { get; set; }
    
    public int SubjectId { get; set; }
    public Subject? Subject { get; set; }
}