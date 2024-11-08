using Domain.Models;
using Infrastructure.Data.Context;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly Context _context;

        public UserRepository(Context context)
        {
            _context = context;
        }

        public async Task<User> Incluir(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> Alterar(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> Excluir(int id)
        {
            var usuario = await _context.Users.FindAsync(id);
            if (usuario == null)
            {
                return null;
            }

            _context.Users.Remove(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task<User> SelecionarAsync(int id)
        {
            return await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<User>> SelecionarTodosAsync()
        {
            return await _context.Users.ToListAsync();
        }
    }
}
