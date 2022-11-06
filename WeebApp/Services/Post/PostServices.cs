using Microsoft.EntityFrameworkCore;
using WeebApp.Data;
using WeebApp.Models.Domain;
using WeebApp.Services.Interfaces;
using NuGet.Versioning;
using System;
using WeebApp.Services.Posts;

namespace WeebApp.Services.Posts
{
    public class PostServices : IPostServices
    {
        private readonly ApplicationDbContext _context;

        public PostServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public Post GetById(Guid id)
        {
            var post = _context.Posts.Include(p => p.Status)
                .Include(p => p.Creator).FirstOrDefault(p => p.Id == id);
            return post;
        }

        public List<Post> GetLastEight()
        {
            var posts = _context.Posts.Where(p => p.StatusId == Enums.StatusEnum.Published)
                .OrderByDescending(p => p.CreatedDate).Take(8).ToList();
            return posts;
        }
    }
}