using Discount.GRPC.Protos;

namespace Basket.API.GRPCServices
{
    public class DiscountGRPCService
    {
        private readonly DiscountProtoService.DiscountProtoServiceClient _discountProtoServiceClient;
        public DiscountGRPCService(DiscountProtoService.DiscountProtoServiceClient discountProtoServiceClient)
        {
            _discountProtoServiceClient = discountProtoServiceClient;
        }
        public async Task<CouponRequest> GetCoupon(string productId)
        {
            var coupon = new GetDiscountRequest { ProductId = productId };
            return await _discountProtoServiceClient.GetDiscountAsync(coupon);
        }
    }
}
