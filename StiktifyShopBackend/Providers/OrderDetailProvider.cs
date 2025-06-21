using Domain.Requests;
using Domain.Responses;
using Google.Protobuf.WellKnownTypes;
using StiktifyShopBackend.Interfaces;
using StiktifyShopBackend.OrderDetails;

namespace StiktifyShopBackend.Providers
{
    public class OrderDetailProvider : IOrderDetailProvider
    {
        private OrderDetailGrpc.OrderDetailGrpcClient _client;

        public OrderDetailProvider(OrderDetailGrpc.OrderDetailGrpcClient client)
        {
            _client = client ?? throw new ArgumentException(nameof(_client));
        }

        public async Task<Domain.Responses.Response> CreateDetail(RequestCreateOrderDetail createOrderDetail)
        {
            var createGrpc = new CreateDetail
            {
                PurchaseMethod = createOrderDetail.PurchaseMethod,
                DateOfDelivery = Timestamp.FromDateTime(createOrderDetail.DateOfDelivery),
                DateOfPurchase = Timestamp.FromDateTime(createOrderDetail.DateOfPurchase),
                DateOfShipping = Timestamp.FromDateTime(createOrderDetail.DateOfShipping),
            };
            var response = await _client.CreateAsync(createGrpc);
            return new Domain.Responses.Response { Message = response.Message, StatusCode = response.StatusCode };
        }

        public async Task<ResponseOrderDetail?> GetOne(string detailsId)
        {
            var detailGrpc = await _client.GetOneAsync(new Id { SearchId = detailsId });
            return new ResponseOrderDetail
            {
                Id = detailGrpc.Id,
                PurchaseMethod = detailGrpc.PurchaseMethod,
                DateOfDelivery = detailGrpc.DateOfDelivery.ToDateTime(),
                DateOfPurchase = detailGrpc.DateOfPurchase.ToDateTime(),
                DateOfShipping = detailGrpc.DateOfShipping.ToDateTime(),
            };
        }

        public async Task<Domain.Responses.Response> UpdateDetail(RequestUpdateOrderDetail updateOrderDetail)
        {
            var updateGrpc = new OrderDetails.OrderDetails
            {
                Id = updateOrderDetail.Id,
                PurchaseMethod = updateOrderDetail.PurchaseMethod,
                DateOfDelivery = Timestamp.FromDateTime(updateOrderDetail.DateOfDelivery),
                DateOfPurchase = Timestamp.FromDateTime(updateOrderDetail.DateOfPurchase),
                DateOfShipping = Timestamp.FromDateTime(updateOrderDetail.DateOfShipping),
            };
            var response = await _client.UpdateAsync(updateGrpc);
            return new Domain.Responses.Response { Message = response.Message, StatusCode = response.StatusCode };
        }
    }
}
