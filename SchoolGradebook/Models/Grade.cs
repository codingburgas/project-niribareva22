using System.ComponentModel.DataAnnotations;
using SchoolGradebook.Models;

namespace SchoolGradebook.Models;

// Represents a grade record for a student in a specific subject
public class Grade : BaseEntity
{
    // The numerical grade value, restricted to a range of 2.00 to 6.00
    [Required]
    [Range(2.00, 6.00, ErrorMessage = "Grade must be between 2.00 and 6.00")]
    public double Value { get; set; }

    // Foreign key and navigation property for the related subject
    public int SubjectId { get; set; }
    public Subject? Subject { get; set; }
    
    // Foreign key and navigation property for the related student
    public int StudentId { get; set; }
    public Student? Student { get; set; }
}