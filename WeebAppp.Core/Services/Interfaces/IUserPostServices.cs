using WeebApp.Models.Domain;

namespace WeebApp.Services.Interfaces
{
    public interface IUserPostServices
    {
        List<Post> GetByCreatorId(string CreatorId);

        Post Details(Guid id);

        Post AddPost(Post post);

        Post GetById(Guid id);

        void EditPost(Post post);

        void Delete(Post post);

        bool PostExists(Guid id);
    }
}
