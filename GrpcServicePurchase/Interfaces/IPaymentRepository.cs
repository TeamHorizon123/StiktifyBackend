using Domain.Requests;
using Domain.Responses;

namespace GrpcServicePurchase.Interfaces
{
    public interface IPaymentRepository
    {
        Task<IEnumerable<ResponsePayment>> GetAll();
        Task<IEnumerable<ResponsePayment>> GetAllOfUser(string userId);
        Task<IEnumerable<ResponsePayment>> GetAllOfProduct(string productId);
        Task<ResponsePayment?> GetOne(string paymentId);
        Task<Response> Create(RequestCreatePayment createPayment);
        Task<Response> Update(RequestUpdatePayment updatePayment);
    }
}
