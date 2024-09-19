using AppVisitAPI.Account;
using AppVisitAPI.DTOs.UsuarioDTO;
using AppVisitAPI.Interfaces.IUsuario;
using AppVisitAPI.ModelsApi;
using Microsoft.AspNetCore.Mvc;

namespace AppVisitAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsuarioController : Controller
    {
        private readonly IAuthenticate _authenticateService;
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IAuthenticate authenticateService, IUsuarioService usuarioService)
        {
            _authenticateService = authenticateService;
            _usuarioService = usuarioService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserToken>> Incluir(UsuarioDTO usuarioDto)
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

            if (!existe) return BadRequest("usuario nao existe");

            var result = await _authenticateService.AuthenticateAsync(loginModel.Email, loginModel.Password);

            if (!result) return BadRequest("senha invalida");

            var usuario = await _authenticateService.GetUserByEmail(loginModel.Email);

            var token = await _authenticateService.GenerateToken(usuario.Id, usuario.Email);

            return new UserToken { Token = token, UsuarioId = usuario.Id };
        }
    }
}
