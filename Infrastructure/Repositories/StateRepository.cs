using Domain.Models;
using Infrastructure.Data.Context;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class StateRepository : IStateRepository
    {
        private readonly Context _context;

        public StateRepository(Context context)
        {
            _context = context;
        }

        public async Task<State> CreateEstado(State state)
        {
            await _context.States.AddAsync(state);
            await _context.SaveChangesAsync();
            return state;
        }

        public async Task<List<State>> GetEstado(int? id = null)
        {
            if (id.HasValue)
            {
                return await _context.States.AsNoTracking().Where(state => state.Id == id).ToListAsync();
            }

            return await _context.States.AsNoTracking().ToListAsync();

        }

        public async Task<bool> UpdateEstado(int id, State state)
        {
            var result = await _context.States
                        .Where(state => state.Id == id)
                        .ExecuteUpdateAsync(setters => setters
                        .SetProperty(e => e.CountryId, state.CountryId)
                        .SetProperty(e => e.Name, state.Name));
            return result > 0;
        }

        public async Task<bool> DeleteEstado(int id)
        {
            var result = await _context.States
                        .Where(state => state.Id == id)
                        .ExecuteDeleteAsync();
            return result > 0;
        }

        public async Task<State> GetEstadoById(int id)
        {
            return await _context.States.AsNoTracking().FirstOrDefaultAsync(state => state.Id == id);
        }
    }
}
