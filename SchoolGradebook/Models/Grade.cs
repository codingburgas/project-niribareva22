using System.ComponentModel.DataAnnotations;

namespace SchoolGradebook.Models;

public class Grade : BaseEntity
{
    [Range(2.00, 6.00)]
    public double Value { get; set; }

    [Required]
    public string Subject { get; set; } = string.Empty;
    
    public int StudentId { get; set; }
    public Student? Student { get; set; }
}