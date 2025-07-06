using Domain.Requests;
using Domain.Responses;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StiktifyShopBackend.Interfaces;
using StiktifyShopBackend.ProductRating;

namespace StiktifyShopBackend.Providers
{
    public class ProductRatingProvider : IProductRatingProvider
    {
        private ProductRatingGrpc.ProductRatingGrpcClient _client;
        private IProductItemProvider _productItemProvider;

        public ProductRatingProvider(ProductRatingGrpc.ProductRatingGrpcClient client, IProductItemProvider productItemProvider)
        {
            _client = client ?? throw new ArgumentException(nameof(_client));
            _productItemProvider = productItemProvider ?? throw new ArgumentException(nameof(_productItemProvider));
        }

        public async Task<Domain.Responses.Response> CreateRating(RequestCreateRating createRating)
        {
            var createGrpc = new CreateRating
            {
                Content = createRating.Content,
                ProductItemId = createRating.ProductItemId,
                ImageList = JsonConvert.SerializeObject(createRating.Image),
                Point = createRating.Point,
                ProductId = createRating.ProductId,
                UserId = createRating.UserId,
            };
            var response = await _client.CreateAsync(createGrpc);
            return new Domain.Responses.Response { Message = response.Message, StatusCode = response.StatusCode };
        }

        public async Task<Domain.Responses.Response> DeleteRating(string productId)
        {
            var response = await _client.DeleteAsync(new Id { SearchId = productId });
            return new Domain.Responses.Response { Message = response.Message, StatusCode = response.StatusCode };
        }

        public IQueryable<ResponseProductRating> GetAll()
        {
            var listGrpc = _client.GetAll(new ProductRating.Empty());
            var list = listGrpc.Item.Select(item => new ResponseProductRating
            {
                Id = item.Id,
                Content = item.Content,
                UserId = item.UserId,
                ProductItemId = item.ProductItemId,
                Point = item.Point,
                Image = JsonConvert.DeserializeObject<ICollection<string>>(item.ImageList),
                ProductId = item.ProductId,
                ProductItem = _productItemProvider.GetOne(item.ProductItemId).Result,
                CreateAt = item.CreateAt.ToDateTime(),
                UpdateAt = item.UpdateAt.ToDateTime(),
            });
            return list.AsQueryable();
        }

        public IQueryable<ResponseProductRating> GetAllOfOption(string optionId)
        {
            var listGrpc = _client.GetAllOfOption(new Id { SearchId = optionId });
            var list = listGrpc.Item.Select(item => new ResponseProductRating
            {
                Id = item.Id,
                Content = item.Content,
                UserId = item.UserId,
                ProductItemId = item.ProductItemId,
                Point = item.Point,
                Image = JsonConvert.DeserializeObject<ICollection<string>>(item.ImageList),
                ProductId = item.ProductId,
                ProductItem = _productItemProvider.GetOne(item.ProductItemId).Result,
                CreateAt = item.CreateAt.ToDateTime(),
                UpdateAt = item.UpdateAt.ToDateTime(),
            });
            return list.AsQueryable();
        }

        public IQueryable<ResponseProductRating> GetAllOfProduct(string productId)
        {
            var listGrpc = _client.GetAllOfProduct(new Id { SearchId = productId });
            var list = listGrpc.Item.Select(item => new ResponseProductRating
            {
                Id = item.Id,
                Content = item.Content,
                UserId = item.UserId,
                ProductItemId = item.ProductItemId,
                Point = item.Point,
                Image = JsonConvert.DeserializeObject<ICollection<string>>(item.ImageList),
                ProductItem = _productItemProvider.GetOne(item.ProductItemId).Result,
                ProductId = item.ProductId,
                CreateAt = item.CreateAt.ToDateTime(),
                UpdateAt = item.UpdateAt.ToDateTime(),
            });
            return list.AsQueryable();
        }

        public async Task<ResponseProductRating?> GetOne(string ratingId)
        {
            var ratingGrpc = await _client.GetOneAsync(new Id { SearchId = ratingId });
            return new ResponseProductRating
            {
                Id = ratingGrpc.Id,
                Content = ratingGrpc.Content,
                UserId = ratingGrpc.UserId,
                ProductItemId = ratingGrpc.ProductItemId,
                Point = ratingGrpc.Point,
                Image = JsonConvert.DeserializeObject<ICollection<string>>(ratingGrpc.ImageList),
                ProductId = ratingGrpc.ProductId,
                ProductItem = await _productItemProvider.GetOne(ratingGrpc.ProductItemId),
                CreateAt = ratingGrpc.CreateAt.ToDateTime(),
                UpdateAt = ratingGrpc.UpdateAt.ToDateTime(),
            };
        }

        public async Task<Domain.Responses.Response> UpdateRating(RequestUpdateRating updateRating)
        {
            var updateGrpc = new Rating
            {
                Id = updateRating.Id,
                Content = updateRating.Content,
                ImageList = JsonConvert.SerializeObject(updateRating.Image)
            };
            var response = await _client.UpdateAsync(updateGrpc);
            return new Domain.Responses.Response { Message = response.Message, StatusCode = response.StatusCode };
        }
    }
}
