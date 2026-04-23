using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolGradebook.Data;
using SchoolGradebook.Models;

namespace SchoolGradebook.Controllers;

[Authorize(Roles = "Teacher")]
public class AttendanceController : Controller
{
    private readonly ApplicationDbContext _context;

    public AttendanceController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Create(int studentId)
    {
        ViewBag.StudentId = studentId;
        ViewBag.Subjects = new SelectList(_context.Subjects, "Id", "Name");
        return View();
    }

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
    
        ViewBag.Subjects = new SelectList(_context.Subjects, "Id", "Name");
        return View(attendance);
    }
}