using AutoMapper;
using E_Commerce.Application.DTOS.Product;
using E_Commerce.Domain.Entities.Products;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Profiles
{
    public class PictureUrlResolver(IOptions<UrlSettings> urlSettings) : IValueResolver<Product, ProductDto, string>
    {
        private readonly UrlSettings _urlSettings = urlSettings.Value;
       
        public string? Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
                return null;
            var baseUrl = _urlSettings.BaseUrl.TrimEnd('/');
            var path = source.PictureUrl.TrimStart('/');

            return $"{baseUrl}/files/{path}";
        }
    }
    

    public class  UrlSettings
    {
        public string BaseUrl { get; set; } = string.Empty;
    }
}
