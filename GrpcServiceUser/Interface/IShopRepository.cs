using Domain.Responses;

namespace GrpcServiceUser.Interface
{
    public interface IShopRepository
    {
        Task<IEnumerable<ResponseShop>> GetAll();
        Task<ResponseShop?> GetOne(string shopId);
        Task<ResponseShop?> GetOfUser(string userId);
        Task<Response> CreateShop(Domain.Requests.RequestCreateShop shop);
        Task<Response> UpdateShop(Domain.Requests.RequestUpdateShop shop);
        Task<Response> DeleteShop(string shopId);

    }
}
