using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolGradebook.Models;

namespace SchoolGradebook.Data;

// Database context that handles both Identity users and custom application data
public class ApplicationDbContext : IdentityDbContext
{
    // Constructor to pass configuration options (like connection strings) to the base class
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // Database sets representing your application tables
    public DbSet<Student> Students { get; set; }
    public DbSet<Grade> Grades { get; set; }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<Attendance> Attendances { get; set; }
}