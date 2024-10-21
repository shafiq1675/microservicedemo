using Basket.API.Models;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Basket.API.Repositories
{
    public interface IBasketRepository
    {
        Task<ShoppingCart> GetShoppingCart(string userName);
        Task<ShoppingCart> UpdateShoppingCart(ShoppingCart cart);
        Task DeleteShoppingCart(string userName);
    }
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _distributedCache;
        public BasketRepository(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task DeleteShoppingCart(string userName)
        {
            await _distributedCache.RemoveAsync(userName);
        }

        public async Task<ShoppingCart> GetShoppingCart(string userName)
        {
            var basket = await _distributedCache.GetStringAsync(userName);
            if (string.IsNullOrEmpty(basket))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<ShoppingCart>(basket);
        }

        public async Task<ShoppingCart> UpdateShoppingCart(ShoppingCart cart)
        {
            await _distributedCache.SetStringAsync(cart.UserName, JsonConvert.SerializeObject(cart));
            return await GetShoppingCart(cart.UserName);
        }
    }
}
