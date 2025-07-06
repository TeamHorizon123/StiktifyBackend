using Domain.Requests;
using Domain.Responses;

namespace GrpcServiceProduct.Interfaces
{
    public interface ICategorySizeRepository
    {
        Task<IEnumerable<ResponseCategorySize>> GetAll();
        Task<IEnumerable<ResponseCategorySize>> GetAllOfCategory(string categoryId);
        Task<IEnumerable<ResponseCategorySize>> GetAllOfProduct(string productId);
        Task<IEnumerable<ResponseCategorySize>> GetAllOfProductOption(string productId);
        Task<ResponseCategorySize?> GetOne(string categorySizeId);
        Task<Response> CreateCategorySize(RequestCreateCategorySize createCategorySize);
        Task<Response> UpdateCategorySize(RequestUpdateCategorySize updateCategorySize);
        Task<Response> DeleteCategorySize(string categorySizeId);
    }
}
