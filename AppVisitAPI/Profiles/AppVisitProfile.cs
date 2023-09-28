using AppVisitAPI.DTOs.ArquivoDTO;
using AppVisitAPI.DTOs.CidadeDTO;
using AppVisitAPI.DTOs.EstadoDTO;
using AppVisitAPI.DTOs.LugarDTO;
using AppVisitAPI.DTOs.PaisDTO;
using AppVisitAPI.Models;
using AutoMapper;

namespace AppVisitAPI.Profiles
{
    public class AppVisitProfile : Profile
    {
        public AppVisitProfile()
        {
            //pais
            CreateMap<CriarPaisDTO, Pais>();
            CreateMap<Pais, LerPaisDTO>();
            CreateMap<EditarPaisDTO, Pais>();

            //estado
            CreateMap<CriarEstadoDTO, Estado>();
            CreateMap<Estado, LerEstadoDTO>();
            CreateMap<EditarEstadoDTO, Estado>();

            //cidade
            CreateMap<CriarCidadeDTO, Cidade>();
            CreateMap<Cidade, LerCidadeDTO>();
            CreateMap<EditarCidadeDTO, Cidade>();

            //lugar
            CreateMap<InserirLugarDTO, Lugar>()
                .ForMember(x => x.Imagem, y => y.MapFrom(a => Convert.FromBase64String(a.Imagem)));
            CreateMap<Lugar, LerLugarDTO>()
                .ForMember(x => x.Imagem, y => y.MapFrom(a => Convert.ToBase64String(a.Imagem)));
            CreateMap<EditarLugarDTO, Lugar>()
                .ForMember(x => x.Imagem, y => y.MapFrom(a => Convert.FromBase64String(a.Imagem)));
        }
    }
}
