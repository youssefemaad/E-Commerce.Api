using AutoMapper;
using DomainLayer.Models;
using Shared.DataTransferObject;
using Microsoft.Extensions.Configuration;

namespace Service.MappingProfile
{
    public class PictureUrlResolver(IConfiguration configuration) : IValueResolver<Product, ProductDto, string>
    {
        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.PictureUrl))
            {
                return string.Empty;
            }
            else
            {
                var baseUrl = configuration.GetSection("Urls")["BaseUrl"];
                var pictureUrl = source.PictureUrl.StartsWith("/") ? source.PictureUrl : "/" + source.PictureUrl;
                var Url = $"{baseUrl}{pictureUrl}";
                return Url;
            }
        }
    }
}