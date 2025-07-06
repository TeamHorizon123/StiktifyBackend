using Domain.Requests;
using Domain.Responses;
using StiktifyShopBackend.Interfaces;
using StiktifyShopBackend.ProductItem;

namespace StiktifyShopBackend.Providers
{
    public class ProductItemProvider : IProductItemProvider
    {
        private ProductItemGrpc.ProductItemGrpcClient _client;
        private IProductProvider _productProvider;

        public ProductItemProvider(ProductItemGrpc.ProductItemGrpcClient client, IProductProvider productProvider)
        {
            _client = client ?? throw new ArgumentException(nameof(client));
            _productProvider = productProvider ?? throw new ArgumentException(nameof(productProvider));
        }

        public async Task<Domain.Responses.Response> AddProductItem(RequestCreateProductItem requestCreate)
        {
            var grpcRequest = new CreateProductItem
            {
                ProductId = requestCreate.ProductId,
                Size = requestCreate.Size,
                Color = requestCreate.Color,
                Type = requestCreate.Type,
                Quantity = requestCreate.Quantity,
                Price = requestCreate.Price,
                Image = requestCreate.Image
            };
            var grpcResponse = await _client.CreateAsync(grpcRequest);
            return new Domain.Responses.Response
            {
                Message = grpcResponse.Message,
                StatusCode = grpcResponse.StatusCode
            };
        }

        public async Task<Domain.Responses.Response> DeleteProductItem(string itemId)
        {
            var grpcResponse = await _client.DeleteAsync(new Id { SearchId = itemId });
            return new Domain.Responses.Response
            {
                Message = grpcResponse.Message,
                StatusCode = grpcResponse.StatusCode
            };
        }

        public IQueryable<ResponseProductItem> GetAll()
        {
            var listGrpc = _client.GetAll(new Empty());
            return listGrpc.Items.Select(item => new ResponseProductItem
            {
                Id = item.Id,
                ProductId = item.ProductId,
                Product = _productProvider.GetOne(item.ProductId).Result,
                Size = item.Size,
                Color = item.Color,
                Type = item.Type,
                Quantity = item.Quantity,
                Price = item.Price,
                Image = item.Image,
                CreateAt = item.CreatedAt.ToDateTime(),
                UpdateAt = item.UpdatedAt.ToDateTime()
            }).AsQueryable();
        }

        public IQueryable<ResponseProductItem> GetAllOfProduct(string productId)
        {
            var listGrpc = _client.GetAllOfProduct(new Id { SearchId = productId });
            return listGrpc.Items.Select(item => new ResponseProductItem
            {
                Id = item.Id,
                ProductId = item.ProductId,
                Product = _productProvider.GetOne(item.ProductId).Result,
                Size = item.Size,
                Color = item.Color,
                Type = item.Type,
                Quantity = item.Quantity,
                Price = item.Price,
                Image = item.Image,
                CreateAt = item.CreatedAt.ToDateTime(),
                UpdateAt = item.UpdatedAt.ToDateTime()
            }).AsQueryable();
        }

        public async Task<ResponseProductItem?> GetOne(string itemId)
        {
            var itemGrpc = await _client.GetOneAsync(new Id { SearchId = itemId });
            return itemGrpc == null ? null : new ResponseProductItem
            {
                Id = itemGrpc.Id,
                ProductId = itemGrpc.ProductId,
                Product = _productProvider.GetOne(itemGrpc.ProductId).Result,
                Size = itemGrpc.Size,
                Color = itemGrpc.Color,
                Type = itemGrpc.Type,
                Quantity = itemGrpc.Quantity,
                Price = itemGrpc.Price,
                Image = itemGrpc.Image,
                CreateAt = itemGrpc.CreatedAt.ToDateTime(),
                UpdateAt = itemGrpc.UpdatedAt.ToDateTime()
            };
        }

        public async Task<Domain.Responses.Response> UpdateProductItem(RequestUpdateProductItem requestUpdate)
        {
            var grpcRequest = new ProductItem.ProductItem
            {
                Id = requestUpdate.Id,
                ProductId = requestUpdate.ProductId,
                Size = requestUpdate.Size,
                Color = requestUpdate.Color,
                Type = requestUpdate.Type,
                Quantity = requestUpdate.Quantity,
                Price = requestUpdate.Price,
                Image = requestUpdate.Image
            };
            var grpcResponse = await _client.UpdateAsync(grpcRequest);
            return new Domain.Responses.Response
            {
                Message = grpcResponse.Message,
                StatusCode = grpcResponse.StatusCode
            };
        }
    }
}
