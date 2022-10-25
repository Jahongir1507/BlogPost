using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WeebApp.Data;
using Microsoft.AspNetCore.Authorization;


namespace WeebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class PostController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PostController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Post
        public IActionResult Index()
        {
            var posts = _context.Posts.OrderByDescending(p => p.CreatedDate).Where(p => p.StatusId == Enums.StatusEnum.WaitingForApproval).ToList();
            return View(posts);
        }

        // GET: Admin/Post/Details/5
 
        public IActionResult Details(Guid id)
        {
            var post = _context.Posts.Include(p => p.Status).FirstOrDefault(p => p.Id == id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

 [HttpPost]
        public IActionResult Approve(Guid? id)
        {
            var post = _context.Posts.FirstOrDefault(p => p.Id == id);
            if (id == null || post == null)
            {
                return NotFound();
            }
            post.StatusId = Enums.StatusEnum.Published;
            _context.SaveChanges();
            return Ok();
        }

        [HttpPost]
        public IActionResult Reject(Guid? id)
        {
            var post = _context.Posts.FirstOrDefault(p => p.Id == id);
            if (id == null || post == null)
            {
                return NotFound();
            }

            post.StatusId = Enums.StatusEnum.Rejected;
            _context.SaveChanges();

            return Ok();

        }
     }

}     