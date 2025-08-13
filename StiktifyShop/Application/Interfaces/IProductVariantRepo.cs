using StiktifyShop.Application.DTOs.Requests;
using StiktifyShop.Application.DTOs.Responses;

namespace StiktifyShop.Application.Interfaces
{
    public interface IProductVariantRepo
    {
        IQueryable<ResponseProductVariant> GetAll();
        Task<ResponseProductVariant?> Get(string variantId);
        Task<Response> Create(CreateProductVariant variant);
        Task<Response> Update(UpdateProductVariant variant);
        Task<Response> Delete(string variantId);
    }
}
