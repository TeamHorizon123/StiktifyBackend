using Domain.Requests;
using Domain.Responses;

namespace GrpcServiceProduct.Interfaces
{
    public interface IProductItemRepository
    {
        Task<ResponseProductItem?> GetProductItem(string productId);
        Task<IEnumerable<ResponseProductItem>> GetAllProductItems();
        Task<IEnumerable<ResponseProductItem>> GetAllOfProduct(string productId);
        Task<Response> AddProductItem(RequestCreateProductItem productItem);
        Task<Response> UpdateProductItem(RequestUpdateProductItem productItem);
        Task<Response> DeleteProductItem(string productId);
    }
}
