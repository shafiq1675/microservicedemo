using AutoMapper;
using Discount.GRPC.Models;
using Discount.GRPC.Protos;
using Discount.GRPC.Repositories;
using Grpc.Core;

namespace Discount.GRPC.Services
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        ICouponRepository _couponRepository;
        ILogger<DiscountService> _logger;
        IMapper _mapper;
        public DiscountService(ICouponRepository couponRepository, ILogger<DiscountService> logger, IMapper mapper)
        {
            _couponRepository = couponRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async override Task<CouponRequest> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await _couponRepository.GetDiscount(request.ProductId);
            if (coupon == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Not Found"));
            }
            _logger.LogInformation("Discount is retrived for Product Name:{productName}, Amount: {amount}", coupon.ProductName, coupon.Amount);
            //return new CouponRequest { ProductId = coupon.ProductId, ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount };
            return _mapper.Map<CouponRequest>(coupon);
        }

        public async override Task<CouponRequest> CreateDiscount(CouponRequest request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Coupon>(request);
            bool response = await _couponRepository.CreateDiscount(coupon);
            if (response)
            {
                _logger.LogInformation("Discount is save for Product Name:{productName}, Amount: {amount}", coupon.ProductName, coupon.Amount);
            }
            else
            {
                _logger.LogInformation("Faild");
            }
            return _mapper.Map<CouponRequest>(coupon);

        }

        public override async Task<CouponRequest> UpdateDiscount(CouponRequest request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Coupon>(request);
            bool response = await _couponRepository.UpdateDiscount(coupon);
            if (response)
            {
                _logger.LogInformation("Discount is Updated for Product Name:{productName}, Amount: {amount}", coupon.ProductName, coupon.Amount);
            }
            else
            {
                _logger.LogInformation("Faild");
            }
            return _mapper.Map<CouponRequest>(coupon);
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var response = await _couponRepository.DeleteDiscount(request.ProductId);
            if (response)
            {
                _logger.LogInformation("Discount is Deleted for Product Name:{productName}, Amount: {amount}", coupon.ProductName, coupon.Amount);
            }
            else
            {
                _logger.LogInformation("Faild");
            }
            return new DeleteDiscountResponse { Success = response };
        }
    }
}
