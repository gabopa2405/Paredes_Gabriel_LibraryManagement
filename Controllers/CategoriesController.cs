using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Paredes_Gabriel_LibraryManagement.Data;
using Paredes_Gabriel_LibraryManagement.Models;

namespace Paredes_Gabriel_LibraryManagement.Controllers;

public class CategoriesController : Controller
{
    private readonly LibraryManagementContext _context;

    public CategoriesController(LibraryManagementContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(int page = 1)
    {
        int pageSize = 5;
        if (page < 1) page = 1;

        int totalItems = await _context.Categories.CountAsync();
        int totalPages = Math.Max(1, (int)Math.Ceiling(totalItems / (double)pageSize));
        if (page > totalPages) page = totalPages;

        ViewBag.CurrentPage = page;
        ViewBag.TotalPages = totalPages;

        var categories = await _context.Categories
            .OrderBy(c => c.GenreId)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()
            .ToListAsync();

        return View(categories);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();
        var category = await _context.Categories.FirstOrDefaultAsync(m => m.GenreId == id);
        if (category == null) return NotFound();
        return View(category);
    }

    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("GenreId,GenreName")] Category category)
    {
        if (ModelState.IsValid)
        {
            _context.Add(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(category);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();
        var category = await _context.Categories.FindAsync(id);
        if (category == null) return NotFound();
        return View(category);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("GenreId,GenreName")] Category category)
    {
        if (id != category.GenreId) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(category);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(category.GenreId)) return NotFound();
                else throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(category);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();
        var category = await _context.Categories.FirstOrDefaultAsync(m => m.GenreId == id);
        if (category == null) return NotFound();
        return View(category);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category != null) _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool CategoryExists(int id) => _context.Categories.Any(e => e.GenreId == id);
}