using CoreApiResponse;
using Discount.API.Models;
using Discount.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Net;

namespace Discount.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : BaseController
    {
        private readonly ICouponRepository _couponRepository;
        public DiscountController(ICouponRepository couponRepository)
        {
            _couponRepository = couponRepository;
        }

        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateDiscount([FromBody] Coupon coupon)
        {
            try
            {
                var response = await _couponRepository.CreateDiscount(coupon);
                if (response)
                {
                    return CustomResult("Success", HttpStatusCode.OK);
                }
                return CustomResult("faild", HttpStatusCode.NotFound);

            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.OK);
            }
        }

        [HttpDelete("productId")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteDiscount(string productId)
        {
            try
            {
                var response = await _couponRepository.DeleteDiscount(productId);
                if (response)
                {
                    return CustomResult("Success", response, HttpStatusCode.OK);
                }
                return CustomResult("faild", HttpStatusCode.NotFound);

            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.OK);
            }
        }

        [HttpGet("productId")]
        [ProducesResponseType(typeof(IEnumerable<Coupon>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetDiscount(string productId)
        {
            try
            {
                var response = await _couponRepository.GetDiscount(productId);
                if (response is not null)
                {
                    return CustomResult("Success", response, HttpStatusCode.OK);
                }
                return CustomResult("faild", HttpStatusCode.NotFound);

            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.OK);
            }
        }

        [HttpPut]
        [ProducesResponseType(typeof(IEnumerable<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateDiscount([FromBody] Coupon coupon)
        {
            try
            {
                var response = await _couponRepository.UpdateDiscount(coupon);
                if (response)
                {
                    return CustomResult("Success", response, HttpStatusCode.OK);
                }
                return CustomResult("faild", HttpStatusCode.NotFound);

            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.OK);
            }
        }
    }
}
