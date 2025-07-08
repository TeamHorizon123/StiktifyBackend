using Domain.Requests;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcServiceProduct.Interfaces;
using GrpcServiceProduct.ProductOption;
using Newtonsoft.Json;

namespace GrpcServiceProduct.Services
{
    public class ProductOptionGrpcServie : ProductOptionGrpc.ProductOptionGrpcBase
    {
        private IProductOptionRepository _repo;

        public ProductOptionGrpcServie(IProductOptionRepository repo)
        {
            _repo = repo ?? throw new ArgumentException(nameof(_repo));
        }

        public override async Task<Options> GetAll(ProductOption.Empty request, ServerCallContext context)
        {
            var listOption = await _repo.GetAll();
            Options grpcList = new Options();
            grpcList.Item.AddRange(listOption.Select(option
                => new ProductOption.Option
                {
                    Id = option.Id,
                    ProductId = option.ProductId,
                    Image = option.Image,
                    Color = option.Color,
                    Type = option.Type,
                    Sizes =JsonConvert.SerializeObject(option.Sizes),
                    CreateAt = Timestamp.FromDateTime(option.CreateAt!.Value.ToUniversalTime()),
                    UpdateAt = Timestamp.FromDateTime(option.UpdateAt!.Value.ToUniversalTime()),
                }));
            return grpcList;
        }

        public override async Task<Options> GetAllOfProduct(Id request, ServerCallContext context)
        {
            var listOption = await _repo.GetAllOfProduct(request.SearchID);
            Options grpcList = new Options();
            grpcList.Item.AddRange(listOption.Select(option
                => new ProductOption.Option
                {
                    Id = option.Id,
                    ProductId = option.ProductId,
                    Image = option.Image,
                    Type = option.Type,
                    Color = option.Color,
                    Sizes = JsonConvert.SerializeObject(option.Sizes),
                    CreateAt = Timestamp.FromDateTime(option.CreateAt!.Value.ToUniversalTime()),
                    UpdateAt = Timestamp.FromDateTime(option.UpdateAt!.Value.ToUniversalTime()),
                }));
            return grpcList;
        }

        public override async Task<ProductOption.Option> GetOne(Id request, ServerCallContext context)
        {
            var option = await _repo.GetOne(request.SearchID);
            if (option == null)
                return new ProductOption.Option();
            return new ProductOption.Option
            {
                Id = option.Id,
                ProductId = option.ProductId,
                Image = option.Image,
                Color = option.Color,
                Type = option.Type,
                Sizes = JsonConvert.SerializeObject(option.Sizes),
                CreateAt = Timestamp.FromDateTime(option.CreateAt!.Value.ToUniversalTime()),
                UpdateAt = Timestamp.FromDateTime(option.UpdateAt!.Value.ToUniversalTime()),
            };
        }

        public override async Task<Response> Create(CreateOption request, ServerCallContext context)
        {
            var option = new RequestCreateOption()
            {
                ProductId = request.ProductId,
                Image = request.Image,
                Color = request.Color,
                Type = request.Type,
                CategorySizes = request.Sizes != null ?
                JsonConvert.DeserializeObject<ICollection<Domain.Entities.CategorySize>>(request.Sizes) : null
            };
            var response = await _repo.CreateProductOption(option);
            return new Response { Message = response.Message, StatusCode = response.StatusCode };
        }

        public override async Task<Response> CreateManyOption(CreateOptions request, ServerCallContext context)
        {
            var createOptions = request.Item.Select(option
                => new RequestCreateOption
                {
                    ProductId = option.ProductId,
                    Image = option.Image,
                    Color = option.Color,
                    Type = option.Type,
                    CategorySizes = option.Sizes != null ?
                        JsonConvert.DeserializeObject<ICollection<Domain.Entities.CategorySize>>(option.Sizes) : null
                }).ToList();
            var response = await _repo.CreateManyProductOption(createOptions);
            return new Response { Message = response.Message, StatusCode = response.StatusCode };
        }

        public override async Task<Response> UpdateOption(ProductOption.Option request, ServerCallContext context)
        {
            var updateOption = new RequestUpdateOption()
            {
                Id = request.ProductId,
                Image = request.Image,
                ProductId = request.ProductId,
                Color = request.Color,
                Type = request.Type,
                CategorySizes = request.Sizes != null ?
                    JsonConvert.DeserializeObject<ICollection<Domain.Entities.CategorySize>>(request.Sizes) : null
            };
            var response = await _repo.UpdateProductOption(updateOption);
            return new Response { Message = response.Message, StatusCode = response.StatusCode };
        }

        public override async Task<Response> DeleteOption(Id request, ServerCallContext context)
        {
            var response = await _repo.DeleteProductOption(request.SearchID);
            return new Response { Message = response.Message, StatusCode = response.StatusCode };
        }

        public override async Task<Response> DeleteManyOption(Ids request, ServerCallContext context)
        {
            var listRemove = request.Item.Select(optionId
                 => optionId.SearchID).ToList();
            var response = await _repo.DeleteManyProductOption(listRemove);
            return new Response { Message = response.Message, StatusCode = response.StatusCode };
        }
    }
}
