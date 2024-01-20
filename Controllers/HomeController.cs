using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PayrollManagementSystem.Data;
using PayrollManagementSystem.Models;
using SelectPdf;

namespace PayrollManagementSystem.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    public readonly EmployeeDbContext dbContext;
    public HomeController(EmployeeDbContext dbContext,ILogger<HomeController> logger)
        {
            this.dbContext = dbContext;
             _logger = logger;
        }


    public IActionResult Index()
    {
        return View();
    }


    public IActionResult Privacy()
    {
        return View();
    }
    public IActionResult contactus()
    {
        return View();
    }
            public IActionResult customErrorPage()
        {
            return View();
        }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
