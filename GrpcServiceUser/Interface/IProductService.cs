namespace GrpcServiceUser.Interface
{
    public interface IProductService
    {
        Task<Product.Response> DeleteAllOfShop(string shopId);
    }
}
