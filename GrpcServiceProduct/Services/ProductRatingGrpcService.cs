using Domain.Requests;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcServiceProduct.Interfaces;
using GrpcServiceProduct.ProductRating;
using Newtonsoft.Json;

namespace GrpcServiceProduct.Services
{
    public class ProductRatingGrpcService : ProductRatingGrpc.ProductRatingGrpcBase
    {
        private IProductRatingRepository _repo;

        public ProductRatingGrpcService(IProductRatingRepository repo)
        {
            _repo = repo ?? throw new ArgumentException(nameof(_repo));
        }

        public override async Task<Ratings> GetAll(ProductRating.Empty request, ServerCallContext context)
        {
            var listRatings = await _repo.GetAll();
            Ratings grpcRatings = new Ratings();
            grpcRatings.Item.AddRange(listRatings.Select(rating
                => new Rating
                {
                    Id = rating.Id,
                    UserId = rating.UserId,
                    ProductId = rating.ProductId,
                    OptionId = rating.OptionId,
                    Point = rating.Point,
                    Content = rating.Content,
                    ImageList = JsonConvert.SerializeObject(rating.Point),
                    CreateAt = Timestamp.FromDateTime(rating.CreateAt!.Value.ToUniversalTime()),
                    UpdateAt = Timestamp.FromDateTime(rating.UpdateAt!.Value.ToUniversalTime()),
                }));
            return grpcRatings;
        }

        public override async Task<Ratings> GetAllOfOption(Id request, ServerCallContext context)
        {
            var listRatings = await _repo.GetAllOfOption(request.SearchId);
            Ratings grpcRatings = new Ratings();
            grpcRatings.Item.AddRange(listRatings.Select(rating
                => new Rating
                {
                    Id = rating.Id,
                    UserId = rating.UserId,
                    ProductId = rating.ProductId,
                    OptionId = rating.OptionId,
                    Point = rating.Point,
                    Content = rating.Content,
                    ImageList = JsonConvert.SerializeObject(rating.Point),
                    CreateAt = Timestamp.FromDateTime(rating.CreateAt!.Value.ToUniversalTime()),
                    UpdateAt = Timestamp.FromDateTime(rating.UpdateAt!.Value.ToUniversalTime()),
                }));
            return grpcRatings;
        }

        public override async Task<Ratings> GetAllOfProduct(Id request, ServerCallContext context)
        {
            var listRatings = await _repo.GetAllOfProduct(request.SearchId);
            Ratings grpcRatings = new Ratings();
            grpcRatings.Item.AddRange(listRatings.Select(rating
                => new Rating
                {
                    Id = rating.Id,
                    UserId = rating.UserId,
                    ProductId = rating.ProductId,
                    OptionId = rating.OptionId,
                    Point = rating.Point,
                    Content = rating.Content,
                    ImageList = JsonConvert.SerializeObject(rating.Point),
                    CreateAt = Timestamp.FromDateTime(rating.CreateAt!.Value.ToUniversalTime()),
                    UpdateAt = Timestamp.FromDateTime(rating.UpdateAt!.Value.ToUniversalTime()),
                }));
            return grpcRatings;
        }

        public override async Task<Rating> GetOne(Id request, ServerCallContext context)
        {
            var rating = await _repo.GetOne(request.SearchId);
            if (rating == null)
                return new Rating();
            return new Rating
            {
                Id = rating.Id,
                UserId = rating.UserId,
                ProductId = rating.ProductId,
                OptionId = rating.OptionId,
                Point = rating.Point,
                Content = rating.Content,
                ImageList = JsonConvert.SerializeObject(rating.Image),
                CreateAt = Timestamp.FromDateTime(rating.CreateAt!.Value.ToUniversalTime()),
                UpdateAt = Timestamp.FromDateTime(rating.UpdateAt!.Value.ToUniversalTime()),
            };
        }

        public override async Task<Response> Create(CreateRating request, ServerCallContext context)
        {
            var createRating = new RequestCreateRating
            {
                UserId = request.UserId,
                OptionId = request.OptionId,
                ProductId = request.ProductId,
                Content = request.Content,
                Point = request.Point,
                Image = JsonConvert.DeserializeObject<ICollection<string>>(request.ImageList)
            };
            var response = await _repo.CreateRating(createRating);
            return new Response { Message = response.Message, StatusCode = response.StatusCode };
        }

        public override async Task<Response> Update(Rating request, ServerCallContext context)
        {
            var updateRating = new RequestUpdateRating
            {
                Id = request.UserId,
                ProductId = request.ProductId,
                OptionId = request.OptionId,
                UserId = request.UserId,
                Content = request.Content,
                Image = JsonConvert.DeserializeObject<ICollection<string>>(request.ImageList),
                Point = request.Point,
            };
            var response = await _repo.UpdateRating(updateRating);
            return new Response { Message = response.Message, StatusCode = response.StatusCode };
        }

        public override async Task<Response> Delete(Id request, ServerCallContext context)
        {
            var response = await _repo.DeleteRating(request.SearchId);
            return new Response { Message = response.Message, StatusCode = response.StatusCode };
        }
    }
}
