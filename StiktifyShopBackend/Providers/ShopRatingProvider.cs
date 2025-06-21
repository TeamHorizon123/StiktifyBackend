using Domain.Requests;
using Domain.Responses;
using StiktifyShopBackend.Interfaces;
using StiktifyShopBackend.ShopRating;

namespace StiktifyShopBackend.Providers
{
    public class ShopRatingProvider : IShopRatingProvider
    {
        private ShopRatingGrpc.ShopRatingGrpcClient _client;

        public ShopRatingProvider(ShopRatingGrpc.ShopRatingGrpcClient client)
        {
            _client = client ?? throw new ArgumentException(nameof(_client));
        }

        public Task<Domain.Responses.Response> CreateShopRating(RequestCreateShopRating createShopRating)
        {
            throw new NotImplementedException();
        }

        public Task<Domain.Responses.Response> DeleteShopRating(string shopId)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ResponseShopRating> GetAllOfShop(string shopId)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseShopRating?> GetOne(string ratingId)
        {
            throw new NotImplementedException();
        }

        public Task<Domain.Responses.Response> UpdateShopRating(RequestUpdateShopRating updateShopRating)
        {
            throw new NotImplementedException();
        }
    }
}
