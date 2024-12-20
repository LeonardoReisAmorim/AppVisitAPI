﻿using Application.DTOs.UserDTO;
using Domain.Models;

namespace Application.Interfaces
{
    public interface IAuthenticate
    {
        Task<bool> AuthenticateAsync(string email, string senha);
        public Task<string> GenerateToken(int id, string email);
        Task<bool> UserExists(string email);
        Task<UserDTO> GetUserByEmail(string email);
    }
}
