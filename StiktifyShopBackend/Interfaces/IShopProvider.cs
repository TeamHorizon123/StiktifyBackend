using Domain.Requests;
using Domain.Responses;

namespace StiktifyShopBackend.Interfaces
{
    public interface IShopProvider
    {
        IQueryable<ResponseShop> GetAll();
        Task<ResponseShop?> GetOne(string shopId);
        Task<ResponseShop?> GetOfUser(string userId);
        Task<Response> CreateShop(RequestCreateShop shop);
        Task<Response> UpdateShop(RequestUpdateShop shop);
        Task<Response> DeleteShop(string shopId);
    }
}
