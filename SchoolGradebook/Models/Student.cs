using System.ComponentModel.DataAnnotations;

namespace SchoolGradebook.Models;

// Represents a student entity in the school system
public class Student : BaseEntity
{
    // The student's name, required and capped at 50 characters
    [Required]
    [StringLength(50)]
    public string Name { get; set; } = string.Empty;

    // The class name, which is required
    [Required]
    public string ClassName { get; set; }

    // List of grades associated with this student
    public List<Grade> Grades { get; set; } = new List<Grade>();
}