using StiktifyShop.Application.DTOs.Requests;
using StiktifyShop.Application.DTOs.Responses;

namespace StiktifyShop.Application.Interfaces
{
    public interface IPaymentMethodRepo
    {
        Task<Response> Create(CreatePaymentMethod paymentMethod);
        Task<Response> Update(UpdatePaymentMethod paymentMethod);
        Task<Response> Delete(string id);
        Task<ResponsePaymentMethod?> Get(string id);
        IQueryable<ResponsePaymentMethod> GetAll();
    }
}
