using Domain.Models;
using Infrastructure.Data.Context;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly Context _context;

        public CountryRepository(Context context)
        {
            _context = context;
        }

        public async Task<Country> CreatePais(Country country)
        {
            await _context.Countries.AddAsync(country);
            await _context.SaveChangesAsync();
            return country;
        }

        public async Task<List<Country>> GetPais(int? id = null)
        {
            if (id.HasValue)
            {
                return await _context.Countries.AsNoTracking().Where(country => country.Id == id).ToListAsync();
            }

            return await _context.Countries.AsNoTracking().ToListAsync();
        }

        public async Task<bool> UpdatePais(int id, Country country)
        {
            var result = await _context.Countries
                         .Where(country => country.Id == id)
                         .ExecuteUpdateAsync(setters => setters
                         .SetProperty(p => p.Name, country.Name));
            return result > 0;
        }

        public async Task<bool> DeletePais(int id)
        {
            var result = await _context.Countries
                         .Where(country => country.Id == id)
                         .ExecuteDeleteAsync();
            return result > 0;
        }

        public async Task<Country> GetPaisById(int id)
        {
            return await _context.Countries.AsNoTracking().FirstOrDefaultAsync(country => country.Id == id);
        }
    }
}
