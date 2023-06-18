using AutoMapper;
using E_Commerce_Backend.Models;
using E_Commerce_Backend.Models.ProductModel;

namespace E_Commerce_Backend.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductDto>();
        }   
    }
}
