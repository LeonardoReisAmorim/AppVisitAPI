using Application.Interfaces;
using Domain.Models;
using Infrastructure.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Application.Services
{
    public class AuthenticateService : IAuthenticate
    {
        private readonly IAuthenticateRepository _authenticateRepository;
        private readonly IConfiguration _configuration;

        public AuthenticateService(IAuthenticateRepository authenticateRepository, IConfiguration configuration)
        {
            _authenticateRepository = authenticateRepository;
            _configuration = configuration;
        }

        public async Task<bool> AuthenticateAsync(string email, string senha)
        {
            var usuario = await _authenticateRepository.GetUserByEmail(email);
            if (usuario == null)
            {
                return false;
            }

            using var hmac = new HMACSHA256(usuario.PasswordSalt);
            var computerHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(senha));
            for (var i = 0; i < computerHash.Length; i++)
            {
                if (computerHash[i] != usuario.PasswordHash[i]) return false;
            }

            return true;
        }

        public async Task<string> GenerateToken(int id, string email)
        {
            var privateKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwt:secretKey"]));
            var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256Signature);
            var usuario = await _authenticateRepository.GetUserByEmail(email);
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

        public async Task<bool> UserExists(string email)
        {
            return await _authenticateRepository.UserExists(email);
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _authenticateRepository.GetUserByEmail(email);
        }
    }
}
