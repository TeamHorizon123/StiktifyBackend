using Domain.Requests;
using Domain.Responses;

namespace StiktifyShopBackend.Interfaces
{
    public interface IPaymentProvider
    {
        IQueryable<ResponsePayment> GetAll();
        IQueryable<ResponsePayment> GetAllOfUser(string userId);
        IQueryable<ResponsePayment> GetAllOfProduct(string productId);
        Task<ResponsePayment?> GetOne(string paymentId);
        Task<Response> Create(RequestCreatePayment createPayment);
        Task<Response> Update(RequestUpdatePayment updatePayment);
    }
}
