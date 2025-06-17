using Domain.Requests;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcServiceProduct.Category;
using GrpcServiceProduct.Interfaces;

namespace GrpcServiceProduct.Services
{
    public class CategoryGrpcService : CategoryGrpc.CategoryGrpcBase
    {
        private ICategoryRepository _repo;

        public CategoryGrpcService(ICategoryRepository repo)
        {
            _repo = repo ?? throw new ArgumentException(nameof(_repo));
        }

        public override async Task<Categories> GetAll(Category.Empty request, ServerCallContext context)
        {
            var listCategories = await _repo.GetAll();
            Category.Categories categories = new Category.Categories();
            categories.Item.AddRange(listCategories.Select(category
                => new Category.Category
                {
                    Id = category.Id,
                    Name = category.Name,
                    ParentId = category.ParentId,
                    CreateAt = Timestamp.FromDateTime(category.CreateAt!.Value.ToUniversalTime()),
                    UpdateAt = Timestamp.FromDateTime(category.UpdateAt!.Value.ToUniversalTime())
                }));
            return categories;
        }

        public override async Task<Categories> GetAllOfProduct(Id request, ServerCallContext context)
        {
            var listCategories = await _repo.GetAllOfProduct(request.SearchId);
            Category.Categories categories = new Category.Categories();
            categories.Item.AddRange(listCategories.Select(category
                => new Category.Category
                {
                    Id = category.Id,
                    Name = category.Name,
                    ParentId = category.ParentId,
                    CreateAt = Timestamp.FromDateTime(category.CreateAt!.Value.ToUniversalTime()),
                    UpdateAt = Timestamp.FromDateTime(category.UpdateAt!.Value.ToUniversalTime())
                }));
            return categories;
        }

        public override async Task<Categories> GetAllChilren(Id request, ServerCallContext context)
        {
            var listCategories = await _repo.GetAllChildren(request.SearchId);
            Category.Categories categories = new Category.Categories();
            categories.Item.AddRange(listCategories.Select(category
                => new Category.Category
                {
                    Id = category.Id,
                    Name = category.Name,
                    ParentId = category.ParentId,
                    CreateAt = Timestamp.FromDateTime(category.CreateAt!.Value.ToUniversalTime()),
                    UpdateAt = Timestamp.FromDateTime(category.UpdateAt!.Value.ToUniversalTime())
                }));
            return categories;
        }

        public override async Task<Category.Category> GetOne(Id request, ServerCallContext context)
        {
            var category = await _repo.GetOne(request.SearchId);
            if (category == null) return new Category.Category();
            Category.Category grpcCategory = new Category.Category
            {
                Id = category.Id,
                Name = category.Name,
                ParentId = category.ParentId,
                CreateAt = Timestamp.FromDateTime(category.CreateAt!.Value.ToUniversalTime()),
                UpdateAt = Timestamp.FromDateTime(category.UpdateAt!.Value.ToUniversalTime())
            };
            return grpcCategory;
        }

        public override async Task<Response> Create(CreateCategory request, ServerCallContext context)
        {
            var createCategory = new RequestCreateCategory
            {
                Name = request.Name,
                ParentId = request.ParentId,
            };
            var response = await _repo.CreateCategory(createCategory);
            return new Response { Message = response.Message, StatusCode = response.StatusCode };
        }

        public override async Task<Response> Update(Category.Category request, ServerCallContext context)
        {
            var updateCategory = new RequestUpdateCategory
            {
                Id = request.Id,
                Name = request.Name,
                ParentId = request.ParentId,
                CreateAt = request.CreateAt.ToDateTime()
            };
            var response = await _repo.UpdateCategory(updateCategory);
            return new Response { Message = response.Message, StatusCode = response.StatusCode };
        }

        public override async Task<Response> Delete(Id request, ServerCallContext context)
        {
            var response = await _repo.DeleteCategory(request.SearchId);
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
