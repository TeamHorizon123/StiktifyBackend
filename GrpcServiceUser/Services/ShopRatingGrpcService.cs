using Domain.Requests;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcServiceUser.Interface;
using GrpcServiceUser.ShopRating;

namespace GrpcServiceUser.Services
{
    public class ShopRatingGrpcService : ShopRatingGrpc.ShopRatingGrpcBase
    {
        private IShopRatingRepository _repo;

        public ShopRatingGrpcService(IShopRatingRepository repo)
        {
            _repo = repo ?? throw new ArgumentException(nameof(_repo));
        }

        public override async Task<ShopRatings> GetAllOfShop(Id request, ServerCallContext context)
        {
            var listRating = await _repo.GetAllOfShop(request.SearchId);
            ShopRating.ShopRatings ratings = new ShopRating.ShopRatings();
            ratings.Item.AddRange(listRating.Select(r =>
            new ShopRating.ShopRating
            {
                Id = r.Id,
                Content = r.Content,
                UserId = r.UserId,
                ShopId = r.ShopId,
                Point = r.Point,
                CreateAt = Timestamp.FromDateTime(r.CreateAt!.Value.ToUniversalTime()),
                UpdateAt = Timestamp.FromDateTime(r.UpdateAt!.Value.ToUniversalTime())
            }));
            return ratings;
        }

        public override async Task<ShopRating.ShopRating> GetOne(Id request, ServerCallContext context)
        {
            var rating = await _repo.GetOne(request.SearchId);
            if (rating == null)
                return new ShopRating.ShopRating();
            return new ShopRating.ShopRating
            {
                Id = rating.Id,
                UserId = rating.UserId,
                ShopId = rating.ShopId,
                Point = rating.Point,
                Content = rating.Content,
                CreateAt = Timestamp.FromDateTime(rating.CreateAt!.Value.ToUniversalTime()),
                UpdateAt = Timestamp.FromDateTime(rating.UpdateAt!.Value.ToUniversalTime())
            };
        }

        public override async Task<Response> Create(CreateRating request, ServerCallContext context)
        {
            var createRating = new RequestCreateShopRating
            {
                UserId = request.UserId,
                ShopId = request.ShopId,
                Content = request.Content,
                Point = request.Point,
            };
            var response = await _repo.CreateShopRating(createRating);
            return new Response { Message = response.Message, StatusCode = response.StatusCode };
        }

        public override async Task<Response> Update(ShopRating.ShopRating request, ServerCallContext context)
        {
            var updateRating = new RequestUpdateShopRating
            {
                Id = request.Id,
                UserId = request.UserId,
                ShopId = request.ShopId,
                Content = request.Content,
                Point = request.Point,
            };
            var response = await _repo.UpdateShopRating(updateRating);
            return new Response { Message = response.Message, StatusCode = response.StatusCode };
        }

        public override async Task<Response> Delete(Id request, ServerCallContext context)
        {
            var response = await _repo.DeleteShopRating(request.SearchId);
            return new Response { Message = response.Message, StatusCode = response.StatusCode };
        }
    }
}
