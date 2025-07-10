using StiktifyShop.Application.DTOs.Requests;
using StiktifyShop.Application.DTOs.Responses;
using StiktifyShop.Application.Interfaces;

namespace StiktifyShop.Infrastructure.Repository
{
    public class ShopRatingRepo : IShopRatingRepo
    {
        public Task<Response> Create(CreateShopRating shopRating)
        {
            throw new NotImplementedException();
        }

        public Task<Response> Delete(string shopId, string userId)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseShopRating?> Get(string shopId, string userId)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ResponseShopRating> GetAll(string shopId)
        {
            throw new NotImplementedException();
        }
    }
}
