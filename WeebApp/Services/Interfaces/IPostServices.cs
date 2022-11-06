using WeebApp.Models.Domain;

namespace WeebApp.Services.Interfaces
{
    public interface IPostServices
    {
        List<Post> GetLastEight();
        
        Post GetById(Guid id);
    }
}
