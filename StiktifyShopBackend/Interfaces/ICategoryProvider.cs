using Domain.Requests;
using Domain.Responses;

namespace StiktifyShopBackend.Interfaces
{
    public interface ICategoryProvider
    {
        IQueryable<ResponseCategory> GetAll();
        IQueryable<ResponseCategory> GetAllOfProduct(string productId);
        IQueryable<ResponseCategory> GetAllChildren(string parentId);
        Task<ResponseCategory?> GetOne(string categoryId);
        Task<Response> CreateCategory(RequestCreateCategory createCategory);
        Task<Response> UpdateCategory(RequestUpdateCategory updateCategory);
        Task<Response> DeleteCategory(string categoryId);
        Task<Response> DeleteMany(string[] listCategoryId);
    }
}
