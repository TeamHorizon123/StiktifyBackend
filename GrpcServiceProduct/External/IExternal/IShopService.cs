using Domain.Responses;

namespace GrpcServiceProduct.External.IExternal
{
    public interface IShopService
    {
        Task<ResponseShop> GetOne(string shopId);
    }
}
