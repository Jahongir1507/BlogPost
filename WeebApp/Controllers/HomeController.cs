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
        private readonly ILogger<HomeController> _logger;
       // private readonly ApplicationDbContext _applicationDbContext;
        private readonly IPostServices _postServices;


        public HomeController(ILogger<HomeController> logger, IPostServices postServices)
        {
            _logger = logger;
            _postServices = postServices;

        }

        [HttpGet]

        public IActionResult Index()
        {
            /*var posts = _postServices.Posts.OrderByDescending(p => p.CreatedDate).Where(p => p.StatusId == 
            Enums.StatusEnum.Published).Take(8).ToList();*/
            var posts = _postServices.GetLastEight();
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