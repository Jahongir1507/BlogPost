using Microsoft.EntityFrameworkCore;
using WeebApp.Data;
using WeebApp.Models;
using System;
using WeebApp.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using WeebApp.Services.Interfaces;

namespace WeebApp.Services.Posts
{
    public class AdminPostServices : IAdminPostServices
    {
        public ApplicationDbContext _context;

        public AdminPostServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Post> GetAll()
        {
             var posts = _context.Posts.Include(p => p.Status).Where(p => p.StatusId != Enums.StatusEnum.Draft).ToList();
            return posts;
        }
        public Post GetById(Guid id)
        {
            var post = _context.Posts.Include(p => p.Status).FirstOrDefault(p => p.Id == id);
            return post;

        }

        public void Approve(Post post)
        {
            post.StatusId = Enums.StatusEnum.Published;
            _context.SaveChanges();
        }

        public void Reject(Post post)
        {
            post.StatusId = Enums.StatusEnum.Rejected;
            _context.SaveChanges();
        }
    }
}
