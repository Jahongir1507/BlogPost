using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeebApp.Data;
using WeebApp.Models;
using WeebApp.Models.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WeebApp.Controllers
{

    public class PostsController : Controller
    {
        private readonly ApplicationDbContext applicationDbContext;
        public PostsController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        

        [HttpGet]
       /* public async Task<IActionResult> Index()
        {
            var posts = await applicationDbContext.Posts.ToListAsync();
            return View(posts);
        }*/

        public async Task<IActionResult> Index()
        {
            return applicationDbContext.Posts != null ?
                        View(await applicationDbContext.Posts.OrderByDescending(x => x.CreatedDate).Take(8).ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Posts'  is null.");
        }

        [Authorize]
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Add(AddPostViewModel addPostRequest)
        {
            var post = new Post()
            {
                Id = Guid.NewGuid(),
                Name = addPostRequest.Name,
                Text = addPostRequest.Text,
                CreatedDate = DateTime.Now

            };

            await applicationDbContext.Posts.AddAsync(post);
            await applicationDbContext.SaveChangesAsync();
            return RedirectToAction("Add");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var post = await applicationDbContext.Posts.FirstOrDefaultAsync(x => x.Id == id);

            if (post != null)
            {
                var viewModel = new UpdatePostViewModel()
                {
                    Id = post.Id,
                    Name = post.Name,
                    Text = post.Text,
                  
                };
                return await Task.Run(() => View("View", viewModel));
            }


            return RedirectToAction("Index");
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(UpdatePostViewModel model)
        {
            var post = await applicationDbContext.Posts.FindAsync(model.Id);
            if (post != null)
            { 
                post.Name = model.Name;
                post.Text = model.Text;

                await applicationDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");

        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Delete(UpdatePostViewModel model)
        {
            var post = await applicationDbContext.Posts.FindAsync(model.Id);
            if (post != null)
            {
                applicationDbContext.Posts.Remove(post);
                await applicationDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");

        }

       
    }
}
