using AppVisitAPI.Models;

namespace AppVisitAPI.Account
{
    public interface IAuthenticate
    {
        Task<bool> AuthenticateAsync(string email, string senha);
        Task<bool> UserExists(string email);
        public Task<string> GenerateToken(int id, string email);
        public Task<Usuario> GetUserByEmail(string email);
    }
}
