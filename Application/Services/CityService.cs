using Application.DTOs.CityDTO;
using Application.Interfaces;
using AutoMapper;
using Domain.Models;
using Infrastructure.Interfaces;

namespace Application.Services
{
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;
        private readonly IMapper _mapper;

        public CityService(ICityRepository cityRepository, IMapper mapper)
        {
            _cityRepository = cityRepository;
            _mapper = mapper;
        }

        public async Task<ReadCityDTO> CreateCidade(CityDTO cidadeDTO)
        {
            City cidade = _mapper.Map<City>(cidadeDTO);
            return _mapper.Map<ReadCityDTO>(await _cityRepository.CreateCidade(cidade));
        }

        public async Task<List<ReadCityDTO>> GetCidade(int? id = null)
        {
            return _mapper.Map<List<ReadCityDTO>>(await _cityRepository.GetCidade(id));
        }

        public async Task<bool> UpdateCidade(int id, CityDTO updateCidadeDTO)
        {
            City cidade = _mapper.Map<City>(updateCidadeDTO);
            return await _cityRepository.UpdateCidade(id, cidade);
        }

        public async Task<bool> DeleteCidade(int id)
        {
            return await _cityRepository.DeleteCidade(id);
        }
    }
}
