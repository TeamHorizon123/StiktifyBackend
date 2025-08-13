using StiktifyShop.Application.DTOs.Requests;
using StiktifyShop.Application.DTOs.Responses;

namespace StiktifyShop.Application.Interfaces
{
    public interface IPaymentRepo
    {
        IQueryable<ResponsePayment> GetAll();
        Task<Response> Create(CreatePayment createPayment);
        Task<Response> Update(UpdatePayment updatePayment);
    }
}
