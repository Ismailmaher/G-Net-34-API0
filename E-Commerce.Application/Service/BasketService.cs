using AutoMapper;
using E_Commerce.Application.Common;
using E_Commerce.Application.Contracts;
using E_Commerce.Application.DTOS.Baskets;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.Baskets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Service
{
    public class BasketService(IBasketRepository basketRepository, IMapper mapper) : IBasketService
    {
        public async Task<Result<BasketDto>> CreateOrUpdateBasketAsync(BasketDto basket, CancellationToken ct = default)
        {
            var customerBasket = mapper.Map<CustomerBasket>(basket);
            var basketresult = await basketRepository.CreateOrUpdateBasketAsync(customerBasket, ct: ct);
            return basketresult != null ? Result<BasketDto>.Ok(mapper.Map<BasketDto>(basketresult)) : Result<BasketDto>.Fail(Error.Failure("Can Not Create or Update Basket"));
        }

        public async Task<Result<bool>> DeleteBasketAsync(string basketId, CancellationToken ct = default)
        {
           var result = await basketRepository.DeleteBasketAsync(basketId, ct);
            return result ? Result<bool>.Ok(true) : Result<bool>.Fail(Error.Failure("Can Not Delete Basket"));
        }

        public async Task<Result<BasketDto>> GetBasketAsync(string Id, CancellationToken ct = default)
        {
            var basket = await basketRepository.GetBasketAsync(Id, ct);
            if (basket == null)
                return Result<BasketDto>.Fail(Error.Failure("Basket not found"));
            return mapper.Map<BasketDto>(basket);
        }
    }
}
