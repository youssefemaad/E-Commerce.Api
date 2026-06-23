using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DataTransferObject.BasketModuleDto;

namespace Presentation.Controllers
{
    public class BasketsController (IServiceManager serviceManager): ApiControllerBase
    {
        // Get Basket
        [HttpGet]
        public async Task<ActionResult<BasketDto>> GetBasket(string id)
        {
            var Basket = await serviceManager.basketService.GetBasketAsync(id);
            return Ok(Basket);
        }

        //Create Or Update Basket
        [HttpPost]
        public async Task<ActionResult<BasketDto>> CreateOrUpdateBasket (BasketDto basketDto)
        {
            var basket = await serviceManager.basketService.CreateOrUpdateAsync(basketDto);
            return Ok(basket);
        }


        //DeleteBasket
        [HttpDelete("{key}")]
        public async Task<ActionResult<bool>> DeleteBasket(string key)
        {
            var Result = await serviceManager.basketService.DeleteBasketAsync(key);
            return Ok(Result);
        }

    }
}
