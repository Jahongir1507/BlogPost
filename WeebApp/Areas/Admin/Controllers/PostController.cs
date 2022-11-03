using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WeebApp.Data;
using Microsoft.AspNetCore.Authorization;
using WeebApp.Services.Admin;

namespace WeebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class PostController : Controller
    {
        private  AdminPostServices _postServices;

        public PostController(ApplicationDbContext context)
        {
            _postServices = new AdminPostServices(context);
        }

        // GET: Admin/Post
        public IActionResult Index()
        {
            var posts = _postServices.GetAll();
            return View(posts);
        }

        // GET: Admin/Post/Details/5
 
        public IActionResult Details(Guid id)
        {
            var post = _postServices.GetById(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

 [HttpPost]
        public IActionResult Approve(Guid? id)
        {
            var post = _postServices.GetById((Guid)id);
            _postServices.Approve(post);
         return Ok();
        }

        [HttpPost]
        public IActionResult Reject(Guid? id)
        {
         var post = _postServices.GetById((Guid)id);
            _postServices.Reject(post);
            return Ok();

        }
     }

}     