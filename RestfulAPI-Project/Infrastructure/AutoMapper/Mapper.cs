using AutoMapper;
using RestfulAPI_Project.Models.DTO_s.AuthDTO;
using RestfulAPI_Project.Models.DTO_s.CategoryDTO;
using RestfulAPI_Project.Models.Entities.Concrete;

namespace RestfulAPI_Project.Infrastructure.AutoMapper
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<Category, GetCategoryDTO>().ReverseMap();
            CreateMap<Category, CreateCategoryDTO>().ReverseMap();
            CreateMap<Category, UpdateCategoryDTO>().ReverseMap();

            CreateMap<AppUser, AuthenticationDTO>().ReverseMap();
        }
    }
}
