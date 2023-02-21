using App.Example;
using App.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace App.Controllers;

public class HomeController : Controller
{
    private readonly IExampleService _exampleService;

    public HomeController(IExampleService exampleService)
    {
        _exampleService = exampleService;
    }

    public IActionResult Index()
    {
        ViewBag.CurrentTime = _exampleService.GetCurrentTime();
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
