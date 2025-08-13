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
                PaymentMethod = new ResponsePaymentMethod
                {
                    Id = payment.PaymentMethod.Id,
                    Name = payment.PaymentMethod.Name,
                    CreateAt = payment.PaymentMethod.CreatedAt,
                    UpdateAt = payment.PaymentMethod.UpdatedAt
                },
                CreateAt = payment.CreatedAt,
                UpdateAt = payment.UpdatedAt
            };
        }
    }
}
