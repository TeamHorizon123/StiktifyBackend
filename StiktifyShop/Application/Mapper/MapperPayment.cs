using StiktifyShop.Application.DTOs.Requests;
using StiktifyShop.Application.DTOs.Responses;
using StiktifyShop.Domain.Entity;

namespace StiktifyShop.Application.Mapper
{
    public class MapperPayment
    {
        public Payment MapCreate(CreatePayment payment)
        {
            return new Payment
            {
                Amount = payment.Amount,
                PaidAt = payment.PaidAt,
                MethodId = payment.MethodId,
                TxnRef = payment.TxnRef,
                UserId = payment.UserId,
                OrderId = payment.OrderId,
                Status = payment.Status,
            };
        }

        public ResponsePayment MapResponse(Payment payment)
        {
            return new ResponsePayment
            {
                Id = payment.Id,
                Amount = payment.Amount,
                PaidAt = payment.PaidAt,
                MethodId = payment.MethodId,
                TxnRef = payment.TxnRef,
                UserId = payment.UserId,
                OrderId = payment.OrderId,
                Status = payment.Status,
                Order = new ResponseOrder
                {
                    Id = payment.Order.Id,
                    UserId = payment.Order.UserId,
                    Price = payment.Order.Price,
                    Quantity = payment.Order.Quantity,
                    ShippingFee = payment.Order.ShippingFee,
                    ProductId = payment.Order.ProductId,
                    ProductItemId = payment.Order.ProductItemId,
                    Status = payment.Order.Status
                },
                CreateAt = payment.CreatedAt,
                UpdateAt = payment.UpdatedAt
            };
        }
    }
}
