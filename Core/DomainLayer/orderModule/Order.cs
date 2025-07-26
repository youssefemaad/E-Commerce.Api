using System.ComponentModel.DataAnnotations.Schema;
using DomainLayer.Models;
using DomainLayer.Models.ProductModule;

namespace DomainLayer.orderModule;

public class Order : BaseEntity<Guid>
{
    public Order()
    {
    }
    public Order(string buyerEmail, OrderAddress shipToAddress, DeliveryMethod deliveryMethod, ICollection<OrderItem> items, decimal subtotal)
    {
        this.buyerEmail = buyerEmail;
        this.shipToAddress = shipToAddress;
        this.DeliveryMethod = deliveryMethod;
        this.Items = items;
        this.Subtotal = subtotal;
    }

    public string buyerEmail { get; set; } = default!;
    public OrderAddress shipToAddress { get; set; } = default!;
    public DeliveryMethod DeliveryMethod { get; set; } = default!;
    public ICollection<OrderItem> Items { get; set; } = [];
    public decimal Subtotal { get; set; }

    public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
    public int DeliveryMethodId { get; set; }
    public OrderStatus Status { get; set; }
    public decimal GetTotal() => Subtotal + DeliveryMethod.Price;
}
