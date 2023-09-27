using AppVisitAPI.DTOs.PaisDTO;
using AppVisitAPI.Models;
using AutoMapper;

namespace AppVisitAPI.Profiles
{
    public class PaisProfile : Profile
    {
        public PaisProfile()
        {
            CreateMap<CriarPaisDTO, Pais>();
            CreateMap<Pais, LerPaisDTO>();
            CreateMap<EditarPaisDTO, Pais>();
        }
    }
}
