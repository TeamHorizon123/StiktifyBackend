using Domain.Requests;
using Domain.Responses;

namespace GrpcServiceProduct.Interfaces
{
    public interface IProductVarriantRepository
    {
        Task<Response> CreateProductVarriant(RequestCreateProductVarriant createProductVarriant);
        Task<Response> DeleteProductVarriant(string productVarriantId);
        Task<Response> UpdateProductVarriant(RequestUpdateProductVarriant updateProductVarriant);
        Task<ResponseProductVarriant?> GetProductVarriant(string optionId, string sizeId);
        Task<IEnumerable<ResponseProductVarriant>> GetAllProductVarriants();
        Task<IEnumerable<ResponseProductVarriant>> GetAllOfProduct(string productId);
        Task<IEnumerable<ResponseProductVarriant>> GetAllOfProductOption(string productOptionId);
    }
}
