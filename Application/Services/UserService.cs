using Application.DTOs.UserDTO;
using Application.Interfaces;
using AutoMapper;
using Domain.Models;
using Infrastructure.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDTO> Alterar(UserDTO usuarioDto)
        {
            var usuario = _mapper.Map<User>(usuarioDto);
            var usuarioAlterado = await _userRepository.Alterar(usuario);
            return _mapper.Map<UserDTO>(usuarioAlterado);
        }

        public async Task<UserDTO> Excluir(int id)
        {
            var usuario = await _userRepository.Excluir(id);
            return _mapper.Map<UserDTO>(usuario);
        }

        public async Task<UserDTO> Incluir(UserDTO usuarioDto)
        {
            var usuario = _mapper.Map<User>(usuarioDto);

            if (usuarioDto.Password != null)
            {
                using var hmac = new HMACSHA256();
                var passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(usuarioDto.Password));
                var passwordSalt = hmac.Key;

                usuario.PasswordHash = passwordHash;
                usuario.PasswordSalt = passwordSalt;
            }

            var usuarioIncluido = await _userRepository.Incluir(usuario);
            return _mapper.Map<UserDTO>(usuarioIncluido);
        }

        public async Task<UserDTO> SelecionarAsync(int id)
        {
            var usuario = await _userRepository.SelecionarAsync(id);
            return _mapper.Map<UserDTO>(usuario);
        }

        public async Task<IEnumerable<UserDTO>> SelecionarTodosAsync()
        {
            var usuarios = await _userRepository.SelecionarTodosAsync();
            return _mapper.Map<IEnumerable<UserDTO>>(usuarios);
        }
    }
}
