using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolGradebook.Data;
using SchoolGradebook.Models;
using Microsoft.AspNetCore.Authorization;

namespace SchoolGradebook.Controllers;

[Authorize]
public class GradesController : Controller
{
    private readonly ApplicationDbContext _context;

    public GradesController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var grades = _context.Grades.Include(g => g.Student);
        return View(await grades.ToListAsync());
    }
    [Authorize(Roles = "Teacher")]
    public IActionResult Create(int studentId)
    {
        ViewBag.StudentId = studentId; 
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Grade grade)
    {
        if (ModelState.IsValid)
        {
            grade.CreatedAt = DateTime.Now;
            _context.Add(grade);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Students");
        }
        ViewBag.StudentId = grade.StudentId;
        return View(grade);
    }
}