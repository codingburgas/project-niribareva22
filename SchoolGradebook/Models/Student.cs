using System.ComponentModel.DataAnnotations;

namespace SchoolGradebook.Models;

public class Student : BaseEntity
{
    [Required]
    [StringLength(50)]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string ClassName { get; set; }
    public List<Grade> Grades { get; set; } = new();
}