﻿using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models;

namespace WebApplication3.Controllers;

public class CalculatorController : Controller
{
    private readonly ILogger<HomeController> _logger;
    
    public CalculatorController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }
    
    public IActionResult Form()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult Result([FromForm] Calculator model)
    {
        if (!model.IsValid())
        {
            return View("Error");
        }
        ViewBag.Result = model.Calculate();
        return View(model);
    }
}