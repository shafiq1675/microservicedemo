using Basket.API.GRPCServices;
using Basket.API.Models;
using Basket.API.Repositories;
using CoreApiResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Basket.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BasketController : BaseController
    {
        private readonly IBasketRepository _basketRepository;
        private readonly DiscountGRPCService _discountGRPCService;
        public BasketController(IBasketRepository basketRepository, DiscountGRPCService discountGRPCService)
        {
            _basketRepository = basketRepository;
            _discountGRPCService = discountGRPCService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetBasket(string userName)
        {
            try
            {
                var response = await _basketRepository.GetShoppingCart(userName);
                return CustomResult("Success", response);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateBasket([FromBody] ShoppingCart shoppingCart)
        {
            try
            {
                foreach (var item in shoppingCart.Items)
                {
                    try
                    {
                        var product = await _discountGRPCService.GetCoupon(item.ProductId);
                        if (product != null)
                        {
                            item.Price -= product.Amount;
                        }
                    }
                    catch (Exception)
                    {

                    }
                }

                var response = await _basketRepository.UpdateShoppingCart(shoppingCart);
                return CustomResult("Success", response);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpDelete]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteBasket(string userName)
        {
            try
            {
                await _basketRepository.DeleteShoppingCart(userName);
                return CustomResult("Success", true);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }
    }
}
