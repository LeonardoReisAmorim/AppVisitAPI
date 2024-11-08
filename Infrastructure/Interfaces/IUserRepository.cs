using Domain.Models;

namespace Infrastructure.Interfaces
{
    public interface IUserRepository
    {
        Task<User> Incluir(User usuario);
        Task<User> Alterar(User usuario);
        Task<User> Excluir(int id);
        Task<User> SelecionarAsync(int id);
        Task<IEnumerable<User>> SelecionarTodosAsync();
    }
}
