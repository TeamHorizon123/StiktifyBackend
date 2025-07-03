using GrpcServiceUser.Product;

namespace GrpcServiceUser.External
{
    public class ProductService : IProductService
    {
        private ProductGrpc.ProductGrpcClient _client;

        public ProductService(ProductGrpc.ProductGrpcClient client)
        {
            _client = client;
        }

        public async Task<Response> DeleteAllOfShop(string shopId)
        {
            return await _client.DeleteAllOfShopAsync(new Id { SearchId = shopId });
        }
    }
}
