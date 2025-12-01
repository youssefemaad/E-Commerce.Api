using AutoMapper;
using DomainLayer.orderModule;
using Microsoft.Extensions.Configuration;
using Shared.DataTransferObject.OrderDtos;

namespace Service.MappingProfile;

public class OrderItemPictureUrlResolver(IConfiguration configuration) : BasePictureUrlResolver(configuration), IValueResolver<OrderItem, OrderItemDto, string>
{
    public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
    {
        return ResolveUrl(source.Product.PictureUrl);
    }
}
