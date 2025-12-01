using DomainLayer.orderModule;

namespace Service.Specifications;

class OrderSpecification : BaseSpecification<Order,Guid>
{
    public OrderSpecification(string email) : base(o => o.UserEmail == email)
    {
        AddOrderIncludes();
        AddOrderByDescending(o => o.OrderDate);
    }

    public OrderSpecification(Guid id) : base(o => o.Id == id)
    {
        AddOrderIncludes();
    }

    private void AddOrderIncludes()
    {
        AddInclude(o => o.DeliveryMethod);
        AddInclude(o => o.Items);
    }
}
    
