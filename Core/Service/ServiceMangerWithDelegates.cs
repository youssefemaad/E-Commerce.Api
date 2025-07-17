using ServiceAbstraction;

namespace Service;

public class ServiceMangerWithDelegates(Func<IProductService> ProductFactory,
                                        Func<IBasketService> BasketFactroy,
                                        Func<IOrderSerivce> OrderFactory,
                                        Func<IAuthenticationService> AuthenticationFactory) : IServiceManager
{
    public IProductService ProductService => ProductFactory.Invoke();

    public IBasketService basketService => BasketFactroy.Invoke();

    public IAuthenticationService AuthenticationService => AuthenticationFactory.Invoke();

    public IOrderSerivce OrderService => OrderFactory.Invoke();

}
