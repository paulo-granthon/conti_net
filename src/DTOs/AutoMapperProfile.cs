using AutoMapper;
using ContiNet.Models;
using ContiNet.DTOs;

public class AutoMapperProfile : Profile
{
  public AutoMapperProfile()
  {
    CreateContinentMappings();
    CreateCountryMappings();
  }

  void CreateContinentMappings()
  {
    CreateContinentToDTOMappings();
    CreateContinentFromDTOMappings();
  }

  void CreateCountryMappings()
  {
    CreateCountryToDTOMappings();
    CreateCountryFromDTOMappings();
  }

  void CreateContinentToDTOMappings()
  {
    CreateMap<Continent, ContinentDTO>()
      .ForMember(dest => dest.Countries, opt => opt.MapFrom(src => src.Countries))
      .ForMember(dest => dest.Population, opt => opt.MapFrom(src => src.Countries.Sum(country => country.Population)));

    CreateMap<Country, ContinentCountryDTO>()
      .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
      .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
      .ForMember(dest => dest.Population, opt => opt.MapFrom(src => src.Population));
  }

  void CreateContinentFromDTOMappings()
  {
    CreateMap<ContinentDTO, Continent>()
      .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
      .ForMember(dest => dest.Countries, opt => opt.MapFrom(src => src.Countries));

    CreateMap<ContinentCountryDTO, Country>()
      .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
      .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
      .ForMember(dest => dest.Population, opt => opt.MapFrom(src => src.Population));
  }

  void CreateCountryToDTOMappings()
  {
    CreateMap<Country, CountryDTO>()
      .ForMember(dest => dest.ContinentName, opt => opt.MapFrom(src => src.Continent!.Name));
  }

  void CreateCountryFromDTOMappings()
  {
    CreateMap<CountryDTO, Country>()
      .ForMember(dest => dest.Continent, opt => opt.MapFrom(src => new Continent { Name = src.ContinentName }));
  }
}
