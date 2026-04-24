using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolGradebook.Data;
using SchoolGradebook.Models;
using SchoolGradebook.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace SchoolGradebook.Controllers;

// Requires user to be logged in
[Authorize]
public class StudentsController : Controller
{
    private readonly ApplicationDbContext _context;

    // Inject database context
    public StudentsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // List all students with optional name search
    public async Task<IActionResult> Index(string searchString)
    {
        var query = _context.Students.AsQueryable();

        // Filter list if a search term is provided
        if (!string.IsNullOrEmpty(searchString))
        {
            query = query.Where(s => s.Name.Contains(searchString));
        }

        // Project database entities to StudentDTOs
        var students = await query
            .Select(s => new StudentDTO {
                Id = s.Id,
                Name = s.Name,
                ClassName = s.ClassName
            }).ToListAsync();

        return View(students);
    }
  
    // Form to create a new student (Teachers only)
    [Authorize(Roles = "Teacher")]
    public IActionResult Create()
    {
        return View();
    }

    // Save a new student record
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Teacher")]
    public async Task<IActionResult> Create(Student student)
    {
        if (ModelState.IsValid)
        {
            student.CreatedAt = DateTime.Now;
            _context.Add(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(student);
    }

    // Load student data for editing
    [Authorize(Roles = "Teacher")]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();
        var student = await _context.Students.FindAsync(id);
        if (student == null) return NotFound();
        return View(student);
    }

    // Save changes to student record
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Teacher")]
    public async Task<IActionResult> Edit(int id, Student student)
    {
        if (id != student.Id) return NotFound();

        if (ModelState.IsValid)
        {
            _context.Update(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(student);
    }

    // Load confirmation page for deletion
    [Authorize(Roles = "Teacher")]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();
        var student = await _context.Students.FirstOrDefaultAsync(m => m.Id == id);
        if (student == null) return NotFound();
        return View(student);
    }

    // Perform student record deletion
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Teacher")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student != null) _context.Students.Remove(student);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}