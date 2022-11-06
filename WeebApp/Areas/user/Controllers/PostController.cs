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
using WeebApp.Services.Users;
using WeebApp.Enums;
using WeebApp.Services.Interfaces;

namespace WeebApp.Areas.user.Controllers
{
    [Area("user")]
    [Authorize(Roles = "User")]
    public class PostController : Controller
    {
        private IUserPostServices _userPostServices;

        public PostController(IUserPostServices userPostServices)
        {
            _userPostServices = userPostServices;
        }

        // GET: user/Creator

        public async Task<IActionResult> Index()
        {
            var curUserId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var curUserPost = _userPostServices.GetByCreatorId(curUserId);
            return View(curUserPost);
        }
        // GET: user/Creator/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = _userPostServices.Details((Guid)id);
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
        public async Task<IActionResult> Create(string submitBtn, AddPostViewModel addPostRequest)
        {
            if (ModelState.IsValid)
            {
                var currentUserId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                Post post = new Post()
                {
                    Name = addPostRequest.Name,
                    Text = addPostRequest.Text,
                    CreatedDate = DateTime.Now,
                    CreatorId = currentUserId

                };

                switch (submitBtn)
                {
                    case "Create as draft":
                        post.StatusId = Enums.StatusEnum.Draft;
                        break;
                    case "Submit to check":
                        post.StatusId = Enums.StatusEnum.WaitingForApproval;
                        break;
                }

                var addedPost = _userPostServices.AddPost(post);
            }
            return RedirectToAction("Index");
        }

        // GET: user/Creator/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = _userPostServices.GetById(id.Value);
            if (post == null)
            {
                return NotFound();
            }
            if (post.StatusId == Enums.StatusEnum.WaitingForApproval)
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
        public async Task<IActionResult> Edit(Guid id, string submitBtn, [Bind("Id,Name,Text,CreatedDate,CreatorId")] UpdatePostViewModel updatePostVM)
        {
            var post = _userPostServices.GetById(id);
            // var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (id != updatePostVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid && post != null)
            {
                try
                {
                    post.Name = updatePostVM.Name;
                    post.Text = updatePostVM.Text;

                    switch (submitBtn)
                    {
                        case "Save as draft":
                            post.StatusId = Enums.StatusEnum.Draft;
                            break;
                        case "Submit to check":
                            post.StatusId = Enums.StatusEnum.WaitingForApproval;
                            break;
                    }
                    _userPostServices.EditPost(post);
                }

                catch (DbUpdateConcurrencyException)
                {
                    if (!_userPostServices.PostExists(updatePostVM.Id))
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
            if (id == null)
            {
                return NotFound();
            }
            var post = _userPostServices.GetById(id.Value);
            if (post.StatusId == StatusEnum.WaitingForApproval)
            {
                return NotFound();
            }
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
            var post = _userPostServices.GetById(id);
            if (post != null)
            {
                _userPostServices.Delete(post);
            }
            return RedirectToAction(nameof(Index));
        }


    }
}