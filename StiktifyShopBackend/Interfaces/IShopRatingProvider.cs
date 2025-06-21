using Domain.Requests;
using Domain.Responses;

namespace StiktifyShopBackend.Interfaces
{
    public interface IShopRatingProvider
    {
        IQueryable<ResponseShopRating> GetAllOfShop(string shopId);
        Task<ResponseShopRating?> GetOne(string ratingId);
        Task<Response> CreateShopRating(RequestCreateShopRating createShopRating);
        Task<Response> UpdateShopRating(RequestUpdateShopRating updateShopRating);
        Task<Response> DeleteShopRating(string shopId);
    }
}
