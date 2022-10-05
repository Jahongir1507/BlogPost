using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WeebApp.Data;
using WeebApp.Models.Domain;
using System.Linq;
using System.Security.Claims;
using WeebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace WeebApp.Areas.user.Controllers
{
    [Area("user")]
    [Authorize(Roles="User")]
    public class PostController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PostController(ApplicationDbContext context)
        {
            _context = context;
        }
       
        // GET: user/Creator

        public async Task<IActionResult> Index()
        {
            var curUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var UserPost = _context.Posts.OrderByDescending(p => p.CreatedDate);
            var UserPost_2 = UserPost.Where(p => p.CreatorId == curUserId);
            return View(await UserPost_2.ToListAsync());
        }
        // GET: user/Creator/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Posts == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .Include(p => p.Creator)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
            }

           // GET: user/Creator/Create
           public IActionResult Create()
           {
                 return View();
           }

        // POST: user/Creator/Create

        [HttpPost]
        public async Task<IActionResult> Create(AddPostViewModel addPostRequest)
        {
            if (ModelState.IsValid)
            {
                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var post = new Post()
                {
                    Name = addPostRequest.Name,
                    Text = addPostRequest.Text,
                    CreatedDate = DateTime.Now,
                    CreatorId = currentUserId

                };
                //_context.Posts.Add(post);
                await _context.Posts.AddAsync(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }



            // return RedirectToAction("Create");
              return RedirectToAction(nameof(Index));
           // return View(addPostRequest);
        }

        // GET: user/Creator/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Posts == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
           
            return View(post);
        }

        // POST: user/Creator/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, UpdatePostViewModel postUpdateVM)
        {
            var post = await _context.Posts.FindAsync(id);
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (id != postUpdateVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                     post.Name = postUpdateVM.Name;
                     post.Text = postUpdateVM.Text;
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }

               catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(post);

        }

        // GET: user/Creator/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Posts == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .Include(p => p.Creator)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: user/Creator/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Posts == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Posts'  is null.");
            }
            var post = await _context.Posts.FindAsync(id);
            if (post != null)
            {
                _context.Posts.Remove(post);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(Guid id)
        {
          return _context.Posts.Any(e => e.Id == id);
        }

    }
}
