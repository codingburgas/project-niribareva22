namespace SchoolGradebook.DTOs;

// Data Transfer Object for carrying student summary information to the UI
public class StudentDTO
{
    // Unique identifier
    public int Id { get; set; }
    
    // Student's full name
    public string Name { get; set; } = string.Empty;
    
    // Class identifier
    public string ClassName { get; set; } = string.Empty;
    
    // Pre-calculated average grade
    public double AverageGrade { get; set; }
}