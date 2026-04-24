using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolGradebook.Data;
using Microsoft.AspNetCore.Authorization;

namespace SchoolGradebook.Controllers;

// Only logged-in users can access the dashboard
[Authorize]
public class DashboardController : Controller
{
    private readonly ApplicationDbContext _context;

    // Inject database context
    public DashboardController(ApplicationDbContext context) => _context = context;

    public async Task<IActionResult> Index()
    {
        // Get student statistics from the database
        var stats = await _context.Students
            .Select(s => new {
                s.Name,
                // Calculate average grade or return 0 if none exist
                Average = s.Grades.Any() ? s.Grades.Average(g => g.Value) : 0,
                // Count total absences for each student
                AbsenceCount = _context.Attendances.Count(a => a.StudentId == s.Id)
            })
            .ToListAsync();

        // Send statistics to the view
        return View(stats);
    }
}