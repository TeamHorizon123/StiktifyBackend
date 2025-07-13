using StiktifyShop.Application.DTOs.Requests;
using StiktifyShop.Application.DTOs.Responses;

namespace StiktifyShop.Application.Interfaces
{
    public interface IProductSizeRepo
    {
        IQueryable<ResponseProductSize> GetAll();
        Task<ResponseProductSize?> Get(string variantId);
        Task<Response> Create(CreateProductSize productSize);
        Task<Response> Update(UpdateProductSize productSize);
        Task<Response> Delete(string variantId);
    }
}
