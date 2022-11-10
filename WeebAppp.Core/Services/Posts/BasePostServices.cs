using Microsoft.EntityFrameworkCore;
using WeebApp.Models.Domain;
using WeebApp.Data;
using WeebApp.Services.Interfaces;
using System.Security.Cryptography.X509Certificates;

namespace WeebApp.Services.Posts
{
    public class BasePostServices : IBasePostServices
    {
        protected readonly ApplicationDbContext _applicationDbContext;
        public BasePostServices(ApplicationDbContext applicationDbContext) 
        { 
            _applicationDbContext = applicationDbContext;
        }
        public Post GetById(Guid id)
        {    
            var post = _applicationDbContext.Posts.Include(p => p.Status)
                .Include(p => p.Creator).FirstOrDefault(p => p.Id == id);
           return post;
        }
    }
}
