using AutoMapper;
using DomainLayer.Models;
using Shared.DataTransferObject;
using Microsoft.Extensions.Configuration;

namespace Service.MappingProfile
{
    public class PictureUrlResolver(IConfiguration configuration) : BasePictureUrlResolver(configuration), IValueResolver<Product, ProductDto, string>
    {
        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            return ResolveUrl(source.PictureUrl);
        }
    }
}