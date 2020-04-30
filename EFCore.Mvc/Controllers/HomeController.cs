using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EFCore.Mvc.Models;
using EFBlog.Domain.Repositories;
using EFBlog.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace EFCore.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BlogService _blogService;

        public HomeController(ILogger<HomeController> logger, BlogService blogService)
        {
            _logger = logger;
            _blogService = blogService;
        }



        public async Task<IActionResult> Index()
        {
            return View(await _blogService.All());
        }

        [HttpPost]
        public async Task<IActionResult> Add(Blog blog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                await _blogService.Add(blog.Url);
                return RedirectToAction(actionName: nameof(Index));
            }
        }


        public async Task<IActionResult> AddString(string blog)
        {
            await _blogService.Add(blog);
            return View("Index");
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
}
