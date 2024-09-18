using AppVisitAPI.DTOs.UsuarioDTO;
using AppVisitAPI.Models;

namespace AppVisitAPI.Interfaces.IUsuario
{
    public interface IUsuarioService
    {
        Task<UsuarioDTO> Incluir(UsuarioDTO usuario);
        Task<UsuarioDTO> Alterar(UsuarioDTO usuario);
        Task<UsuarioDTO> Excluir(int id);
        Task<UsuarioDTO> SelecionarAsync(int id);
        Task<IEnumerable<UsuarioDTO>> SelecionarTodosAsync();
    }
}
