using Domain.Requests;
using Domain.Responses;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StiktifyShopBackend.Interfaces;
using StiktifyShopBackend.Product;

namespace StiktifyShopBackend.Providers
{
    public class ProductProvider : IProductProvider
    {
        private ProductGrpc.ProductGrpcClient _client;

        public ProductProvider(ProductGrpc.ProductGrpcClient client)
        {
            _client = client ?? throw new ArgumentException(nameof(_client));
        }

        public async Task<Domain.Responses.Response> Create(RequestCreateProduct createProduct)
        {
            var createGprc = new CreateProduct
            {
                Description = createProduct.Description,
                Name = createProduct.Name,
                ShopId = createProduct.ShopId,
                Thumbnail = createProduct.Thumbnail,
                CategoryIds = JsonConvert.SerializeObject(createProduct.CategoryId)
            };
            var response = await _client.CreateAsync(createGprc);
            return new Domain.Responses.Response { Message = response.Message, StatusCode = response.StatusCode };
        }

        public async Task<Domain.Responses.Response> Delete(string productId)
        {
            var response = await _client.DeleteAsync(new Id { SearchId = productId });
            return new Domain.Responses.Response { Message = response.Message, StatusCode = response.StatusCode };
        }

        public async Task<Domain.Responses.Response> DeleteAllOfShop(string shopId)
        {
            var response = await _client.DeleteAllOfShopAsync(new Id { SearchId = shopId });
            return new Domain.Responses.Response { Message = response.Message, StatusCode = response.StatusCode };
        }

        public async Task<Domain.Responses.Response> DeleteMany(ICollection<string> listProductId)
        {
            var listRemove = new Ids();
            listRemove.Item.AddRange(listProductId.Select(id => new Id { SearchId = id }));
            var response = await _client.DeleteManyAsync(listRemove);
            return new Domain.Responses.Response { Message = response.Message, StatusCode = response.StatusCode };
        }

        public IQueryable<ResponseProduct> GetAll()
        {
            var gprcList = _client.GetAll(new Product.Empty());
            var list = gprcList.Item.Select(item => new ResponseProduct
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                IsActive = item.IsActive,
                Order = item.Order,
                RangePrice = item.RangePrice,
                RatingPoint = item.RatingPoint,
                RatingTurn = item.RatingTurn,
                ShopId = item.ShopId,
                Thumbnail = item.Thumbnail,
                CreateAt = item.CreateAt.ToDateTime(),
                UpdateAt = item.UpdateAt.ToDateTime()
            });
            return list.AsQueryable();
        }

        public async Task<IEnumerable<string>> GetAllImage(string productId)
        {
            var gprcList = await _client.GetAllImageAsync(new Id { SearchId = productId });
            var list = gprcList.Item.Select(item => item.ImageUri);
            return list;
        }

        public IQueryable<ResponseProduct> GetAllOfCategory(string categoryId)
        {
            var gprcList = _client.GetAllOfCategory(new Id { SearchId = categoryId });
            var list = gprcList.Item.Select(item => new ResponseProduct
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                IsActive = item.IsActive,
                Order = item.Order,
                ShopId = item.ShopId,
                Thumbnail = item.Thumbnail,
                RangePrice = item.RangePrice,
                RatingPoint = item.RatingPoint,
                RatingTurn = item.RatingTurn,
                CreateAt = item.CreateAt.ToDateTime(),
                UpdateAt = item.UpdateAt.ToDateTime()
            });
            return list.AsQueryable();
        }

        public IQueryable<ResponseProduct> GetAllOfShop(string shopId)
        {
            var gprcList = _client.GetAllOfShop(new Id { SearchId = shopId });
            var list = gprcList.Item.Select(item => new ResponseProduct
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                IsActive = item.IsActive,
                Order = item.Order,
                RatingTurn = item.RatingTurn,
                RatingPoint = item.RatingPoint,
                RangePrice = item.RangePrice,
                ShopId = item.ShopId,
                Thumbnail = item.Thumbnail,
                CreateAt = item.CreateAt.ToDateTime(),
                UpdateAt = item.UpdateAt.ToDateTime()
            });
            return list.AsQueryable();
        }

        public async Task<ResponseProduct?> GetOne(string productId)
        {
            var productGrpc = await _client.GetOneAsync(new Id { SearchId = productId });
            return new ResponseProduct
            {
                Id = productGrpc.Id,
                Name = productGrpc.Name,
                Description = productGrpc.Description,
                RangePrice = productGrpc.RangePrice,
                RatingPoint = productGrpc.RatingPoint,
                RatingTurn = productGrpc.RatingTurn,
                IsActive = productGrpc.IsActive,
                Order = productGrpc.Order,
                ShopId = productGrpc.ShopId,
                Thumbnail = productGrpc.Thumbnail,
                CreateAt = productGrpc.CreateAt.ToDateTime(),
                UpdateAt = productGrpc.UpdateAt.ToDateTime()
            };
        }

        public async Task<Domain.Responses.Response> Update(RequestUpdateProduct updateProduct)
        {
            var updateGrpc = new Product.Product
            {
                Id = updateProduct.Id,
                Description = updateProduct.Description,
                Name = updateProduct.Name,
                IsActive = updateProduct.IsActive,
                ShopId = updateProduct.ShopId,
                Thumbnail = updateProduct.Thumbnail,
                CategoryIds = JsonConvert.SerializeObject(updateProduct.CategoryId),
            };
            var response = await _client.UpdateAsync(updateGrpc);
            return new Domain.Responses.Response { Message = response.Message, StatusCode = response.StatusCode };
        }
    }
}
