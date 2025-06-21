using Domain.Requests;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcServiceOrder.Interfaces;
using GrpcServiceOrder.Tracking;

namespace GrpcServiceOrder.Services
{
    public class OrderTrackingGrpcService : OrderTrackingGrpc.OrderTrackingGrpcBase
    {
        private IOrderTrackingRepository _repo;

        public OrderTrackingGrpcService(IOrderTrackingRepository repo)
        {
            _repo = repo ?? throw new ArgumentException(nameof(_repo));
        }

        public override async Task<Trackings> GetAllOfOrder(Id request, ServerCallContext context)
        {
            var listTrackings = await _repo.GetAllOfOrder(request.SearchId);
            Trackings trackings = new Trackings();
            trackings.Item.AddRange(listTrackings.Select(tracking
                => new Tracking.Tracking
                {
                    Id = tracking.Id,
                    CourierInfo = tracking.CourierInfo,
                    Location = tracking.Location,
                    Message = tracking.Message,
                    OrderId = tracking.OrderId,
                    Status = tracking.Status,
                    TimeTracking = Timestamp.FromDateTime(tracking.TimeTracking!.Value.ToUniversalTime()),
                    CreateAt = Timestamp.FromDateTime(tracking.CreateAt!.Value.ToUniversalTime()),
                    UpdateAt = Timestamp.FromDateTime(tracking.UpdateAt!.Value.ToUniversalTime()),
                }));
            return trackings;
        }

        public override async Task<Response> Create(CreateTracking request, ServerCallContext context)
        {
            var createTracking = new RequestCreateTracking
            {
                OrderId = request.OrderId,
                CourierInfo = request.CourierInfo,
                Location = request.Location,
                Message = request.Message,
                Status = request.Status,
                TimeTracking = request.TimeTracking.ToDateTime()
            };
            var response = await _repo.CreateTracking(createTracking);
            return new Response { Message = response.Message, StatusCode = response.StatusCode };
        }
    }
}
