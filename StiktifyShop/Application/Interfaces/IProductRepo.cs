using StiktifyShop.Application.DTOs.Requests;
using StiktifyShop.Application.DTOs.Responses;

namespace StiktifyShop.Application.Interfaces
{
    public interface IProductRepo
    {
        IQueryable<ResponseProduct> GetAll();
        Task<ResponseProduct?> Get(string productId);
        Task<Response> Create(CreateProduct product);
        Task<Response> Update(UpdateProduct product);
        Task<Response> Delete(string productId);
    }
}
