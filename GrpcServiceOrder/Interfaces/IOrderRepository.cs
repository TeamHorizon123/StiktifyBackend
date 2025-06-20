using Domain.Requests;
using Domain.Responses;

namespace GrpcServiceOrder.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<ResponseOrder>> GetAll();
        Task<IEnumerable<ResponseOrder>> GetAllOfShop(string shopId);
        Task<IEnumerable<ResponseOrder>> GetAllOfUser(string userId);
        Task<IEnumerable<ResponseOrder>> GetAllOfProduct(string productId);
        Task<ResponseOrder?> GetOne(string orderId);
        Task<Response> CreateOrder(RequestCreateOrder createOrder);
        Task<Response> UpdateOrder(RequestUpdateOrder updateOrder);
    }
}
