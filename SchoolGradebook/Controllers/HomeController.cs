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

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        // Взимаме последните 5 оценки и отсъствия
        ViewBag.RecentGrades = await _context.Grades
            .OrderByDescending(g => g.Id)
            .Take(5)
            .Include(g => g.Student)
            .Include(g => g.Subject)
            .ToListAsync();

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

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}