using Domain.Responses;
using GrpcServiceProduct.External.IExternal;

namespace GrpcServiceProduct.External
{
    public class ShopService : IShopService
    {
        public Task<ResponseShop> GetOne(string shopId)
        {
            throw new NotImplementedException();
        }
    }
}
