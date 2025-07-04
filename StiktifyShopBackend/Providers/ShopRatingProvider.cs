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

        public async Task<Domain.Responses.Response> CreateShopRating(RequestCreateShopRating createShopRating)
        {
            var createGrpc = new CreateRating
            {
                UserId = createShopRating.UserId,
                ShopId = createShopRating.ShopId,
                Content = createShopRating.Content,
                Point = createShopRating.Point,
            };
            var response = await _client.CreateAsync(createGrpc);
            return new Domain.Responses.Response { StatusCode = response.StatusCode, Message = response.Message };
        }

        public async Task<Domain.Responses.Response> DeleteShopRating(string shopId)
        {
            var response = await _client.DeleteAsync(new Id { SearchId = shopId });
            return new Domain.Responses.Response { StatusCode = response.StatusCode, Message = response.Message };
        }

        public IQueryable<ResponseShopRating> GetAllOfShop(string shopId)
        {
            var grpcList = _client.GetAllOfShop(new Id { SearchId = shopId });
            IEnumerable<ResponseShopRating> list = grpcList.Item.Select(
                rating => new ResponseShopRating
                {
                    Id = rating.Id,
                    Content = rating.Content,
                    Point = rating.Point,
                    ShopId = shopId,
                    UserId = rating.UserId,
                    CreateAt = rating.CreateAt.ToDateTime(),
                    UpdateAt = rating.UpdateAt.ToDateTime(),
                });
            return list.AsQueryable();
        }

        public async Task<ResponseShopRating?> GetOne(string ratingId)
        {
            var grpcRating = await _client.GetOneAsync(new Id { SearchId = ratingId });
            return new ResponseShopRating
            {
                Id = grpcRating.Id,
                Content = grpcRating.Content,
                Point = grpcRating.Point,
                ShopId = grpcRating.ShopId,
                UserId = grpcRating.UserId,
                CreateAt = grpcRating.CreateAt.ToDateTime(),
                UpdateAt = grpcRating.UpdateAt.ToDateTime(),
            };
        }

        public async Task<Domain.Responses.Response> UpdateShopRating(RequestUpdateShopRating updateShopRating)
        {
            var updateGrpc = new ShopRating.ShopRating
            {
                Id = updateShopRating.Id,
                UserId = updateShopRating.UserId,
                ShopId = updateShopRating.ShopId,
                Point = updateShopRating.Point,
                Content = updateShopRating.Content
            };
            var response = await _client.UpdateAsync(updateGrpc);
            return new Domain.Responses.Response { StatusCode = response.StatusCode, Message = response.Message };
        }
    }
}
