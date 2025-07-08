using Domain.Requests;
using Domain.Responses;

namespace StiktifyShopBackend.Interfaces
{
    public interface ICategorySizeProvider
    {
        IQueryable<ResponseCategorySize> GetAll();
        IQueryable<ResponseCategorySize> GetAllOfCategory(string categoryId);
        IQueryable<ResponseCategorySize> GetAllOfProduct(string productId);
        IQueryable<ResponseCategorySize> GetAllOfProductOption(string optionId);
        Task<ResponseCategorySize?> GetOne(string sizeId);
        Task<Response> AddCategorySize(RequestCreateCategorySize requestCreate);
        Task<Response> UpdateCategorySize(RequestUpdateCategorySize requestUpdate);
        Task<Response> DeleteCategorySize(string sizeId);
    }
}
