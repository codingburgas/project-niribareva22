using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolGradebook.Data;
using SchoolGradebook.Models;
using Microsoft.AspNetCore.Authorization;

namespace SchoolGradebook.Controllers;

// Only logged-in users can access this controller
[Authorize]
public class GradesController : Controller
{
    private readonly ApplicationDbContext _context;

    // Inject database context
    public GradesController(ApplicationDbContext context)
    {
        _context = context;
    }

    // List all grades with their associated student data
    public async Task<IActionResult> Index()
    {
        var grades = _context.Grades.Include(g => g.Student);
        return View(await grades.ToListAsync());
    }

    // Only teachers can create new grades
    [Authorize(Roles = "Teacher")]
    public IActionResult Create(int studentId)
    {
        ViewBag.StudentId = studentId; 
        // Get subjects for the dropdown menu
        ViewBag.Subjects = new SelectList(_context.Subjects, "Id", "Name");
        return View();
    }

    // Save the new grade to the database
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Grade grade)
    {
        if (ModelState.IsValid)
        {
            // Set timestamp and save record
            grade.CreatedAt = DateTime.Now;
            _context.Add(grade);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Students");
        }
        
        // Return to form if validation fails
        ViewBag.StudentId = grade.StudentId;
        return View(grade);
    }
}