using StiktifyShop.Application.DTOs.Requests;
using StiktifyShop.Application.DTOs.Responses;
using StiktifyShop.Domain.Entity;

namespace StiktifyShop.Application.Mapper
{
    public class MapperPaymentRefund
    {
        public PaymentRefund MapCreate(CreatePaymentRefund paymentRefund)
        {
            return new PaymentRefund
            {
                Amount = paymentRefund.Amount,
                Reason = paymentRefund.Reason,
                RefundAt = paymentRefund.RefundAt,
                Id = paymentRefund.Id,
            };
        }
        public ResponsePaymentRefund MapResponse(PaymentRefund paymentRefund)
        {
            return new ResponsePaymentRefund
            {
                Id = paymentRefund.Id,
                Amount = paymentRefund.Amount,
                Reason = paymentRefund.Reason,
                RefundAt = paymentRefund.RefundAt,
                Payment = new ResponsePayment
                {
                    Id = paymentRefund.Payment.Id,
                    Amount = paymentRefund.Payment.Amount,
                    PaidAt = paymentRefund.Payment.PaidAt,
                    MethodId = paymentRefund.Payment.MethodId,
                    TxnRef = paymentRefund.Payment.TxnRef,
                    UserId = paymentRefund.Payment.UserId,
                    OrderId = paymentRefund.Payment.OrderId,
                    Status = paymentRefund.Payment.Status,
                },
                CreateAt = paymentRefund.CreatedAt,
                UpdateAt = paymentRefund.UpdatedAt
            };
        }
    }
}

