using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Data;
using System.Diagnostics;
using WeebApp.Data;
using WeebApp.Models;
using WeebApp.Services.Interfaces;
using WeebApp.Services.Posts;
namespace WeebApp.Controllers
{
    public class HomeController : Controller
    {
       private IPostServices _blogPostServices;

        public HomeController(IPostServices postServices)
        { 
            _blogPostServices = postServices;
        }

        [HttpGet]

        public IActionResult Index()
        {
           var posts = _blogPostServices.GetLastEight();
            return View(posts);
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