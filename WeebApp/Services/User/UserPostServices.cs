using Microsoft.EntityFrameworkCore;
using WeebApp.Data;
using WeebApp.Models;
using WeebApp.Models.Domain;

namespace MvcApp.Services.Users
{
    public class UserPostServices
    {
        public ApplicationDbContext _context;

        public UserPostServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Post> GetByCreatorId(string authorId)
        {
            var authorPosts = _context.Posts.Include(p => p.Status).Where(p => p.CreatorId == authorId);
            return authorPosts.ToList();
        }
        public Post Details(Guid id)
        {
            var post = _context.Posts.Include(p => p.Status).FirstOrDefault(p => p.Id == id);
            return post;
        }

        public Post AddPost(Post post)
        {
            _context.Posts.Add(post);
            _context.SaveChanges();
            return post;
        }
        public Post GetById(Guid id)
        {
            var post = _context.Posts.Find(id);
            return post;
        }

        public void EditPost(Post post)
        {
            _context.Update(post);
            _context.SaveChanges();
        }

        public Post Delete(Post post)
        {
            _context.Posts.Remove(post);
            _context.SaveChanges();
            return post;
        }
        public bool PostExists(Guid id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }
    }
}
