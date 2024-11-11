using Domain.Models;
using Infrastructure.Data.Context;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PlaceRepository : IPlaceRepository
    {
        private readonly Context _context;

        public PlaceRepository(Context context)
        {
            _context = context;
        }

        public async Task<Place> CreateLugar(Place place)
        {
            await _context.Places.AddAsync(place);
            await _context.SaveChangesAsync();
            return place;
        }

        public async Task<List<Place>> GetLugar(int? id = null)
        {
            if (id.HasValue)
            {
                return await _context.Places.AsNoTracking()
                             .Include(place => place.FileVR)
                             .Include(place => place.City)
                             .Where(place => place.Id == id)
                             .AsSplitQuery()
                             .Select(x => new Place
                             {
                                 City = x.City,
                                 CityId = x.CityId,
                                 Description = x.Description,
                                 FileVR = new FileVR
                                 {
                                     CreatedAt = x.FileVR.CreatedAt,
                                     FileName = x.FileVR.FileName,
                                     Id = x.FileVR.Id
                                 },
                                 Id = x.Id,
                                 FileVRId = x.FileVR.Id,
                                 Image = x.Image,
                                 Name = x.Name,
                                 UsageInstructionsVR = x.UsageInstructionsVR
                             })
                             .ToListAsync();
            }

            return await _context.Places.AsNoTracking()
                         .Include(place => place.FileVR)
                         .Include(place => place.City)
                         .AsSplitQuery()
                         .Select(x => new Place
                         {
                             City = x.City,
                             CityId = x.CityId,
                             Description = x.Description,
                             FileVR = new FileVR
                             {
                                 CreatedAt = x.FileVR.CreatedAt,
                                 FileName = x.FileVR.FileName,
                                 Id = x.FileVR.Id
                             },
                             Id = x.Id,
                             FileVRId = x.FileVR.Id,
                             Image = x.Image,
                             Name = x.Name,
                             UsageInstructionsVR = x.UsageInstructionsVR
                         })
                         .ToListAsync();
        }

        public async Task<string?> GetUtilizacaoLugarVRById(int id)
        {
            if (id == 0)
            {
                return null;
            }

            return await _context.Places
                         .AsNoTracking()
                         .Where(place => place.Id == id)
                         .Select(place => place.UsageInstructionsVR)
                         .SingleOrDefaultAsync();
        }

        public async Task<bool> UpdateLugar(int id, Place place)
        {
            var result = await _context.Places
                .Where(place => place.Id == id)
                .ExecuteUpdateAsync(setters => setters
                .SetProperty(l => l.FileVRId, place.FileVRId)
                .SetProperty(l => l.Description, place.Description)
                .SetProperty(l => l.CityId, place.CityId)
                .SetProperty(l => l.Image, place.Image)
                .SetProperty(l => l.UsageInstructionsVR, place.UsageInstructionsVR)
                .SetProperty(l => l.Name, place.Name));
            return result > 0;
        }

        public async Task<bool> DeleteLugar(int id)
        {
            var result = await _context.Places
                .Where(place => place.Id == id)
                .ExecuteDeleteAsync();
            return result > 0;
        }

        public async Task<bool> ExistsArquivo(int arquivoId)
        {
            return await _context.Places.AnyAsync(place => place.FileVRId == arquivoId);
        }

        public async Task<Place> GetLugarById(int id)
        {
            return await _context.Places.AsNoTracking().FirstOrDefaultAsync(place => place.Id == id);
        }
    }
}
