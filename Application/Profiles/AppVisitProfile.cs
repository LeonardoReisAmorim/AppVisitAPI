using Application.DTOs.CityDTO;
using Application.DTOs.CountryDTO;
using Application.DTOs.PlaceDTO;
using Application.DTOs.StateDTO;
using Application.DTOs.UserDTO;
using AutoMapper;
using Domain.Models;

namespace Application.Profiles
{
    public class AppVisitProfile : Profile
    {
        public AppVisitProfile()
        {
            CreateMap<CountryDTO, Country>();
            CreateMap<Country, ReadCountryDTO>();

            CreateMap<StateDTO, State>();
            CreateMap<State, ReadStateDTO>();

            CreateMap<CityDTO, City>();
            CreateMap<City, ReadCityDTO>();

            CreateMap<PlaceDTO, Place>()
                .ForMember(x => x.Image, y => y.MapFrom(a => Convert.FromBase64String(a.Image)));
            CreateMap<Place, ReadPlaceDTO>()
            .ForMember(x => x.Image, y => y.MapFrom(a => Convert.ToBase64String(a.Image)));

            CreateMap<User, UserDTO>().ReverseMap();
        }
    }
}
