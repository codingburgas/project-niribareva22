using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolGradebook.Data;
using SchoolGradebook.Models;
using SchoolGradebook.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace SchoolGradebook.Controllers;


[Authorize]
public class StudentsController : Controller
{
    private readonly ApplicationDbContext _context;

    public StudentsController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(string searchString)
    {
        var query = _context.Students.AsQueryable();

        if (!string.IsNullOrEmpty(searchString))
        {
            query = query.Where(s => s.Name.Contains(searchString));
        }

        var students = await query
            .Select(s => new StudentDTO {
                Id = s.Id,
                Name = s.Name,
                ClassName = s.ClassName
            }).ToListAsync();

        return View(students);
    }
  
    [Authorize(Roles = "Teacher")]
    public IActionResult Create()
    {
        return View();
    }

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

    [Authorize(Roles = "Teacher")]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();
        var student = await _context.Students.FindAsync(id);
        if (student == null) return NotFound();
        return View(student);
    }

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

    [Authorize(Roles = "Teacher")]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();
        var student = await _context.Students.FirstOrDefaultAsync(m => m.Id == id);
        if (student == null) return NotFound();
        return View(student);
    }

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