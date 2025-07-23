using AutoMapper;

using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.CategoryDto;
using Application.ProductDto;
using Application.CartDto;


namespace Application
{
    public class MappingProfile:Profile 
    {
        public MappingProfile() {

         

            CreateMap<Product, ProductDtos>();

            CreateMap<ProductDtos, Product>();

            
            CreateMap<CreateProductDto, Product>();

            CreateMap<UpdateProductDto, Product>();



            CreateMap<CreateCategoryDtos, Category>();

            CreateMap<Category, CategoryDtos>();

            CreateMap<CategoryDtos, Category>();


            CreateMap<Cart, getCartDto>();

            CreateMap<CartItem, CartItemDto>();
            CreateMap<CartItemDto, CartItem>();
        }
    }
}
