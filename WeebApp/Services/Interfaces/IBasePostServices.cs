using WeebApp.Models.Domain;

namespace WeebApp.Services.Interfaces
{
    public interface IBasePostService
    {
        Post GetById(Guid id);
    }
}