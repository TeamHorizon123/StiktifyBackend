using Domain.Requests;
using Domain.Responses;

namespace GrpcServiceProduct.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<ResponseCategory>> GetAll();
        Task<IEnumerable<ResponseCategory>> GetAllOfProduct(string productId);
        Task<IEnumerable<ResponseCategory>> GetAllChildren(string parentId);
        Task<ResponseCategory?> GetOne(string categoryId);
        Task<Response> CreateCategory(RequestCreateCategory createCategory);
        Task<Response> UpdateCategory(RequestUpdateCategory updateCategory);
        Task<Response> DeleteCategory(string categoryId);
        Task<Response> DeleteMany(string[] listCategoryId);   
    }
}
