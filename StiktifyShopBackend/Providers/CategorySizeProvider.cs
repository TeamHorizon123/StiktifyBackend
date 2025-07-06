using Domain.Requests;
using Domain.Responses;
using StiktifyShopBackend.CategorySize;
using StiktifyShopBackend.Interfaces;
using System.Drawing;

namespace StiktifyShopBackend.Providers
{
    public class CategorySizeProvider : ICategorySizeProvider
    {
        private CategorySizeGrpc.CategorySizeGrpcClient _client;
        private ICategoryProvider _categoryProvider;

        public CategorySizeProvider(CategorySizeGrpc.CategorySizeGrpcClient client, ICategoryProvider categoryProvider)
        {
            _client = client ?? throw new ArgumentException(nameof(client));
            _categoryProvider = categoryProvider ?? throw new ArgumentException(nameof(categoryProvider));
        }

        private ResponseCategory? GetCategoryInfo(string categoryId)
        {
            var grpcCategory = _categoryProvider.GetOne(categoryId).Result;
            return grpcCategory == null ? null : new ResponseCategory
            {
                Id = grpcCategory.Id,
                Name = grpcCategory.Name,
                ParentId = grpcCategory.ParentId,
                CreateAt = grpcCategory.CreateAt,
                UpdateAt = grpcCategory.UpdateAt
            };
        }

        public async Task<Domain.Responses.Response> AddCategorySize(RequestCreateCategorySize requestCreate)
        {
            var grpcRequest = new CreateCategorySize
            {
                CategoryId = requestCreate.CategoryId,
                Size = requestCreate.Size,
            };
            var grpcResponse = await _client.CreateAsync(grpcRequest);
            return new Domain.Responses.Response
            {
                Message = grpcResponse.Message,
                StatusCode = grpcResponse.StatusCode
            };
        }

        public async Task<Domain.Responses.Response> DeleteCategorySize(string sizeId)
        {
            var grpcResponse = await _client.DeleteAsync(new Id { SearchId = sizeId });
            return new Domain.Responses.Response
            {
                Message = grpcResponse.Message,
                StatusCode = grpcResponse.StatusCode
            };
        }

        public IQueryable<ResponseCategorySize> GetAll()
        {
            var listGrpc = _client.GetAll(new Empty());
            return listGrpc.Item.Select(size => new ResponseCategorySize
            {
                Id = size.Id,
                CategoryId = size.CategoryId,
                Size = size.Size,
                CreateAt = size.CreateAt.ToDateTime(),
                UpdateAt = size.UpdateAt.ToDateTime(),

            }).AsQueryable();
        }

        public IQueryable<ResponseCategorySize> GetAllOfCategory(string categoryId)
        {
            var listGrpc = _client.GetAllOfCategory(new Id { SearchId = categoryId });
            return listGrpc.Item.Select(size => new ResponseCategorySize
            {
                Id = size.Id,
                CategoryId = size.CategoryId,
                Size = size.Size,
                CreateAt = size.CreateAt.ToDateTime(),
                UpdateAt = size.UpdateAt.ToDateTime(),
            }).AsQueryable();
        }

        public IQueryable<ResponseCategorySize> GetAllOfProduct(string productId)
        {
            var listGrpc = _client.GetAllOfProduct(new Id { SearchId = productId });
            return listGrpc.Item.Select(size => new ResponseCategorySize
            {
                Id = size.Id,
                CategoryId = size.CategoryId,
                Size = size.Size,
                CreateAt = size.CreateAt.ToDateTime(),
                UpdateAt = size.UpdateAt.ToDateTime(),

            }).AsQueryable();
        }

        public async Task<ResponseCategorySize?> GetOne(string sizeId)
        {
            var grpcSize = await _client.GetOneAsync(new Id { SearchId = sizeId });
            return grpcSize == null ? null : new ResponseCategorySize
            {
                Id = grpcSize.Id,
                CategoryId = grpcSize.CategoryId,
                Size = grpcSize.Size,
                CreateAt = grpcSize.CreateAt.ToDateTime(),
                UpdateAt = grpcSize.UpdateAt.ToDateTime(),
            };
        }

        public async Task<Domain.Responses.Response> UpdateCategorySize(RequestUpdateCategorySize requestUpdate)
        {
            var grpcRequest = new CategorySize.CategorySize
            {
                Id = requestUpdate.Id,
                CategoryId = requestUpdate.CategoryId,
                Size = requestUpdate.Size,
            };
            var grpcResponse = await _client.UpdateAsync(grpcRequest);
            return new Domain.Responses.Response
            {
                Message = grpcResponse.Message,
                StatusCode = grpcResponse.StatusCode
            };
        }

        public IQueryable<ResponseCategorySize> GetAllOfProductOption(string optionId)
        {
            var listGrpc = _client.GetAllOfProductOption(new Id { SearchId = optionId });
            return listGrpc.Item.Select(size => new ResponseCategorySize
            {
                Id = size.Id,
                CategoryId = size.CategoryId,
                Size = size.Size,
                CreateAt = size.CreateAt.ToDateTime(),
                UpdateAt = size.UpdateAt.ToDateTime(),
                Category = GetCategoryInfo(size.CategoryId)
            }).AsQueryable();
        }
    }
}
