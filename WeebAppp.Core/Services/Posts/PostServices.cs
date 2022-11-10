using Microsoft.EntityFrameworkCore;
using WeebApp.Data;
using WeebApp.Models.Domain;
using WeebApp.Services.Interfaces;
using System;
using WeebApp.Services.Posts;

namespace WeebApp.Services.Posts
{
    public class PostServices : BasePostServices, IPostServices
    {
       public PostServices(ApplicationDbContext context) : base(context)
        {

        }

        public List<Post> GetLastEight()
        {
            var posts = _applicationDbContext.Posts.Where(p => p.StatusId == Enums.StatusEnum.Published)
                .OrderByDescending(p => p.CreatedDate).Take(8).ToList();
            return posts;
        }
    }
}