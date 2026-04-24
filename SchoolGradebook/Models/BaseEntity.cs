using System.ComponentModel.DataAnnotations;

namespace SchoolGradebook.Models;

// Abstract base class to provide common properties for all database entities
public abstract class BaseEntity
{
    // Primary key for the database table
    [Key]
    public int Id { get; set; }
    
    // Timestamp for when the record was created
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}