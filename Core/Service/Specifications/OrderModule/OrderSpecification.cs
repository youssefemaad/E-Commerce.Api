using DomainLayer.orderModule;

namespace Service.Specifications;

class OrderSpecification : BaseSpecification<Order, Guid>
{
    public OrderSpecification(string email) : base(o => o.buyerEmail == email)
    {
        AddInclude(o => o.DeliveryMethod);
        AddInclude(o => o.Items);
        AddOrderByDescending(o => o.OrderDate);
    }

    public OrderSpecification(Guid id) : base(o => o.Id == id)
    {
        AddInclude(o => o.DeliveryMethod);
        AddInclude(o => o.Items);
    }
}

