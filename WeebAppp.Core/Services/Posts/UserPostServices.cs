using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeebApp.Data;
using WeebApp.Models;
using WeebApp.Models.Domain;
using WeebApp.Services.Interfaces;

namespace WeebApp.Services.Posts
{
    public class UserPostServices : BasePostServices, IUserPostServices
    {

        public UserPostServices(ApplicationDbContext context):base(context)
        {
        }

        public List<Post> GetByCreatorId(string authorId)
        {
            var authorPosts = _applicationDbContext.Posts.Include(p => p.Status).Where(p => p.CreatorId == authorId);
            return authorPosts.ToList();
        }
        public Post Details(Guid id)
        {
            var post = _applicationDbContext.Posts.Include(p => p.Status).FirstOrDefault(p => p.Id == id);
            return post;
        }

        public Post AddPost(Post post)
        {
            _applicationDbContext.Posts.Add(post);
            _applicationDbContext.SaveChanges();
            return post;
        }
  
        public void EditPost(Post post)
        {
            _applicationDbContext.Update(post);
            _applicationDbContext.SaveChanges();
        }

        public void Delete(Post post)
        {
            _applicationDbContext.Posts.Remove(post);
            _applicationDbContext.SaveChanges();

        }
        public bool PostExists(Guid id)
        {
            return _applicationDbContext.Posts.Any(e => e.Id == id);
        }
    }
}