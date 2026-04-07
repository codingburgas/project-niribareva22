using System.ComponentModel.DataAnnotations;
namespace SchoolGradebook.Models;

public abstract class BaseEntity
{
    [Key]
    public int Id { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}