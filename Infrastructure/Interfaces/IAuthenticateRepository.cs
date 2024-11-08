using Domain.Models;

namespace Infrastructure.Interfaces
{
    public interface IAuthenticateRepository
    {
        Task<User> GetUserByEmail(string email);
        Task<bool> UserExists(string email);
    }
}
