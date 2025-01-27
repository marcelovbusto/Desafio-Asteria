using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AsteriaChallenger.Models;

namespace AsteriaChallenger.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public class SalesController : Controller
    {
        public IActionResult Index()
        {
            // Retorna a view: Views/Sales/Index.cshtml
            return View();
        }
    }

    public HomeController(ILogger<HomeController> logger)
    {
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

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
