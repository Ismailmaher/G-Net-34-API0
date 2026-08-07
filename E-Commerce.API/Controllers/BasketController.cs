using E_Commerce.Application.Contracts;
using E_Commerce.Application.DTOS.Baskets;
using E_Commerce.Application.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{
   
    public class BasketController(IBasketService BasketService) : APIBaseController
    {
        #region Get
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BasketDto), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BasketDto>> GetBasket(string id, CancellationToken ct)
        {
            var basket = await BasketService.GetBasketAsync(id, ct);
            return ToActionResult(basket);
        }
        #endregion
        #region CreateOrUpdate
        [HttpPost]
        public async Task<ActionResult<BasketDto>> CreateOrUpdateBasket(BasketDto basketdto, CancellationToken ct)
        {
            var saved = await BasketService.CreateOrUpdateBasketAsync(basketdto, ct);
            return ToActionResult(saved);
        }
        #endregion
        #region Delete
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteBasket(string id, CancellationToken ct)
        {
            var result = await BasketService.DeleteBasketAsync(id, ct);
            return ToActionResult(result);
        }


        #endregion
    }
}
