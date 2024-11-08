using Application.DTOs.UserDTO;
using Application.Interfaces;
using AppVisitAPI.ModelsApi;
using Microsoft.AspNetCore.Mvc;

namespace AppVisitAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IAuthenticate _authenticateService;
        private readonly IUserService _usuarioService;

        public UserController(IAuthenticate authenticateService, IUserService usuarioService)
        {
            _authenticateService = authenticateService;
            _usuarioService = usuarioService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserToken>> Incluir(UserDTO usuarioDto)
        {
            if (usuarioDto == null) return BadRequest("necessário enviar todos os dados");

            var emailExiste = await _authenticateService.UserExists(usuarioDto.Email);

            if (emailExiste) return BadRequest($"o email {usuarioDto.Email} está cadastrado");

            var usuario = await _usuarioService.Incluir(usuarioDto);

            if (usuario == null) return BadRequest("ocorreu um erro ao cadastrar");

            var token = await _authenticateService.GenerateToken(usuario.Id, usuario.Email);

            return new UserToken { Token = token, UsuarioId = usuario.Id };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserToken>> Selecionar(LoginModel loginModel)
        {
            var existe = await _authenticateService.UserExists(loginModel.Email);

            if (!existe) return BadRequest("Usuário não existe");

            var result = await _authenticateService.AuthenticateAsync(loginModel.Email, loginModel.Password);

            if (!result) return BadRequest("Senha inválida");

            var usuario = await _authenticateService.GetUserByEmail(loginModel.Email);

            var token = await _authenticateService.GenerateToken(usuario.Id, usuario.Email);

            return new UserToken { Token = token, UsuarioId = usuario.Id };
        }
    }
}
