using Application.DTOs.PlaceDTO;
using Application.Interfaces;
using AutoMapper;
using Domain.Models;
using Infrastructure.Interfaces;

namespace Application.Services
{
    public class PlaceService : IPlaceService
    {
        private readonly IPlaceRepository _placeRepository;
        private readonly IMapper _mapper;

        public PlaceService(IPlaceRepository placeRepository, IMapper mapper)
        {
            _placeRepository = placeRepository;
            _mapper = mapper;
        }

        public async Task<ReadPlaceDTO> CreateLugar(PlaceDTO lugarDTO)
        {
            if (await _placeRepository.ExistsArquivo(lugarDTO.FileVRId))
            {
                throw new Exception("Já existe um ambiente virtual vinculado a este lugar");
            }

            Place lugar = _mapper.Map<Place>(lugarDTO);
            return _mapper.Map<ReadPlaceDTO>(await _placeRepository.CreateLugar(lugar));
        }

        public async Task<List<ReadPlaceDTO>> GetLugar(int? id = null)
        {
            var result = await _placeRepository.GetLugar(id);

            return result.Select(lugar => new ReadPlaceDTO
                   {
                       FileVRId = lugar.FileVRId,
                       City = lugar.City.Name,
                       Description = lugar.Description,
                       Id = lugar.Id,
                       Image = Convert.ToBase64String(lugar.Image),
                       Name = lugar.Name,
                       FileName = lugar.FileVR.FileName,
                       CityId = lugar.CityId,
                       UsageInstructionsVR = lugar.UsageInstructionsVR
                   }).ToList();
        }

        public async Task<string?> GetUtilizacaoLugarVRById(int id)
        {
            return await _placeRepository.GetUtilizacaoLugarVRById(id);
        }

        public async Task<bool> UpdateLugar(int id, PlaceDTO updateLugarDTO)
        {
            Place lugar = _mapper.Map<Place>(updateLugarDTO);
            return await _placeRepository.UpdateLugar(id, lugar);
        }

        public async Task<bool> DeleteLugar(int id)
        {
            return await _placeRepository.DeleteLugar(id);
        }
    }
}
