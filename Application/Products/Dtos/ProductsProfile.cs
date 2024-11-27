using Application.Products.Commands.CreateProduct;
using Application.Products.Commands.UpdateProduct;
using AutoMapper;
using Domain.Entities;

namespace Application.Products.Dtos
{
    public class ProductsProfile : Profile
    {
        public ProductsProfile()
        {
            CreateMap<CreateProductCommand, Product>();
            CreateMap<Product, ProductDto>();

            CreateMap<UpdateProductCommand, Product>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom((src, dest) => !string.IsNullOrEmpty(src.Name) ? src.Name : dest.Name))
            .ForMember(dest => dest.Category, opt => opt.MapFrom((src, dest) => !string.IsNullOrEmpty(src.Category) ? src.Category : dest.Category))
            .ForMember(dest => dest.Description, opt => opt.MapFrom((src, dest) => src.Description != null ? src.Description : dest.Description))
            .ForMember(dest => dest.Price, opt => opt.MapFrom((src, dest) => src.Price != default ? src.Price : dest.Price));
        }
    }
}
