using Domain.Requests;
using Domain.Responses;

namespace GrpcServiceProduct.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<ResponseProduct>> GetAll();
        Task<IEnumerable<ResponseProduct>> GetAllOfShop(string shopId);
        Task<IEnumerable<ResponseProduct>> GetAllOfCategory(string categoryId);
        Task<IEnumerable<string>> GetAllImage(string productId);
        Task<ResponseProduct?> GetOne(string productId);
        Task<Response> Create(RequestCreateProduct createProduct);
        Task<Response> Update(RequestUpdateProduct updateProduct);
        Task<Response> Delete(string productId);
        Task<Response> DeleteMany(string[] listProductId);
    }
}
