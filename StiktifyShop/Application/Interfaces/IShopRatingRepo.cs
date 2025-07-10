using StiktifyShop.Application.DTOs.Requests;
using StiktifyShop.Application.DTOs.Responses;

namespace StiktifyShop.Application.Interfaces
{
    public interface IShopRatingRepo
    {
        IQueryable<ResponseShopRating> GetAll(string shopId);
        Task<ResponseShopRating?> Get(string shopId, string userId);
        Task<Response> Create(CreateShopRating shopRating);
        Task<Response> Delete(string shopId, string userId);
    }
}
