using Domain.Requests;
using Domain.Responses;

namespace StiktifyShopBackend.Interfaces
{
    public interface IProductItemProvider
    {
        IQueryable<ResponseProductItem> GetAll();
        IQueryable<ResponseProductItem> GetAllOfProduct(string productId);
        Task<ResponseProductItem?> GetOne(string itemId);
        Task<Response> AddProductItem(RequestCreateProductItem requestCreate);
        Task<Response> UpdateProductItem(RequestUpdateProductItem requestUpdate);
        Task<Response> DeleteProductItem(string itemId);
    }
}
