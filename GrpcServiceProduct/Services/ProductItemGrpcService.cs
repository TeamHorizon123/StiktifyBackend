using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcServiceProduct.Interfaces;
using GrpcServiceProduct.ProductItem;

namespace GrpcServiceProduct.Services
{
    public class ProductItemGrpcService : ProductItemGrpc.ProductItemGrpcBase
    {
        private IProductItemRepository _repo;
        public ProductItemGrpcService(IProductItemRepository repo)
        {
            _repo = repo ?? throw new ArgumentException(nameof(repo));
        }

        public override async Task<ProductItems> GetAll(ProductItem.Empty request, ServerCallContext context)
        {
            var listData = await _repo.GetAllProductItems();
            var response = new ProductItems();
            response.Items.AddRange(listData.Select(pr => new ProductItem.ProductItem
            {
                Id = pr.Id,
                ProductId = pr.ProductId,
                Color = pr.Color,
                Image = pr.Image,
                Price = pr.Price,
                Size = pr.Size,
                Type = pr.Type,
                Quantity = pr.Quantity,
                CreatedAt = Timestamp.FromDateTime(pr.CreateAt.ToUniversalTime()),
                UpdatedAt = Timestamp.FromDateTime(pr.UpdateAt.ToUniversalTime())
            }));
            return response;
        }

        public override async Task<ProductItems> GetAllOfProduct(Id request, ServerCallContext context)
        {
            var list = await _repo.GetAllOfProduct(request.SearchId);
            var response = new ProductItems();
            response.Items.AddRange(list.Select(pr => new ProductItem.ProductItem
            {
                Id = pr.Id,
                ProductId = pr.ProductId,
                Color = pr.Color,
                Image = pr.Image,
                Price = pr.Price,
                Size = pr.Size,
                Type = pr.Type,
                Quantity = pr.Quantity,
                CreatedAt = Timestamp.FromDateTime(pr.CreateAt.ToUniversalTime()),
                UpdatedAt = Timestamp.FromDateTime(pr.UpdateAt.ToUniversalTime())
            }));
            return response;
        }

        public override async Task<ProductItem.ProductItem> GetOne(Id request, ServerCallContext context)
        {
            var productItem = await _repo.GetProductItem(request.SearchId);
            return new ProductItem.ProductItem
            {
                Id = productItem?.Id ?? string.Empty,
                ProductId = productItem?.ProductId ?? string.Empty,
                Color = productItem?.Color ?? string.Empty,
                Image = productItem?.Image ?? string.Empty,
                Price = productItem?.Price ?? 0,
                Size = productItem?.Size ?? string.Empty,
                Type = productItem?.Type ?? string.Empty,
                Quantity = productItem?.Quantity ?? 0,
                CreatedAt = Timestamp.FromDateTime(productItem?.CreateAt.ToUniversalTime() ?? DateTime.UtcNow),
                UpdatedAt = Timestamp.FromDateTime(productItem?.UpdateAt.ToUniversalTime() ?? DateTime.UtcNow)
            };
        }

        public override async Task<Response> Create(CreateProductItem request, ServerCallContext context)
        {
            var productItem = new Domain.Requests.RequestCreateProductItem
            {
                ProductId = request.ProductId,
                Color = request.Color,
                Image = request.Image,
                Price = request.Price,
                Size = request.Size,
                Type = request.Type,
                Quantity = request.Quantity
            };
            var response = await _repo.AddProductItem(productItem);
            return new Response { Message = response.Message, StatusCode = response.StatusCode };
        }

        public override async Task<Response> Update(ProductItem.ProductItem request, ServerCallContext context)
        {
            var productItem = new Domain.Requests.RequestUpdateProductItem
            {
                Id = request.Id,
                ProductId = request.ProductId,
                Color = request.Color,
                Image = request.Image,
                Price = request.Price,
                Size = request.Size,
                Type = request.Type,
                Quantity = request.Quantity
            };
            var response = await _repo.UpdateProductItem(productItem);
            return new Response { Message = response.Message, StatusCode = response.StatusCode };
        }

        public override async Task<Response> Delete(Id request, ServerCallContext context)
        {
            var response = await _repo.DeleteProductItem(request.SearchId);
            return new Response
            {
                Message = response.Message,
                StatusCode = response.StatusCode
            };
        }

        public override Task<Response> DeleteMany(Ids request, ServerCallContext context)
        {
            throw new NotImplementedException("DeleteMany is not implemented yet. Please implement it in the repository and service layer.");
        }
    }
}
