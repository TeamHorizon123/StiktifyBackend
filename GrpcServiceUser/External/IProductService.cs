namespace GrpcServiceUser.External
{
    public interface IProductService
    {
        Task<Product.Response> DeleteAllOfShop(string shopId);
    }
}
