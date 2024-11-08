using Domain.Models;
using Infrastructure.Data.Context;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CityRepository : ICityRepository
    {
        private readonly Context _context;

        public CityRepository(Context context)
        {
            _context = context;
        }

        public async Task<City> CreateCidade(City city)
        {
            await _context.Cities.AddAsync(city);
            await _context.SaveChangesAsync();
            return city;
        }

        public async Task<List<City>> GetCidade(int? id = null)
        {
            if (id.HasValue)
            {
                return await _context.Cities.AsNoTracking().Where(city => city.Id == id).ToListAsync();
            }

            return await _context.Cities.AsNoTracking().ToListAsync();
        }

        public async Task<bool> UpdateCidade(int id, City city)
        {
            var result = await _context.Cities
                        .Where(city => city.Id == id)
                        .ExecuteUpdateAsync(setters => setters
                        .SetProperty(c => c.StateId, city.StateId)
                        .SetProperty(c => c.Name, city.Name));
            return result > 0;
        }

        public async Task<bool> DeleteCidade(int id)
        {
            var result = await _context.Cities
                .Where(city => city.Id == id)
                .ExecuteDeleteAsync();
            return result > 0;
        }

        public async Task<City> GetCidadeById(int id)
        {
            return await _context.Cities.AsNoTracking().FirstOrDefaultAsync(city => city.Id == id);
        }
    }
}
