using Domain.Models;
using Infrastructure.Data.Context;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class TypePlaceRepository : ITypePlaceRepository
    {
        private readonly Context _context;

        public TypePlaceRepository(Context context)
        {
            _context = context;
        }

        public async Task<TypePlace> CreateTypePlace(TypePlace typePlace)
        {
            await _context.TypePlaces.AddAsync(typePlace);
            await _context.SaveChangesAsync();
            return typePlace;
        }

        public async Task<List<TypePlace>> GetTypePlaces(int? id = null)
        {
            if (id.HasValue)
            {
                return await _context.TypePlaces.AsNoTracking().Where(city => city.Id == id).ToListAsync();
            }

            return await _context.TypePlaces.AsNoTracking().ToListAsync();
        }

        public async Task<bool> UpdateTypePlace(int id, TypePlace typePlace)
        {
            var result = await _context.TypePlaces
                        .Where(typeplace => typeplace.Id == id)
                        .ExecuteUpdateAsync(setters => setters
                        .SetProperty(c => c.Type, typePlace.Type)
                        .SetProperty(c => c.ImageUrl, typePlace.ImageUrl));
            return result > 0;
        }

        public async Task<bool> DeleteTypePlace(int id)
        {
            var result = await _context.TypePlaces
                .Where(typeplace => typeplace.Id == id)
                .ExecuteDeleteAsync();
            return result > 0;
        }

        public async Task<TypePlace> GetTypePlaceById(int id)
        {
            return await _context.TypePlaces.AsNoTracking().FirstOrDefaultAsync(typeplace => typeplace.Id == id);
        }
    }
}
