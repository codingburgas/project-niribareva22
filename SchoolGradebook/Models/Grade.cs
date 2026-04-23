using System.ComponentModel.DataAnnotations;
using SchoolGradebook.Models;

namespace SchoolGradebook.Models;

public class Grade : BaseEntity
{
    [Required]
    [Range(2.00, 6.00, ErrorMessage = "Grade must be between 2.00 and 6.00")]
    public double Value { get; set; }

    public int SubjectId { get; set; }
    public Subject? Subject { get; set; }
    
    public int StudentId { get; set; }
    
    public Student? Student { get; set; }
}