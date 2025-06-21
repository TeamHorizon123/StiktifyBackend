using Domain.Requests;
using Domain.Responses;

namespace StiktifyShopBackend.Interfaces
{
    public interface IPaymentMethodProvider
    {
        IQueryable<ResponsePaymentMethod> GetAll();
        Task<ResponsePaymentMethod?> GetOne(string methodId);
        Task<Response> Create(RequestCreateMethod createMethod);
        Task<Response> Update(RequestUpdateMethod updateMethod);
    }
}
