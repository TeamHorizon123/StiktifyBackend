using StiktifyShop.Application.DTOs.Requests;
using StiktifyShop.Application.DTOs.Responses;

namespace StiktifyShop.Application.Interfaces
{
    public interface ICategoryRepo
    {
        IQueryable<ResponseCategory> GetAll();
        Task<ResponseCategory?> GetById(string categoryId);
        Task<Response> Create(CreateCategory category);
        Task<Response> Update(UpdateCategory category);
        Task<Response> Delete(string categoryId);
    }
}
