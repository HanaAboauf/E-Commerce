using AutoMapper;
using E_Commerce.Domain.Entities.ProductModule;
using E_Commerce.Shared.DTOs.ProductDTOs;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services.Profiles
{
    internal class ProductPictureURLResolver : IValueResolver<Product, ProductDTO, string>
    {
        private readonly IConfiguration _Configration;

        public ProductPictureURLResolver(IConfiguration configration)
        {
            _Configration = configration;
        }

        public string Resolve(Product source, ProductDTO destination, string destMember, ResolutionContext context)
        {
            if(string.IsNullOrEmpty(source.PictureUrl)) return string.Empty;

            if(source.PictureUrl.StartsWith("http")) return source.PictureUrl;

            var baseUrl = _Configration.GetSection("URLs")["BaseURL"];   
            
            if(string.IsNullOrEmpty(baseUrl)) return string.Empty;

            return $"{baseUrl}{source.PictureUrl}";


        }
    }
}
