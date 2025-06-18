using Domain.Entities;
using Domain.Requests;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcServiceProduct.Interfaces;
using GrpcServiceProduct.Product;
using Newtonsoft.Json;

namespace GrpcServiceProduct.Services
{
    public class ProductGrpcService : ProductGrpc.ProductGrpcBase
    {
        private IProductRepository _repo;

        public ProductGrpcService(IProductRepository repo)
        {
            _repo = repo ?? throw new ArgumentException(nameof(_repo));
        }

        public override async Task<Products> GetAll(Product.Empty request, ServerCallContext context)
        {
            var listProducts = await _repo.GetAll();
            Product.Products grpcList = new();
            grpcList.Item.AddRange(listProducts.Select(item
                => new Product.Product
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    Thumbnail = item.Thumbnail,
                    ShopId = item.ShopId,
                    Discount = item.Discount,
                    Order = item.Order,
                    Price = item.Price,
                    Rating = item.Rating,
                    IsActive = item.IsActive,
                    CreateAt = Timestamp.FromDateTime(item.CreateAt!.Value.ToUniversalTime()),
                    UpdateAt = Timestamp.FromDateTime(item.UpdateAt!.Value.ToUniversalTime())
                }));
            return grpcList;
        }

        public override async Task<Products> GetAllOfShop(Id request, ServerCallContext context)
        {
            var listProducts = await _repo.GetAllOfShop(request.SearchId);
            Product.Products grpcList = new();
            grpcList.Item.AddRange(listProducts.Select(item
                => new Product.Product
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    Thumbnail = item.Thumbnail,
                    ShopId = item.ShopId,
                    Discount = item.Discount,
                    Order = item.Order,
                    Price = item.Price,
                    Rating = item.Rating,
                    IsActive = item.IsActive,
                    CreateAt = Timestamp.FromDateTime(item.CreateAt!.Value.ToUniversalTime()),
                    UpdateAt = Timestamp.FromDateTime(item.UpdateAt!.Value.ToUniversalTime())
                }));
            return grpcList;
        }

        public override async Task<Products> GetAllOfCategory(Id request, ServerCallContext context)
        {
            var listProducts = await _repo.GetAllOfCategory(request.SearchId);
            Product.Products grpcList = new();
            grpcList.Item.AddRange(listProducts.Select(item
                => new Product.Product
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    Thumbnail = item.Thumbnail,
                    ShopId = item.ShopId,
                    Discount = item.Discount,
                    Order = item.Order,
                    Price = item.Price,
                    Rating = item.Rating,
                    IsActive = item.IsActive,
                    CreateAt = Timestamp.FromDateTime(item.CreateAt!.Value.ToUniversalTime()),
                    UpdateAt = Timestamp.FromDateTime(item.UpdateAt!.Value.ToUniversalTime())
                }));
            return grpcList;
        }

        public override async Task<Links> GetAllImage(Id request, ServerCallContext context)
        {
            var listProducts = await _repo.GetAllImage(request.SearchId);
            Product.Links grpcList = new();
            grpcList.Item.AddRange(listProducts.Select(item
                => new Product.Link
                {
                    ImageUri = item,
                }));
            return grpcList;
        }

        public override async Task<Product.Product> GetOne(Id request, ServerCallContext context)
        {
            var product = await _repo.GetOne(request.SearchId);
            if (product == null)
                return new Product.Product();
            Product.Product grpcProduct = new()
            {
                Id = product.Id,
                ShopId = product.ShopId,
                Thumbnail = product.Thumbnail,
                Name = product.Name,
                Description = product.Description,
                Discount = product.Discount,
                Order = product.Order,
                Rating = product.Rating,
                Price = product.Price,
                IsActive = product.IsActive,
                CreateAt = Timestamp.FromDateTime(product.CreateAt!.Value.ToUniversalTime()),
                UpdateAt = Timestamp.FromDateTime(product.UpdateAt!.Value.ToUniversalTime())
            };
            return grpcProduct;
        }

        public override async Task<Response> Create(CreateProduct request, ServerCallContext context)
        {
            var createProduct = new RequestCreateProduct
            {
                ShopId = request.ShopId,
                Name = request.Name,
                Description = request.Description,
                Discount = request.Discount,
                Price = request.Price,
                Thumbnail = request.Thumbnail,
                CategoryId = JsonConvert.DeserializeObject<string[]>(request.CategoryIds) ?? []
            };
            var response = await _repo.Create(createProduct);
            return new Response { Message = response.Message, StatusCode = response.StatusCode };
        }

        public override async Task<Response> Update(Product.Product request, ServerCallContext context)
        {
            var updateProduct = new RequestUpdateProduct
            {
                Id = request.Id,
                ShopId = request.ShopId,
                Name = request.Name,
                Description = request.Description,
                Discount = request.Discount,
                Price = request.Price,
                Thumbnail = request.Thumbnail,
                CategoryId = JsonConvert.DeserializeObject<string[]>(request.CategoryIds) ?? [],
                IsActive = request.IsActive,
                CreateAt = request.CreateAt.ToDateTime(),
            };

            var response = await _repo.Update(updateProduct);
            return new Response { Message = response.Message, StatusCode = response.StatusCode };
        }

        public override async Task<Response> Delete(Id request, ServerCallContext context)
        {
            var response = await _repo.Delete(request.SearchId);
            return new Response { Message = response.Message, StatusCode = response.StatusCode };
        }

        public override async Task<Response> DeleteMany(Ids request, ServerCallContext context)
        {
            string[] listRemove = { };
            int index = 0;
            foreach (var item in request.Item)
            {
                listRemove.SetValue(item.SearchId, index);
                index++;
            }
            var response = await _repo.DeleteMany(listRemove);
            return new Response { Message = response.Message, StatusCode = response.StatusCode };
        }
    }
}
