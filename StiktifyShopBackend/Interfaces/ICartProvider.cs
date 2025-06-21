using Domain.Requests;
using Domain.Responses;

namespace StiktifyShopBackend.Interfaces
{
    public interface ICartProvider
    {
        IQueryable<ResponseProductRating> GetAll();
        IQueryable<ResponseProductRating> GetAllOfProduct(string productId);
        IQueryable<ResponseProductRating> GetAllOfOption(string optionId);
        Task<ResponseProductRating?> GetOne(string ratingId);
        Task<Response> CreateRating(RequestCreateRating createRating);
        Task<Response> UpdateRating(RequestUpdateRating updateRating);
        Task<Response> DeleteRating(string productId);
    }
}
