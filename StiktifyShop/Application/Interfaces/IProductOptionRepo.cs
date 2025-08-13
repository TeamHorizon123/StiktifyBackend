using StiktifyShop.Application.DTOs.Requests;
using StiktifyShop.Application.DTOs.Responses;

namespace StiktifyShop.Application.Interfaces
{
    public interface IProductOptionRepo
    {
        Task<Response> Create(CreateProductOption productOption);
        Task<Response> Update(UpdateProductOption productOption);
        Task<Response> Delete(string optionId);
        Task<ResponseProductOption?> Get(string optionId);
        IQueryable<ResponseProductOption> GetAll();
    }
}
