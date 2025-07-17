using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.BasketModule;
using ServiceAbstraction;
using Shared.DataTransferObject.BasketModuleDto;

namespace Service
{
    public class BasketService(IBasketRepository basketRepository, IMapper mapper) : IBasketService
    {
        public async Task<BasketDto> CreateOrUpdateAsync(BasketDto basket)
        {
            var CustomerBasket = mapper.Map<BasketDto,CustomerBasket>(basket);
            var IsCreatedOrUpdated = await basketRepository.CreateOrUpdateBasketAsync(CustomerBasket);

            if(IsCreatedOrUpdated is not null)
                return await GetBasketAsync(basket.Id);
            else
                throw new Exception("Can Not Update Or Create Basket Now");
        }

        public async Task<bool> DeleteBasketAsync(string key) => await basketRepository.DeleteBasketAsync(key);

        public async Task<BasketDto> GetBasketAsync(string key)
        {
            var basket = await basketRepository.GetBasketAsync(key);

            if(basket is not null)
                return mapper.Map<CustomerBasket,BasketDto>(basket);
            else
                throw new BasketNotFoundException(key);
        }
    }
}
