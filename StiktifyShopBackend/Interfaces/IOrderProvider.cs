using Domain.Requests;
using Domain.Responses;

namespace StiktifyShopBackend.Interfaces
{
    public interface IOrderProvider
    {
        IQueryable<ResponseOrder> GetAll();
        IQueryable<ResponseOrder> GetAllOfShop(string shopId);
        IQueryable<ResponseOrder> GetAllOfUser(string userId);
        IQueryable<ResponseOrder> GetAllOfProduct(string productId);
        Task<ResponseOrder?> GetOne(string orderId);
        Task<Response> CreateOrder(RequestCreateOrder createOrder);
        Task<Response> UpdateOrder(RequestUpdateOrder updateOrder);
    }
}
