using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services
{
    public class DiscountService(DiscountContext Context, ILogger<DiscountService> Logger) : DiscountProtoService.DiscountProtoServiceBase
    {
        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await Context.Coupons.FirstOrDefaultAsync(x => x.ProductName == request.ProductName) 
                ?? new Coupon { ProductName = "No discount", Amount =0, Description="Nothing" };
            return coupon.Adapt<CouponModel>();
        }

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>() ?? throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request"));
            Context.Coupons.Add(coupon);
            await Context.SaveChangesAsync();

            return coupon.Adapt<CouponModel>();
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>() ?? throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request"));
            Context.Coupons.Update(coupon);
            await Context.SaveChangesAsync();

            return coupon.Adapt<CouponModel>();
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.ProductName.Adapt<string>() ?? throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request"));
            var existingCoupon = await Context.Coupons.FirstOrDefaultAsync(x => x.ProductName == coupon);

            if (existingCoupon != null)
                Context.Coupons.Remove(existingCoupon);
            else
                throw new RpcException(new Status(StatusCode.NotFound, $"Dicsount code:{coupon} is not found"));

            await Context.SaveChangesAsync();

            return new DeleteDiscountResponse { Success = true };
        }
    }
}
