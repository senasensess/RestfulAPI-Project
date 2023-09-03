using RestfulAPI_Project.Models.Entities.Concrete;

namespace RestfulAPI_Project.Infrastructure.Repositories.Interfaces
{
    public interface IAuthRepository
    {
        AppUser Authentication(string userName, string password);
    }
}
