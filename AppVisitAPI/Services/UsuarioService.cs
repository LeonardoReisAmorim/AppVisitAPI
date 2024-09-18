using AppVisitAPI.DTOs.UsuarioDTO;
using AppVisitAPI.Interfaces.IUsuario;
using AppVisitAPI.Models;
using AutoMapper;
using System.Security.Cryptography;
using System.Text;

namespace AppVisitAPI.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;

        public UsuarioService(IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }

        public async Task<UsuarioDTO> Alterar(UsuarioDTO usuarioDto)
        {
            var usuario = _mapper.Map<Usuario>(usuarioDto);
            var usuarioAlterado = await _usuarioRepository.Alterar(usuario);
            return _mapper.Map<UsuarioDTO>(usuarioAlterado);
        }

        public async Task<UsuarioDTO> Excluir(int id)
        {
            var usuario = await _usuarioRepository.Excluir(id);
            return _mapper.Map<UsuarioDTO>(usuario);
        }

        public async Task<UsuarioDTO> Incluir(UsuarioDTO usuarioDto)
        {
            var usuario = _mapper.Map<Usuario>(usuarioDto);

            if(usuarioDto.Password != null)
            {
                using var hmac = new HMACSHA256();
                var passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(usuarioDto.Password));
                var passwordSalt = hmac.Key;

                usuario.PasswordHash = passwordHash;
                usuario.PasswordSalt = passwordSalt;
            }

            var usuarioIncluido = await _usuarioRepository.Incluir(usuario);
            return _mapper.Map<UsuarioDTO>(usuarioIncluido);
        }

        public async Task<UsuarioDTO> SelecionarAsync(int id)
        {
            var usuario = await _usuarioRepository.SelecionarAsync(id);
            return _mapper.Map<UsuarioDTO>(usuario);
        }

        public async Task<IEnumerable<UsuarioDTO>> SelecionarTodosAsync()
        {
            var usuarios = await _usuarioRepository.SelecionarTodosAsync();
            return _mapper.Map<IEnumerable<UsuarioDTO>>(usuarios);
        }
    }
}
