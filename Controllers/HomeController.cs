using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Paredes_Gabriel_LibraryManagement.Data;
using Paredes_Gabriel_LibraryManagement.Models;

namespace Paredes_Gabriel_LibraryManagement.Controllers;

public class HomeController : Controller
{
    private readonly LibraryManagementContext _context;

    public HomeController(LibraryManagementContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        ViewBag.AuthorsCount = await _context.Authors.CountAsync();
        ViewBag.CategoriesCount = await _context.Categories.CountAsync();
        ViewBag.BooksCount = await _context.Books.CountAsync();
        ViewBag.MembersCount = await _context.Members.CountAsync();
        ViewBag.LoansCount = await _context.Loans.CountAsync();
        ViewBag.ActiveLoansCount = await _context.Loans.CountAsync(l => l.ReturnStatus == false || l.ReturnStatus == null);
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}