using Application.DTOs.TypePlaceDTO;
using Application.Interfaces;
using Application.Utils;
using AutoMapper;
using Domain.Models;
using Infrastructure.Interfaces;

namespace Application.Services
{
    public class TypePlaceService : ITypePlaceService
    {
        private readonly ITypePlaceRepository _typePlaceRepository;
        private readonly IMapper _mapper;
        private readonly FileUtils _fileUtils;

        public TypePlaceService(ITypePlaceRepository typePlaceRepository, IMapper mapper, FileUtils fileUtils)
        {
            _typePlaceRepository = typePlaceRepository;
            _mapper = mapper;
            _fileUtils = fileUtils;
        }

        public async Task<TypePlaceDTO> CreateTypePlace(TypePlaceDTO typePlaceDTO)
        {
            if (!_fileUtils.ExistsFilePng(typePlaceDTO.Type.ToLower())) 
            {
                byte[] content = Convert.FromBase64String(typePlaceDTO.ImageRequest);
                await _fileUtils.CreateFilePng(typePlaceDTO.Type.ToLower(), content);
            }

            TypePlace typePlace = new()
            {
                Type = typePlaceDTO.Type,
                ImageUrl = _fileUtils.GetFilePathPng(typePlaceDTO.Type.ToLower())
            };
            
            return _mapper.Map<TypePlaceDTO>(await _typePlaceRepository.CreateTypePlace(typePlace));
        }

        public async Task<List<TypePlaceDTO>> GetTypePlace(int? id = null)
        {
            List<TypePlace> typePlaces = await _typePlaceRepository.GetTypePlaces(id);
            List<TypePlaceDTO> typePlaceDTOs = new();

            typePlaces.ForEach(async (t) =>
            {
                typePlaceDTOs.Add(new TypePlaceDTO
                {
                    Id = t.Id,
                    Type = t.Type,
                    Image = await _fileUtils.ReadAllBytesPng(t.Type)
                });
            });

            return typePlaceDTOs;
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
