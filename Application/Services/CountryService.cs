using Application.DTOs.CountryDTO;
using Application.Interfaces;
using AutoMapper;
using Domain.Models;
using Infrastructure.Interfaces;

namespace Application.Services
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;

        public CountryService(ICountryRepository countryRepository, IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        public async Task<ReadCountryDTO> CreatePais(CountryDTO paisDTO)
        {
            Country pais = _mapper.Map<Country>(paisDTO);
            return _mapper.Map<ReadCountryDTO>(await _countryRepository.CreatePais(pais));
        }

        public async Task<List<ReadCountryDTO>> GetPais(int? id = null)
        {
            return _mapper.Map<List<ReadCountryDTO>>(await _countryRepository.GetPais(id));
        }

        public async Task<bool> UpdatePais(int id, CountryDTO updatePaisDTO)
        {
            Country pais = _mapper.Map<Country>(updatePaisDTO);
            return await _countryRepository.UpdatePais(id, pais);
        }

        public async Task<bool> DeletePais(int id)
        {
            return await _countryRepository.DeletePais(id);
        }
    }
}
