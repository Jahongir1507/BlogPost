using WeebApp.Models.Domain;

namespace WeebApp.Services.Interfaces
{
    public interface IAdminPostServices : IBasePostServices
    {
        List<Post> GetAll();
        
        void Approve(Post post);
        void Reject(Post post);
    }
}
