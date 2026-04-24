using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolGradebook.Data;
using SchoolGradebook.Models;

namespace SchoolGradebook.Controllers;

// Only teachers can access this controller
[Authorize(Roles = "Teacher")]
public class AttendanceController : Controller
{
    private readonly ApplicationDbContext _context;

    // Inject database context
    public AttendanceController(ApplicationDbContext context)
    {
        _context = context;
    }

    // Show the page to create a new attendance record
    public IActionResult Create(int studentId)
    {
        ViewBag.StudentId = studentId;
        // Get list of subjects for the dropdown menu
        ViewBag.Subjects = new SelectList(_context.Subjects, "Id", "Name");
        return View();
    }

    // Save the new attendance record to the database
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Attendance attendance)
    {
        if (ModelState.IsValid)
        {
            _context.Attendances.Add(attendance);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Students");
        }
    
        // If data is invalid, reload the subject list and return the view
        ViewBag.Subjects = new SelectList(_context.Subjects, "Id", "Name");
        return View(attendance);
    }
}