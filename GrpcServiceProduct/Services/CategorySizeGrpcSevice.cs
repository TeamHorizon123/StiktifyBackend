using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcServiceProduct.CategorySize;
using GrpcServiceProduct.Interfaces;

namespace GrpcServiceProduct.Services
{
    public class CategorySizeGrpcSevice : CategorySizeGrpc.CategorySizeGrpcBase
    {
        private ICategorySizeRepository _repo;
        public CategorySizeGrpcSevice(ICategorySizeRepository repo)
        {
            _repo = repo ?? throw new ArgumentException(nameof(repo));
        }
        public override async Task<CategorySizes> GetAll(CategorySize.Empty request, ServerCallContext context)
        {
            var listData = await _repo.GetAll();
            var response = new CategorySizes();
            response.Item.AddRange(listData.Select(cs => new CategorySize.CategorySize
            {
                Id = cs.Id,
                CategoryId = cs.CategoryId,
                Size = cs.Size,
                CreateAt = Timestamp.FromDateTime(cs.CreateAt!.Value.ToUniversalTime()),
                UpdateAt = Timestamp.FromDateTime(cs.UpdateAt!.Value.ToUniversalTime())
            }));

            return response;
        }

        public override async Task<CategorySizes> GetAllOfCategory(Id request, ServerCallContext context)
        {
            var listData = await _repo.GetAllOfCategory(request.SearchId);
            var response = new CategorySizes();
            response.Item.AddRange(listData.Select(cs => new CategorySize.CategorySize
            {
                Id = cs.Id,
                CategoryId = cs.CategoryId,
                Size = cs.Size,
                CreateAt = Timestamp.FromDateTime(cs.CreateAt!.Value.ToUniversalTime()),
                UpdateAt = Timestamp.FromDateTime(cs.UpdateAt!.Value.ToUniversalTime())
            }));
            return response;
        }

        public override async Task<CategorySize.CategorySize> GetOne(Id request, ServerCallContext context)
        {
            var data = await _repo.GetOne(request.SearchId);
            if (data == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Category Size not found"));
            }
            return new CategorySize.CategorySize
            {
                Id = data.Id,
                CategoryId = data.CategoryId,
                Size = data.Size,
                CreateAt = Timestamp.FromDateTime(data.CreateAt!.Value.ToUniversalTime()),
                UpdateAt = Timestamp.FromDateTime(data.UpdateAt!.Value.ToUniversalTime())
            };
        }

        public override async Task<Response> Create(CreateCategorySize request, ServerCallContext context)
        {
            var createCategorySize = new Domain.Requests.RequestCreateCategorySize
            {
                CategoryId = request.CategoryId,
                Size = request.Size
            };
            var response = await _repo.CreateCategorySize(createCategorySize);
            return new Response
            {
                StatusCode = response.StatusCode,
                Message = response.Message,
            };
        }

        public override async Task<Response> Update(CategorySize.CategorySize request, ServerCallContext context)
        {
            var updateCategorySize = new Domain.Requests.RequestUpdateCategorySize
            {
                Id = request.Id,
                CategoryId = request.CategoryId,
                Size = request.Size
            };
            var response = await _repo.UpdateCategorySize(updateCategorySize);
            return new Response
            {
                StatusCode = response.StatusCode,
                Message = response.Message,
            };
        }

        public override async Task<Response> Delete(Id request, ServerCallContext context)
        {
            var response = await _repo.DeleteCategorySize(request.SearchId);
            return new Response
            {
                StatusCode = response.StatusCode,
                Message = response.Message,
            };
        }

        public override async Task<CategorySizes> GetAllOfProduct(Id request, ServerCallContext context)
        {
            var listData = await _repo.GetAllOfProduct(request.SearchId);
            var response = new CategorySizes();
            response.Item.AddRange(listData.Select(cs => new CategorySize.CategorySize
            {
                Id = cs.Id,
                CategoryId = cs.CategoryId,
                Size = cs.Size,
                CreateAt = Timestamp.FromDateTime(cs.CreateAt!.Value.ToUniversalTime()),
                UpdateAt = Timestamp.FromDateTime(cs.UpdateAt!.Value.ToUniversalTime())
            }));
            return response;
        }

        public override async Task<CategorySizes> GetAllOfProductOption(Id request, ServerCallContext context)
        {
            var listData = await _repo.GetAllOfProductOption(request.SearchId);
            var response = new CategorySizes();
            response.Item.AddRange(listData.Select(cs => new CategorySize.CategorySize
            {
                Id = cs.Id,
                CategoryId = cs.CategoryId,
                Size = cs.Size,
                CreateAt = Timestamp.FromDateTime(cs.CreateAt!.Value.ToUniversalTime()),
                UpdateAt = Timestamp.FromDateTime(cs.UpdateAt!.Value.ToUniversalTime())
            }));
            return response;
        }
    }
}
