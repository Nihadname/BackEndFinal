﻿using Microsoft.AspNetCore.Mvc;

namespace BackEndFinal.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
