using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolGradebook.Data;

namespace SchoolGradebook.Controllers;

public class DashboardController : Controller
{
    private readonly ApplicationDbContext _context;

    public DashboardController(ApplicationDbContext context) => _context = context;

    public async Task<IActionResult> Index()
    {
        var stats = await _context.Students
            .Select(s => new {
                s.Name,
                Average = s.Grades.Any() ? s.Grades.Average(g => g.Value) : 0
            }).ToListAsync();

        return View(stats);
    }
}