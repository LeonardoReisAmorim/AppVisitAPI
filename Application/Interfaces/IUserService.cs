using Application.DTOs.UserDTO;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<UserDTO> Incluir(UserDTO usuario);
        Task<UserDTO> Alterar(UserDTO usuario);
        Task<UserDTO> Excluir(int id);
        Task<UserDTO> SelecionarAsync(int id);
        Task<IEnumerable<UserDTO>> SelecionarTodosAsync();
    }
}
