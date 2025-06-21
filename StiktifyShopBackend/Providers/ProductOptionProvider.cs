using Domain.Requests;
using Domain.Responses;

using StiktifyShopBackend.Interfaces;
using StiktifyShopBackend.ProductOption;

namespace StiktifyShopBackend.Providers
{
    public class ProductOptionProvider : IProductOptionProvider
    {
        private ProductOptionGrpc.ProductOptionGrpcClient _client;

        public ProductOptionProvider(ProductOptionGrpc.ProductOptionGrpcClient client)
        {
            _client = client ?? throw new ArgumentException(nameof(_client));
        }

        public async Task<Domain.Responses.Response> CreateManyProductOption(ICollection<RequestCreateOption> createOption)
        {
            var listCreate = new CreateOptions();
            listCreate.Item.AddRange(createOption.Select(option => new CreateOption
            {
                ProductId = option.ProductId,
                Image = option.Image,
                Attribute = option.Attribute,
                Quantity = option.Quantity,
                Value = option.Value
            }));
            var response = await _client.CreateManyOptionAsync(listCreate);
            return new Domain.Responses.Response { Message = response.Message, StatusCode = response.StatusCode };
        }

        public async Task<Domain.Responses.Response> CreateProductOption(RequestCreateOption createOption)
        {
            var createGrpc = new CreateOption
            {
                ProductId = createOption.ProductId,
                Image = createOption.Image,
                Attribute = createOption.Attribute,
                Quantity = createOption.Quantity,
                Value = createOption.Value
            };
            var response = await _client.CreateAsync(createGrpc);
            return new Domain.Responses.Response { Message = response.Message, StatusCode = response.StatusCode };
        }

        public async Task<Domain.Responses.Response> DeleteManyProductOption(ICollection<string> listOptionId)
        {
            var listRemove = new Ids();
            listRemove.Item.AddRange(listOptionId.Select(id => new Id { SearchID = id }));
            var response = await _client.DeleteManyOptionAsync(listRemove);
            return new Domain.Responses.Response { Message = response.Message, StatusCode = response.StatusCode };
        }

        public async Task<Domain.Responses.Response> DeleteProductOption(string optionId)
        {
            var response = await _client.DeleteOptionAsync(new Id { SearchID = optionId });
            return new Domain.Responses.Response { Message = response.Message, StatusCode = response.StatusCode };
        }

        public IQueryable<ResponseProductOption> GetAll()
        {
            var listGrpc = _client.GetAll(new ProductOption.Empty());
            var listOption = listGrpc.Item.Select(item => new ResponseProductOption
            {
                Id = item.Id,
                Attribute = item.Attribute,
                Image = item.Image,
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                Value = item.Value,
                CreateAt = item.CreateAt.ToDateTime(),
                UpdateAt = item.UpdateAt.ToDateTime(),
            });
            return listOption.AsQueryable();
        }

        public IQueryable<ResponseProductOption> GetAllOfProduct(string productId)
        {
            var listGrpc = _client.GetAllOfProduct(new Id() { SearchID = productId });
            var listOption = listGrpc.Item.Select(item => new ResponseProductOption
            {
                Id = item.Id,
                Attribute = item.Attribute,
                Image = item.Image,
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                Value = item.Value,
                CreateAt = item.CreateAt.ToDateTime(),
                UpdateAt = item.UpdateAt.ToDateTime(),
            });
            return listOption.AsQueryable();
        }

        public async Task<ResponseProductOption?> GetOne(string optionId)
        {
            var optionGrpc = await _client.GetOneAsync(new Id { SearchID = optionId });
            return new ResponseProductOption
            {
                Id = optionGrpc.Id,
                Attribute = optionGrpc.Attribute,
                Image = optionGrpc.Image,
                ProductId = optionGrpc.ProductId,
                Quantity = optionGrpc.Quantity,
                Value = optionGrpc.Value,
                CreateAt = optionGrpc.CreateAt.ToDateTime(),
                UpdateAt = optionGrpc.UpdateAt.ToDateTime(),
            };
        }

        public async Task<Domain.Responses.Response> UpdateProductOption(RequestUpdateOption updateOption)
        {
            var updateGrpc = new ProductOption.Option
            {
                Id = updateOption.Id,
                Attribute = updateOption.Attribute,
                Image = updateOption.Image,
                ProductId = updateOption.ProductId,
                Quantity = updateOption.Quantity,
                Value = updateOption.Value
            };
            var response = await _client.UpdateOptionAsync(updateGrpc);
            return new Domain.Responses.Response { Message = response.Message, StatusCode = response.StatusCode };
        }
    }
}
