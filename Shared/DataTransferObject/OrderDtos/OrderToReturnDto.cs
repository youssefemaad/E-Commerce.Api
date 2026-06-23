using Shared.DataTransferObject.IdentityModuleDto;

namespace Shared.DataTransferObject.OrderDtos;

public class OrderToReturnDto
{
    public Guid Id { get; set; }
    public string buyerEmail { get; set; } = default!;
    public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
    public AddressDto shipToAddress { get; set; } = default!;
    public string DeliveryMethod { get; set; } = default!;
    public decimal DeliveryCost { get; set; }
    public string Status { get; set; } = default!;
    public ICollection<OrderItemDto> OrderItems { get; set; } = [];
    public decimal Subtotal { get; set; }
    public decimal Total {get; set;}
}
