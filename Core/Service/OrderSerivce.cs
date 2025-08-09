using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models;
using DomainLayer.Models.BasketModule;
using DomainLayer.orderModule;
using Service.Specifications;
using ServiceAbstraction;
using Shared.DataTransferObject.IdentityModuleDto;
using Shared.DataTransferObject.OrderDtos;

namespace Service;

public class OrderSerivce(IMapper _mapper, IBasketRepository basketRepository, IUnitOfWork unitOfWork) : IOrderSerivce
{
    public async Task<OrderToReturnDto> CreateOrder(OrderDto orderDto, string Email)
    {
        var OrderAddress = _mapper.Map<AddressDto, OrderAddress>(orderDto.shippingAddress);
        var Basket = await basketRepository.GetBasketAsync(orderDto.BasketId) ?? throw new BasketNotFoundException(orderDto.BasketId);
        List<OrderItem> orderItems = [];
        var ProductRepo = unitOfWork.GetRepository<Product, int>();

        foreach (var item in Basket.Items)
        {
            var product = await ProductRepo.GetByIdAsync(item.Id) ?? throw new ProductNotFoundException(item.Id);


            orderItems.Add(CreateOrderItem(item, product));
        }

        var DeliveryMethod = await unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(orderDto.DeliveryMethodId)
            ?? throw new DeliveryMethodNotFoundException(orderDto.DeliveryMethodId);

        var subtotal = orderItems.Sum(item => item.Price * item.Quantity);
        var order = new Order(Email, OrderAddress, DeliveryMethod, orderItems, subtotal);

        await unitOfWork.GetRepository<Order, Guid>().AddAsync(order);
        await unitOfWork.SaveChangesAsync();

        return _mapper.Map<Order, OrderToReturnDto>(order);
    }

    private static OrderItem CreateOrderItem(BasketItem item, Product product)
    {
        return new OrderItem()
        {
            Product = new ProductItemOrder()
            {
                ProductId = product.Id,
                PictureUrl = product.PictureUrl,
                ProductName = product.Name
            },
            Price = product.Price,
            Quantity = item.Quantity
        };
    }

    public async Task<IEnumerable<DeliveryMethodDto>> GetDeliveryMethodsAsync()
    {
        var deliveryMethods = await unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsync();
        return _mapper.Map<IEnumerable<DeliveryMethod>, IEnumerable<DeliveryMethodDto>>(deliveryMethods);
    }

    public async Task<IEnumerable<OrderToReturnDto>> GetAllOrdersAsync(string Email)
    {
        var Spec = new OrderSpecification(Email);
        var orders = await unitOfWork.GetRepository<Order, Guid>().GetAllAsync(Spec);
        return _mapper.Map<IEnumerable<Order>, IEnumerable<OrderToReturnDto>>(orders);
    }

    public async Task<OrderToReturnDto> GetOrderById(Guid id)
    {
        var spec = new OrderSpecification(id);
        var Order = await unitOfWork.GetRepository<Order, Guid>().GetByIdAsync(spec);
        return _mapper.Map<Order, OrderToReturnDto>(Order!);
    }

}
