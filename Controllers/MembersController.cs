using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Paredes_Gabriel_LibraryManagement.Data;
using Paredes_Gabriel_LibraryManagement.Models;

namespace Paredes_Gabriel_LibraryManagement.Controllers;

public class MembersController : Controller
{
    private readonly LibraryManagementContext _context;

    public MembersController(LibraryManagementContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(int page = 1)
    {
        int pageSize = 5;
        if (page < 1) page = 1;

        int totalItems = await _context.Members.CountAsync();
        int totalPages = Math.Max(1, (int)Math.Ceiling(totalItems / (double)pageSize));
        if (page > totalPages) page = totalPages;

        ViewBag.CurrentPage = page;
        ViewBag.TotalPages = totalPages;

        var members = await _context.Members
            .OrderBy(m => m.MemberId)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()
            .ToListAsync();

        return View(members);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();
        var member = await _context.Members.FirstOrDefaultAsync(m => m.MemberId == id);
        if (member == null) return NotFound();
        return View(member);
    }

    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("MemberId,Name,MembershipId,ContactInfo,JoinDate")] Member member)
    {
        if (ModelState.IsValid)
        {
            _context.Add(member);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(member);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();
        var member = await _context.Members.FindAsync(id);
        if (member == null) return NotFound();
        return View(member);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("MemberId,Name,MembershipId,ContactInfo,JoinDate")] Member member)
    {
        if (id != member.MemberId) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(member);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MemberExists(member.MemberId)) return NotFound();
                else throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(member);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();
        var member = await _context.Members.FirstOrDefaultAsync(m => m.MemberId == id);
        if (member == null) return NotFound();
        return View(member);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var member = await _context.Members.FindAsync(id);
        if (member != null) _context.Members.Remove(member);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool MemberExists(int id) => _context.Members.Any(e => e.MemberId == id);
}