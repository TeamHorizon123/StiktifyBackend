using StiktifyShop.Application.DTOs.Requests;
using StiktifyShop.Application.DTOs.Responses;
using StiktifyShop.Domain.Entity;

namespace StiktifyShop.Application.Mapper
{
    public class MapperPaymentMethod
    {
        public PaymentMethod MapCreate(CreatePaymentMethod paymentMethod)
        {
            return new PaymentMethod
            {
                Name = paymentMethod.Name,
                Enable = paymentMethod.Enable,
            };
        }

        public ResponsePaymentMethod MapResponse(PaymentMethod paymentMethod)
        {
            return new ResponsePaymentMethod
            {
                Id = paymentMethod.Id,
                Name = paymentMethod.Name,
                Enable = paymentMethod.Enable,
                CreateAt = paymentMethod.CreatedAt,
                UpdateAt = paymentMethod.UpdatedAt
            };
        }
    }
}
