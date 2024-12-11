using AutoMapper;
using WorldWideNews.API.Entities;
using WorldWideNews.API.Repository;
using WorldWideNews.Models.Dtos;

namespace WorldWideNews.API.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();

            CreateMap<Reporter, ReporterDto>();
            CreateMap<ReporterDto, Reporter>();

            CreateMap<News, NewsDto>();
            CreateMap<NewsDto, News>();

            CreateMap<NewsAgency, NewsAgencyDto>();
            CreateMap<NewsAgencyDto , NewsAgency>();    

            CreateMap<Country, CountryDto>();
            CreateMap<CountryDto,Country>();
        }
    }
}
