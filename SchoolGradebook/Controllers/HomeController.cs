using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolGradebook.Models;
using SchoolGradebook.Data;

namespace SchoolGradebook.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;

    // Inject logger and database context
    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        // Get the 5 most recent grades and include related data
        ViewBag.RecentGrades = await _context.Grades
            .OrderByDescending(g => g.Id)
            .Take(5)
            .Include(g => g.Student)
            .Include(g => g.Subject)
            .ToListAsync();

        // Get the 5 most recent attendance records and include related data
        ViewBag.RecentAbsences = await _context.Attendances
            .OrderByDescending(a => a.Date)
            .Take(5)
            .Include(a => a.Student)
            .Include(a => a.Subject)
            .ToListAsync();

        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    // Display error information
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}