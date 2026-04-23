using System.ComponentModel.DataAnnotations;
using SchoolGradebook.Models;

namespace SchoolGradebook.Models;

public class Grade : BaseEntity
{
    [Required]
    [Range(2.00, 6.00, ErrorMessage = "Grade must be between 2.00 and 6.00")]
    public double Value { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 2)]
    public string Subject { get; set; } = string.Empty;
    
    public int StudentId { get; set; }
    
    public Student? Student { get; set; }
}