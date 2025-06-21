using Domain.Requests;
using Domain.Responses;
using Grpc.Core;
using GrpcServiceOrder.Interfaces;
using GrpcServiceOrder.Order;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.Xml;

namespace GrpcServiceOrder.Data
{
    public class OrderDetailsRepository : IOrderDetailRepository
    {
        private AppDbContext _context;
        private ILogger _logger;

        public OrderDetailsRepository(AppDbContext context, ILogger<OrderDetailsRepository> logger)
        {
            _context = context ?? throw new ArgumentException(nameof(_context));
            _logger = logger ?? throw new ArgumentException(nameof(_logger));
        }

        public async Task<Domain.Responses.Response> CreateDetail(RequestCreateOrderDetail createOrderDetail)
        {
            try
            {
                var order = await _context.Orders.FindAsync(createOrderDetail.Id);
                if (order == null)
                    throw new Exception("Order does not exist.");
                var orderDetails = new Domain.Entities.OrderDetail
                {
                    Id = createOrderDetail.Id,
                    PurchaseMethod = createOrderDetail.PurchaseMethod,
                    DateOfDelivery = createOrderDetail.DateOfDelivery,
                    DateOfPurchase = createOrderDetail.DateOfPurchase,
                    DateOfShipping = createOrderDetail.DateOfShipping,
                    CreateAt = DateTime.Now
                };
                _context.OrderDetails.Add(orderDetails);
                await _context.SaveChangesAsync();
                return new Domain.Responses.Response { Message = $"_id: {orderDetails.Id}", StatusCode = 201 };
            }
            catch (Exception err)
            {
                _logger.LogError($"Fail to create order details \nError: {err.Message}");
                throw new RpcException(new Status(StatusCode.Internal, "Internal Error"));
            }
        }

        public Task<IEnumerable<ResponseOrderDetail>> GetAllOfOrder(string orderId)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseOrderDetail?> GetOne(string detailsId)
        {
            try
            {
                return await _context.OrderDetails
                    .Where(details => details.Id == detailsId)
                    .Select(orderDetail => new ResponseOrderDetail
                    {
                        Id = orderDetail.Id,
                        DateOfDelivery = orderDetail.DateOfDelivery,
                        DateOfPurchase = orderDetail.DateOfPurchase,
                        DateOfShipping = orderDetail.DateOfShipping,
                        PurchaseMethod = orderDetail.PurchaseMethod
                    }).FirstOrDefaultAsync();
            }
            catch (Exception err)
            {
                _logger.LogError($"Fail to get a order detail \nError: {err.Message}");
                throw new RpcException(new Status(StatusCode.Internal, "Internal Error"));
            }
        }

        public async Task<Domain.Responses.Response> UpdateDetail(RequestUpdateOrderDetail updateOrderDetail)
        {
            try
            {
                var exist = await _context.OrderDetails.FindAsync(updateOrderDetail.Id);
                if (exist == null)
                    return new Domain.Responses.Response { Message = "Order details does not exist.", StatusCode = 404 };

                exist.PurchaseMethod = updateOrderDetail.PurchaseMethod;
                exist.DateOfPurchase = updateOrderDetail.DateOfPurchase;
                exist.DateOfDelivery = updateOrderDetail.DateOfDelivery;
                exist.DateOfShipping = updateOrderDetail.DateOfShipping;
                exist.UpdateAt = DateTime.Now;
                _context.OrderDetails.Update(exist);
                await _context.SaveChangesAsync();
                return new Domain.Responses.Response { Message = $"_id: {exist.Id}", StatusCode = 200 };
            }
            catch (Exception err)
            {
                _logger.LogError($"Fail to get a order detail \nError: {err.Message}");
                throw new RpcException(new Status(StatusCode.Internal, "Internal Error"));
            }
        }
    }
}
