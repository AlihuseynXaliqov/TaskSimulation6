﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TaskSimulation6.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles ="Admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
