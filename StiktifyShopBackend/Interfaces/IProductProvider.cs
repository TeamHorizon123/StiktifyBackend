using Domain.Requests;
using Domain.Responses;

namespace StiktifyShopBackend.Interfaces
{
    public interface IProductProvider
    {
        IQueryable<ResponseProduct> GetAll();
        IQueryable<ResponseProduct> GetAllOfShop(string shopId);
        IQueryable<ResponseProduct> GetAllOfCategory(string categoryId);
        Task<IEnumerable<string>> GetAllImage(string productId);
        Task<ResponseProduct?> GetOne(string productId);
        Task<Response> Create(RequestCreateProduct createProduct);
        Task<Response> Update(RequestUpdateProduct updateProduct);
        Task<Response> Delete(string productId);
        Task<Response> DeleteAllOfShop(string shopId);
        Task<Response> DeleteMany(ICollection<string> listProductId);
    }
}
