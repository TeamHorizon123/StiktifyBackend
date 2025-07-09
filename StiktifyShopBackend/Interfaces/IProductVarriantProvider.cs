using Domain.Requests;
using Domain.Responses;

namespace StiktifyShopBackend.Interfaces
{
    public interface IProductVarriantProvider
    {
        IQueryable<ResponseProductVarriant> GetAll();
        IQueryable<ResponseProductVarriant> GetAllOfProduct(string productId);
        IQueryable<ResponseProductVarriant> GetAllOfProductOption(string optionId);
        Task<ResponseProductVarriant?> GetOne(string optionId, string sizeId);
        Task<Response> AddProductVarriant(RequestCreateProductVarriant requestCreate);
        Task<Response> UpdateProductVarriant(RequestCreateProductVarriant requestUpdate);
        Task<Response> DeleteProductVarriant(string optionId, string sizeId);
    }
}
