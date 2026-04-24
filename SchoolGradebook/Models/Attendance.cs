namespace SchoolGradebook.Models;

// Represents an attendance record for a student in a specific subject
public class Attendance : BaseEntity 
{
    // The date the attendance was recorded
    public DateTime Date { get; set; } = DateTime.Now;
    
    // Status indicating if the absence is excused
    public bool IsExcused { get; set; }
    
    // Foreign key and navigation property for the related student
    public int StudentId { get; set; }
    public Student? Student { get; set; }
    
    // Foreign key and navigation property for the related subject
    public int SubjectId { get; set; }
    public Subject? Subject { get; set; }
}