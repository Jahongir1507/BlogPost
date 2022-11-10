using WeebApp.Models.Domain;

namespace WeebApp.Services.Interfaces
{
    public interface IPostServices : IBasePostServices
    {
        List<Post> GetLastEight();
        
    }
}
