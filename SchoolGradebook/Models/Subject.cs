namespace SchoolGradebook.Models;

// Represents a subject taught in the school
public class Subject : BaseEntity 
{
    // The name of the subject
    public string Name { get; set; } = string.Empty;
    
    // The name of the teacher assigned to the subject
    public string TeacherName { get; set; } = string.Empty;
}