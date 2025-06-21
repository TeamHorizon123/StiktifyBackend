using Domain.Requests;
using Domain.Responses;

namespace GrpcServicePurchase.Interfaces
{
    public interface IPaymentMethodRepository
    {
        Task<IEnumerable<ResponsePaymentMethod>> GetAll();
        Task<ResponsePaymentMethod?> GetOne(string methodId);
        Task<Response> Create(RequestCreateMethod createMethod);
        Task<Response> Update(RequestUpdateMethod updateMethod);
    }
}
