using Domain.Requests;
using Domain.Responses;
using Microsoft.AspNetCore.Mvc;
using StiktifyShopBackend.Interfaces;
using StiktifyShopBackend.ProductVarriant;

namespace StiktifyShopBackend.Providers
{
    public class ProductVarriantProvider : IProductVarriantProvider
    {
        private ProductVarriantGrpc.ProductVarriantGrpcClient _client;
        private IProductOptionProvider _productOptionProvider;
        private ICategorySizeProvider _categorySizeProvider;

        public ProductVarriantProvider
            (ProductVarriantGrpc.ProductVarriantGrpcClient client,
            IProductOptionProvider productOptionProvider,
            ICategorySizeProvider categorySizeProvider)
        {
            _client = client ?? throw new ArgumentException(nameof(client));
            _productOptionProvider = productOptionProvider ?? throw new ArgumentException(nameof(productOptionProvider));
            _categorySizeProvider = categorySizeProvider ?? throw new ArgumentException(nameof(categorySizeProvider));
        }

        public async Task<Domain.Responses.Response> AddProductVarriant(RequestCreateProductVarriant requestCreate)
        {
            var grpcRequest = new CreateProductVariant
            {
                ProductOptionId = requestCreate.ProductOptionId,
                SizeId = requestCreate.SizeId,
                Quantity = requestCreate.Quantity,
                Price = requestCreate.Price
            };
            var grpcResponse = await _client.CreateAsync(grpcRequest);
            return new Domain.Responses.Response
            {
                StatusCode = grpcResponse.StatusCode,
                Message = grpcResponse.Message,
            };
        }

        public async Task<Domain.Responses.Response> DeleteProductVarriant(string optionId, string sizeId)
        {
            var grpcResponse = await _client.DeleteAsync(new VarriantId { ProductOptionId = optionId, SizeId = sizeId });
            return new Domain.Responses.Response
            {
                StatusCode = grpcResponse.StatusCode,
                Message = grpcResponse.Message,
            };
        }

        public IQueryable<ResponseProductVarriant> GetAll()
        {
            var grpcList = _client.GetAll(new Empty());
            return grpcList.Item.Select(varriant => new ResponseProductVarriant
            {                
                ProductOptionId = varriant.ProductOptionId,
                SizeId = varriant.SizeId,
                Quantity = varriant.Quantity,
                Price = varriant.Price,
                ProductOption = _productOptionProvider.GetOne(varriant.ProductOptionId).Result,
                Size = _categorySizeProvider.GetOne(varriant.SizeId).Result
            }).AsQueryable();
        }

        public IQueryable<ResponseProductVarriant> GetAllOfProduct(string productId)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ResponseProductVarriant> GetAllOfProductOption(string optionId)
        {
            var gprcList = _client.GetAllOfProductOption(new Id { SearchId = optionId });
            return gprcList.Item.Select(varriant => new ResponseProductVarriant
            {
                ProductOptionId = varriant.ProductOptionId,
                SizeId = varriant.SizeId,
                Quantity = varriant.Quantity,
                Price = varriant.Price,
                ProductOption = _productOptionProvider.GetOne(varriant.ProductOptionId).Result,
                Size = _categorySizeProvider.GetOne(varriant.SizeId).Result
            }).AsQueryable();
        }

        public async Task<ResponseProductVarriant?> GetOne(string optionId, string sizeId)
        {
            var grpcVarriant = await _client.GetOneAsync(new VarriantId
            {
                ProductOptionId = optionId,
                SizeId = sizeId
            });
            return grpcVarriant == null ? null : new ResponseProductVarriant
            {
                ProductOptionId = grpcVarriant.ProductOptionId,
                SizeId = grpcVarriant.SizeId,
                Quantity = grpcVarriant.Quantity,
                Price = grpcVarriant.Price,
                ProductOption = await _productOptionProvider.GetOne(grpcVarriant.ProductOptionId),
                Size = await _categorySizeProvider.GetOne(grpcVarriant.SizeId)
            };
        }

        public async Task<Domain.Responses.Response> UpdateProductVarriant(RequestUpdateProductVarriant requestUpdate)
        {
            var grpcRequest = new ProductVariant
            {
                ProductOptionId = requestUpdate.ProductOptionId,
                SizeId = requestUpdate.SizeId,
                Quantity = requestUpdate.Quantity,
                Price = requestUpdate.Price
            };
            var grpcResponse = await _client.UpdateAsync(grpcRequest);
            return new Domain.Responses.Response
            {
                StatusCode = grpcResponse.StatusCode,
                Message = grpcResponse.Message,
            };
        }
    }
}
