using Domain.Entities;
using Domain.Requests;
using Domain.Responses;
using Grpc.Core;
using GrpcServiceOrder.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GrpcServiceOrder.Data
{
    public class OrderTrackingRepository : IOrderTrackingRepository
    {
        private AppDbContext _context;
        private ILogger _logger;

        public OrderTrackingRepository(AppDbContext context, ILogger<OrderTrackingRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Response> CreateTracking(RequestCreateTracking createTracking)
        {
            try
            {
                if (!await _context.Orders.AnyAsync(o => o.Id == createTracking.OrderId))
                    throw new Exception("Order does not exist.");
                var tracking = new OrderTracking
                {
                    OrderId = createTracking.OrderId,
                    Location = createTracking.Location,
                    CourierInfo = createTracking.CourierInfo,
                    Status = createTracking.Status,
                    Message = createTracking.Message,
                    TimeTracking = createTracking.TimeTracking,
                    CreateAt = DateTime.Now
                };
                _context.OrderTrackings.Add(tracking);
                await _context.SaveChangesAsync();
                return new Response { Message = $"_id: {tracking.Id}", StatusCode = 201 };
            }
            catch (Exception err)
            {
                _logger.LogError($"Fail to create a order tracking \nError: {err.Message}");
                throw new RpcException(new Status(StatusCode.Internal, "Internal Error"));
            }
        }

        public async Task<IEnumerable<ResponseOrderTracking>> GetAllOfOrder(string orderId)
        {
            try
            {
                return await _context.OrderTrackings
                    .Where(o => o.OrderId == orderId)
                    .Select(o => new ResponseOrderTracking
                    {
                        Id = o.Id,
                        OrderId = o.OrderId,
                        CourierInfo = o.CourierInfo,
                        Location = o.Location,
                        Message = o.Message,
                        TimeTracking = o.TimeTracking,
                        Status = o.Status,
                        CreateAt = o.CreateAt,
                        UpdateAt = o.UpdateAt,
                    })
                    .ToListAsync();
            }
            catch (Exception err)
            {
                _logger.LogError($"Fail to get all tracking of a order - {orderId} \nError: {err.Message}");
                throw new RpcException(new Status(StatusCode.Internal, "Internal Error"));
            }
        }
    }
}
