using Domain.Requests;
using Domain.Responses;
using StiktifyShopBackend.Interfaces;
using StiktifyShopBackend.PaymentMethod;

namespace StiktifyShopBackend.Providers
{
    public class PaymentMethodProvider : IPaymentMethodProvider
    {
        private PaymentMethodGrpc.PaymentMethodGrpcClient _client;

        public PaymentMethodProvider(PaymentMethodGrpc.PaymentMethodGrpcClient client)
        {
            _client = client ?? throw new ArgumentException(nameof(_client));
        }

        public async Task<Domain.Responses.Response> Create(RequestCreateMethod createMethod)
        {
            var createGrpc = new CreateMethod
            {
                Enable = createMethod.Enable,
                Name = createMethod.Name,
            };
            var response = await _client.CreateAsync(createGrpc);
            return new Domain.Responses.Response { Message = response.Message, StatusCode = response.StatusCode };
        }

        public IQueryable<ResponsePaymentMethod> GetAll()
        {
            var grpcList = _client.GetAll(new PaymentMethod.Empty());
            var list = grpcList.Item.Select(item => new ResponsePaymentMethod
            {
                Id = item.Id,
                Enable = item.Enable,
                Name = item.Name,
                CreateAt = item.CreateAt.ToDateTime(),
                UpdateAt = item.UpdateAt.ToDateTime(),
            });
            return list.AsQueryable();
        }

        public async Task<ResponsePaymentMethod?> GetOne(string methodId)
        {
            var grpcItem = await _client.GetOneAsync(new Id { SearchId = methodId });
            return new ResponsePaymentMethod
            {
                Id = grpcItem.Id,
                Enable = grpcItem.Enable,
                Name = grpcItem.Name,
                CreateAt = grpcItem.CreateAt.ToDateTime(),
                UpdateAt = grpcItem.UpdateAt.ToDateTime(),
            };
        }

        public async Task<Domain.Responses.Response> Update(RequestUpdateMethod updateMethod)
        {
            var updateGrpc = new PaymentMethod.PaymentMethod
            {
                Id = updateMethod.Id,
                Enable = updateMethod.Enable,
                Name = updateMethod.Name,
            };
            var response = await _client.UpdateAsync(updateGrpc);
            return new Domain.Responses.Response { Message = response.Message, StatusCode = response.StatusCode };
        }
    }
}
