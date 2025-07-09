using Grpc.Core;
using GrpcServiceProduct.Interfaces;
using GrpcServiceProduct.ProductVarriant;

namespace GrpcServiceProduct.Services
{
    public class ProductVarriantGrpcService : ProductVarriantGrpc.ProductVarriantGrpcBase
    {
        private IProductVarriantRepository _repo;
        public ProductVarriantGrpcService(IProductVarriantRepository repo)
        {
            _repo = repo ?? throw new ArgumentException(nameof(repo));
        }

        public override async Task<ProductVariants> GetAll(Empty request, ServerCallContext context)
        {
            var productVarriants = await _repo.GetAllProductVarriants();
            var response = new ProductVariants();
            response.Item.AddRange(productVarriants.Select(pv => new ProductVarriant.ProductVariant
            {
                Price = pv.Price,
                ProductOptionId = pv.ProductOptionId,
                Quantity = pv.Quantity,
                SizeId = pv.SizeId,
            }));
            return response;
        }

        public override async Task<ProductVariants> GetAllOfProductOption(Id request, ServerCallContext context)
        {
            var productVarriants = await _repo.GetAllOfProductOption(request.SearchId);
            var response = new ProductVariants();
            response.Item.AddRange(productVarriants.Select(pv => new ProductVarriant.ProductVariant
            {
                Price = pv.Price,
                ProductOptionId = pv.ProductOptionId,
                Quantity = pv.Quantity,
                SizeId = pv.SizeId,
            }));
            return response;
        }

        public override async Task<ProductVariant> GetOne(VarriantId request, ServerCallContext context)
        {
            var productVarriant = await _repo.GetProductVarriant(request.ProductOptionId, request.SizeId);
            return new ProductVarriant.ProductVariant
            {
                Price = productVarriant?.Price ?? 0,
                ProductOptionId = productVarriant?.ProductOptionId ?? string.Empty,
                Quantity = productVarriant?.Quantity ?? 0,
                SizeId = productVarriant?.SizeId ?? string.Empty
            };
        }

        public override async Task<Response> Create(CreateProductVariant request, ServerCallContext context)
        {
            var createProductVarriant = new Domain.Requests.RequestCreateProductVarriant
            {
                Price = request.Price,
                ProductOptionId = request.ProductOptionId,
                Quantity = request.Quantity,
                SizeId = request.SizeId
            };
            var response = await _repo.CreateProductVarriant(createProductVarriant);
            return new Response { Message = response.Message, StatusCode = response.StatusCode };
        }

        public override async Task<Response> Update(ProductVariant request, ServerCallContext context)
        {
            var updateProductVarriant = new Domain.Requests.RequestCreateProductVarriant
            {
                Price = request.Price,
                ProductOptionId = request.ProductOptionId,
                Quantity = request.Quantity,
                SizeId = request.SizeId
            };
            var response = await _repo.UpdateProductVarriant(updateProductVarriant);
            return new Response { Message = response.Message, StatusCode = response.StatusCode };
        }

        public override async Task<Response> Delete(VarriantId request, ServerCallContext context)
        {
            var response = await _repo.DeleteProductVarriant(request.ProductOptionId);
            return new Response
            {
                Message = response.Message,
                StatusCode = response.StatusCode
            };
        }
    }
}
