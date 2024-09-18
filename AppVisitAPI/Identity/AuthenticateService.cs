using AppVisitAPI.Account;
using AppVisitAPI.Data.Context;
using AppVisitAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace AppVisitAPI.Identity
{
    public class AuthenticateService : IAuthenticate
    {
        private readonly Context _context;
        private readonly IConfiguration _configuration;

        public AuthenticateService(Context context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<bool> AuthenticateAsync(string email, string senha)
        {
            var usuario = await _context.Usuarios.Where(x => x.Email.ToLower() == email.ToLower()).FirstOrDefaultAsync();
            if (usuario == null)
            {
                return false;
            }

            using var hmac = new HMACSHA256(usuario.PasswordSalt);
            var computerHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(senha));
            for( var i = 0; i < computerHash.Length; i++)
            {
                if (computerHash[i] != usuario.PasswordHash[i]) return false;
            }

            return true;
        }

        public async Task<string> GenerateToken(int id, string email)
        {
            var privateKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwt:secretKey"]));
            var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256Signature);
            var usuario = await this.GetUserByEmail(email);
            var tokenConfig = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("id", id.ToString()),
                    new Claim("email", email),
                    new Claim("admin", usuario.IsAdmin.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(60),
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenConfig);

            return tokenHandler.WriteToken(token);
        }

        public async Task<Usuario> GetUserByEmail(string email)
        {
            return await _context.Usuarios.Where(x => x.Email.ToLower() == email.ToLower()).FirstOrDefaultAsync();
        }

        public async Task<bool> UserExists(string email)
        {
            return await _context.Usuarios.AnyAsync(x => x.Email.ToLower() == email.ToLower());
        }
    }
}
