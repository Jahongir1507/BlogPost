using WeebApp.Models;
using WeebApp.Models.Domain;

namespace WeebApp.Services.Interfaces
{
    public interface IBasePostServices
    {
        Post GetById(Guid id);
    }
}
