using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolGradebook.Data;
using Microsoft.AspNetCore.Authorization;

namespace SchoolGradebook.Controllers;
[Authorize]
public class DashboardController : Controller
{
    private readonly ApplicationDbContext _context;

    public DashboardController(ApplicationDbContext context) => _context = context;

    public async Task<IActionResult> Index()
    {
        var stats = await _context.Students
            .Select(s => new {
                s.Name,
                Average = s.Grades.Any() ? s.Grades.Average(g => g.Value) : 0,
                AbsenceCount = _context.Attendances.Count(a => a.StudentId == s.Id)
            })
            .ToListAsync();

        return View(stats);
    }
}