using Microsoft.EntityFrameworkCore;
using WeebApp.Data;
using WeebApp.Models.Domain;
using WeebApp.Services.Interfaces;

namespace WeebApp.Services.Posts
{
    public class BasePostService :IBasePostService
    {
        protected readonly ApplicationDbContext _context;

        public BasePostService(ApplicationDbContext context)
        {
            _context = context;
        }
        public Post GetById(Guid id)
        {
            var post = _context.Posts.Include(p => p.Status).FirstOrDefault(p => p.Id == id);
            
            return post;
        }
    }
}