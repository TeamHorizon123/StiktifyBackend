using Domain.Entities;
using Domain.Requests;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcServiceProduct.Interfaces;
using GrpcServiceProduct.Product;
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
                    ProductItemId = rating.ProductItemId,
                    Point = rating.Point,
                    Content = rating.Content,
                    ImageList = JsonConvert.SerializeObject(rating.Image),
                    CreateAt = Timestamp.FromDateTime(rating.CreateAt!.Value.ToUniversalTime()),
                    UpdateAt = Timestamp.FromDateTime(rating.UpdateAt!.Value.ToUniversalTime()),
                }));
            return grpcRatings;
        }

        public override async Task<Ratings> GetAllOfOption(ProductRating.Id request, ServerCallContext context)
        {
            var listRatings = await _repo.GetAllOfOption(request.SearchId);
            Ratings grpcRatings = new Ratings();
            grpcRatings.Item.AddRange(listRatings.Select(rating
                => new Rating
                {
                    Id = rating.Id,
                    UserId = rating.UserId,
                    ProductId = rating.ProductId,
                    ProductItemId = rating.ProductItemId,
                    Point = rating.Point,
                    Content = rating.Content,
                    ImageList = JsonConvert.SerializeObject(rating.Point),
                    CreateAt = Timestamp.FromDateTime(rating.CreateAt!.Value.ToUniversalTime()),
                    UpdateAt = Timestamp.FromDateTime(rating.UpdateAt!.Value.ToUniversalTime()),
                }));
            return grpcRatings;
        }

        public override async Task<Ratings> GetAllOfProduct(ProductRating.Id request, ServerCallContext context)
        {
            var listRatings = await _repo.GetAllOfProduct(request.SearchId);
            Ratings grpcRatings = new Ratings();
            grpcRatings.Item.AddRange(listRatings.Select(rating
                => new Rating
                {
                    Id = rating.Id,
                    UserId = rating.UserId,
                    ProductId = rating.ProductId,
                    ProductItemId = rating.ProductItemId,
                    Point = rating.Point,
                    Content = rating.Content,
                    ImageList = JsonConvert.SerializeObject(rating.Point),
                    CreateAt = Timestamp.FromDateTime(rating.CreateAt!.Value.ToUniversalTime()),
                    UpdateAt = Timestamp.FromDateTime(rating.UpdateAt!.Value.ToUniversalTime()),
                }));
            return grpcRatings;
        }

        public override async Task<Rating> GetOne(ProductRating.Id request, ServerCallContext context)
        {
            var rating = await _repo.GetOne(request.SearchId);
            if (rating == null)
                return new Rating();
            return new Rating
            {
                Id = rating.Id,
                UserId = rating.UserId,
                ProductId = rating.ProductId,
                ProductItemId = rating.ProductItemId,
                Point = rating.Point,
                Content = rating.Content,
                ImageList = JsonConvert.SerializeObject(rating.Image),
                CreateAt = Timestamp.FromDateTime(rating.CreateAt!.Value.ToUniversalTime()),
                UpdateAt = Timestamp.FromDateTime(rating.UpdateAt!.Value.ToUniversalTime()),
            };
        }

        public override async Task<ProductRating.Response> Create(CreateRating request, ServerCallContext context)
        {
            var createRating = new RequestCreateRating
            {
                UserId = request.UserId,
                ProductItemId = request.ProductItemId,
                ProductId = request.ProductId,
                Content = request.Content,
                Point = request.Point,
                Image = JsonConvert.DeserializeObject<List<string>>(request.ImageList)
            };
            var response = await _repo.CreateRating(createRating);
            return new ProductRating.Response { Message = response.Message, StatusCode = response.StatusCode };
        }

        public override async Task<ProductRating.Response> Update(Rating request, ServerCallContext context)
        {
            var updateRating = new RequestUpdateRating
            {
                Id = request.UserId,
                ProductId = request.ProductId,
                ProductItemId = request.ProductItemId,
                UserId = request.UserId,
                Content = request.Content,
                Image = JsonConvert.DeserializeObject<List<string>>(request.ImageList),
                Point = request.Point,
            };
            var response = await _repo.UpdateRating(updateRating);
            return new ProductRating.Response { Message = response.Message, StatusCode = response.StatusCode };
        }

        public override async Task<ProductRating.Response> Delete(ProductRating.Id request, ServerCallContext context)
        {
            var response = await _repo.DeleteRating(request.SearchId);
            return new ProductRating.Response { Message = response.Message, StatusCode = response.StatusCode };
        }
    }
}
