using WeebApp.Models.Domain;

namespace WeebApp.Services.Interfaces
{
    public interface IAdminPostServices
    {
        List<Post> GetAll();
        Post GetById(Guid id);
        
        void Approve(Post post);
        void Reject(Post post);
    }
}
