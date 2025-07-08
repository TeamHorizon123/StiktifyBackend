using Domain.Requests;
using Domain.Responses;
using StiktifyShopBackend.Category;
using StiktifyShopBackend.Interfaces;

namespace StiktifyShopBackend.Providers
{
    public class CategoryProvider : ICategoryProvider
    {
        private CategoryGrpc.CategoryGrpcClient _client;

        public CategoryProvider(CategoryGrpc.CategoryGrpcClient client)
        {
            _client = client ?? throw new ArgumentException(nameof(_client));
        }

        public async Task<Domain.Responses.Response> CreateCategory(RequestCreateCategory createCategory)
        {
            var createCategoryGprc = new CreateCategory
            {
                Name = createCategory.Name,
                ParentId = createCategory.ParentId,
            };
            var response = await _client.CreateAsync(createCategoryGprc);
            return new Domain.Responses.Response { Message = response.Message, StatusCode = response.StatusCode };
        }

        public async Task<Domain.Responses.Response> DeleteCategory(string categoryId)
        {
            var response = await _client.DeleteAsync(new Id { SearchId = categoryId });
            return new Domain.Responses.Response { Message = response.Message, StatusCode = response.StatusCode };
        }

        public async Task<Domain.Responses.Response> DeleteMany(string[] listCategoryId)
        {
            var listRemove = new Ids();
            listRemove.Item.AddRange(listCategoryId.Select(id => new Id { SearchId = id }));
            var response = await _client.DeleteManyAsync(listRemove);
            return new Domain.Responses.Response { Message = response.Message, StatusCode = response.StatusCode };
        }

        public IQueryable<ResponseCategory> GetAll()
        {
            var listGrpc = _client.GetAll(new Category.Empty());
            var listCategory = listGrpc.Item.Select(category => new ResponseCategory
            {
                Id = category.Id,
                ParentId = category.ParentId,
                Name = category.Name,
                CreateAt = category.CreateAt.ToDateTime(),
                UpdateAt = category.UpdateAt.ToDateTime(),
            });
            return listCategory.AsQueryable();
        }

        public IQueryable<ResponseCategory> GetAllChildren(string parentId)
        {
            var listGrpc = _client.GetAllChilren(new Id { SearchId = parentId });
            var listCategory = listGrpc.Item.Select(category => new ResponseCategory
            {
                Id = category.Id,
                ParentId = category.ParentId,
                Name = category.Name,
                CreateAt = category.CreateAt.ToDateTime(),
                UpdateAt = category.UpdateAt.ToDateTime(),
            });
            return listCategory.AsQueryable();
        }

        public IQueryable<ResponseCategory> GetAllOfProduct(string productId)
        {
            var listGrpc = _client.GetAllOfProduct(new Id { SearchId = productId });
            var listCategory = listGrpc.Item.Select(category => new ResponseCategory
            {
                Id = category.Id,
                ParentId = category.ParentId,
                Name = category.Name,
                CreateAt = category.CreateAt.ToDateTime(),
                UpdateAt = category.UpdateAt.ToDateTime(),
            });
            return listCategory.AsQueryable();
        }

        public async Task<ResponseCategory?> GetOne(string categoryId)
        {
            var categoryGrpc = await _client.GetOneAsync(new Id { SearchId = categoryId });
            return new ResponseCategory
            {
                Id = categoryGrpc.Id,
                Name = categoryGrpc.Name,
                ParentId = categoryGrpc.ParentId,
                CreateAt = categoryGrpc.CreateAt.ToDateTime(),
                UpdateAt = categoryGrpc.UpdateAt.ToDateTime(),
            };
        }

        public async Task<Domain.Responses.Response> UpdateCategory(RequestUpdateCategory updateCategory)
        {
            var updateCategoryGrpc = new Category.Category
            {
                Id = updateCategory.Id,
                Name = updateCategory.Name,
                ParentId = updateCategory.ParentId,
            };
            var response = await _client.UpdateAsync(updateCategoryGrpc);
            return new Domain.Responses.Response { Message = response.Message, StatusCode = response.StatusCode };
        }
    }
}
