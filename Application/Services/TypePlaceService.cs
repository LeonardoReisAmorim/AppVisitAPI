using Application.DTOs.TypePlaceDTO;
using Application.Interfaces;
using AutoMapper;
using Domain.Models;
using Infrastructure.Interfaces;

namespace Application.Services
{
    public class TypePlaceService : ITypePlaceService
    {
        private readonly ITypePlaceRepository _typePlaceRepository;
        private readonly IMapper _mapper;

        public TypePlaceService(ITypePlaceRepository typePlaceRepository, IMapper mapper)
        {
            _typePlaceRepository = typePlaceRepository;
            _mapper = mapper;
        }

        public async Task<TypePlaceDTO> CreateTypePlace(TypePlaceDTO typePlaceDTO)
        {
            TypePlace typePlace = _mapper.Map<TypePlace>(typePlaceDTO);
            return _mapper.Map<TypePlaceDTO>(await _typePlaceRepository.CreateTypePlace(typePlace));
        }

        public async Task<List<TypePlaceDTO>> GetTypePlace(int? id = null)
        {
            return _mapper.Map<List<TypePlaceDTO>>(await _typePlaceRepository.GetTypePlaces(id));
        }

        public async Task<bool> UpdateTypePlace(int id, TypePlaceDTO updateTypePlaceDTO)
        {
            TypePlace typePlace = _mapper.Map<TypePlace>(updateTypePlaceDTO);
            return await _typePlaceRepository.UpdateTypePlace(id, typePlace);
        }

        public async Task<bool> DeleteTypePlace(int id)
        {
            return await _typePlaceRepository.DeleteTypePlace(id);
        }
    }
}
