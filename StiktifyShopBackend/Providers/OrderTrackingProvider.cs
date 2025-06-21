using Domain.Requests;
using Domain.Responses;
using Google.Protobuf.WellKnownTypes;
using StiktifyShopBackend.Interfaces;
using StiktifyShopBackend.Tracking;

namespace StiktifyShopBackend.Providers
{
    public class OrderTrackingProvider : IOrderTrackingProvider
    {
        private OrderTrackingGrpc.OrderTrackingGrpcClient _client;

        public OrderTrackingProvider(OrderTrackingGrpc.OrderTrackingGrpcClient client)
        {
            _client = client ?? throw new ArgumentException(nameof(_client));
        }

        public async Task<Domain.Responses.Response> CreateTracking(RequestCreateTracking createTracking)
        {
            var createGrpc = new CreateTracking
            {
                OrderId = createTracking.OrderId,
                CourierInfo = createTracking.CourierInfo,
                Location = createTracking.Location,
                Message = createTracking.Message,
                Status = createTracking.Status,
                TimeTracking = Timestamp.FromDateTime(createTracking.TimeTracking.ToUniversalTime()),
            };
            var response = await _client.CreateAsync(createGrpc);
            return new Domain.Responses.Response { Message = response.Message, StatusCode = response.StatusCode };
        }

        public IQueryable<ResponseOrderTracking> GetAllOfOrder(string orderId)
        {
            var grpcList = _client.GetAllOfOrder(new Id { SearchId = orderId });
            var list = grpcList.Item.Select(item => new ResponseOrderTracking
            {
                Id = item.Id,
                OrderId = item.Id,
                CourierInfo = item.CourierInfo,
                Location = item.Location,
                Message = item.Message,
                Status = item.Status,
                TimeTracking = item.TimeTracking.ToDateTime(),
                CreateAt = item.CreateAt.ToDateTime(),
                UpdateAt = item.UpdateAt.ToDateTime()
            });
            return list.AsQueryable();
        }
    }
}
