using Domain.Requests;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcServiceOrder.Interfaces;
using GrpcServiceOrder.OrderDetails;

namespace GrpcServiceOrder.Services
{
    public class OrderDetailsGrpcService : OrderDetailGrpc.OrderDetailGrpcBase
    {
        private IOrderDetailRepository _repo;

        public OrderDetailsGrpcService(IOrderDetailRepository repo)
        {
            _repo = repo ?? throw new ArgumentException(nameof(_repo));
        }

        public override async Task<OrderDetails.OrderDetails> GetOne(Id request, ServerCallContext context)
        {
            var orderDetails = await _repo.GetOne(request.SearchId);
            if (orderDetails == null)
                return new OrderDetails.OrderDetails();
            return new OrderDetails.OrderDetails()
            {
                Id = orderDetails.Id,
                PurchaseMethod = orderDetails.PurchaseMethod,
                DateOfDelivery = Timestamp.FromDateTime(orderDetails.DateOfDelivery!.Value.ToUniversalTime()),
                DateOfShipping = Timestamp.FromDateTime(orderDetails.DateOfShipping!.Value.ToUniversalTime()),
                DateOfPurchase = Timestamp.FromDateTime(orderDetails.DateOfPurchase!.Value.ToUniversalTime()),
            };
        }

        public override async Task<Response> Create(CreateDetail request, ServerCallContext context)
        {
            var orderDetails = new RequestCreateOrderDetail
            {
                Id = request.Id,
                PurchaseMethod = request.PurchaseMethod,
                DateOfDelivery = request.DateOfDelivery.ToDateTime(),
                DateOfPurchase = request.DateOfPurchase.ToDateTime(),
                DateOfShipping = request.DateOfShipping.ToDateTime()
            };

            var response = await _repo.CreateDetail(orderDetails);
            return new Response { Message = response.Message, StatusCode = response.StatusCode };
        }

        public override async Task<Response> Update(OrderDetails.OrderDetails request, ServerCallContext context)
        {
            var orderDetails = new RequestUpdateOrderDetail
            {
                Id = request.Id,
                PurchaseMethod = request.PurchaseMethod,
                DateOfDelivery = request.DateOfDelivery.ToDateTime(),
                DateOfPurchase = request.DateOfPurchase.ToDateTime(),
                DateOfShipping = request.DateOfShipping.ToDateTime()
            };

            var response = await _repo.UpdateDetail(orderDetails);
            return new Response { Message = response.Message, StatusCode = response.StatusCode };
        }
    }
}
