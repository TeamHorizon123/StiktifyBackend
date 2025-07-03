using Domain.Requests;
using Domain.Responses;
using Grpc.Core;
using GrpcServiceOrder.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GrpcServiceOrder.Data
{
    public class OrderRepository : IOrderRepository
    {
        private AppDbContext _context;
        private ILogger _logger;

        public OrderRepository(AppDbContext context, ILogger<OrderRepository> logger)
        {
            _context = context ?? throw new ArgumentException(nameof(_context));
            _logger = logger ?? throw new ArgumentException(nameof(_logger));
        }

        public async Task<Response> CreateOrder(RequestCreateOrder createOrder)
        {
            try
            {
                var order = new Domain.Entities.Order
                {
                    UserId = createOrder.UserId,
                    AddressId = createOrder.AddressId,
                    OptionSizeColorId = createOrder.SizeColor,
                    Quantity = createOrder.Quantity,
                    Price = createOrder.Price,
                    Discount = createOrder.Discount,
                    ShippingFee = createOrder.ShippingFee,
                    Status = createOrder.Status
                };
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();
                return new Response { Message = $"_id: {order.Id}", StatusCode = 201 };
            }
            catch (Exception err)
            {
                _logger.LogError($"Fail to create order \nError: {err.Message}");
                throw new RpcException(new Status(StatusCode.Internal, "Internal Error"));
            }
        }

        public async Task<IEnumerable<ResponseOrder>> GetAll()
        {
            try
            {
                return await _context.Orders
                    .Select(o => new ResponseOrder
                    {
                        Id = o.Id,
                        AddressId = o.AddressId,
                        UserId = o.UserId,
                        SizeColorId = o.OptionSizeColorId,
                        Quantity = o.Quantity,
                        Discount = o.Discount,
                        Price = o.Price,
                        ShippingFee = o.ShippingFee,
                        Status = o.Status,
                        CreateAt = o.CreateAt,
                        UpdateAt = o.UpdateAt,
                    })
                    .ToListAsync();
            }
            catch (Exception err)
            {
                _logger.LogError($"Fail to get all order \nError: {err.Message}");
                throw new RpcException(new Status(StatusCode.Internal, "Internal Error"));
            }
        }

        public async Task<IEnumerable<ResponseOrder>> GetAllOfProduct(string productId)
        {
            try
            {
                return await _context.Orders
                    .Where(o => o.OptionSizeColorId == productId)
                    .Select(o => new ResponseOrder
                    {
                        Id = o.Id,
                        AddressId = o.AddressId,
                        UserId = o.UserId,
                        SizeColorId = o.OptionSizeColorId,
                        Quantity = o.Quantity,
                        Discount = o.Discount,
                        Price = o.Price,
                        ShippingFee = o.ShippingFee,
                        Status = o.Status,
                        CreateAt = o.CreateAt,
                        UpdateAt = o.UpdateAt,
                    })
                    .ToListAsync();
            }
            catch (Exception err)
            {
                _logger.LogError($"Fail to get all order of product-{productId} \nError: {err.Message}");
                throw new RpcException(new Status(StatusCode.Internal, "Internal Error"));
            }
        }

        public Task<IEnumerable<ResponseOrder>> GetAllOfShop(string shopId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ResponseOrder>> GetAllOfUser(string userId)
        {
            try
            {
                return await _context.Orders
                    .Where(o => o.UserId == userId)
                    .Select(o => new ResponseOrder
                    {
                        Id = o.Id,
                        AddressId = o.AddressId,
                        UserId = o.UserId,
                        SizeColorId = o.OptionSizeColorId,
                        Quantity = o.Quantity,
                        Discount = o.Discount,
                        Price = o.Price,
                        ShippingFee = o.ShippingFee,
                        Status = o.Status,
                        CreateAt = o.CreateAt,
                        UpdateAt = o.UpdateAt,
                    })
                    .ToListAsync();
            }
            catch (Exception err)
            {
                _logger.LogError($"Fail to get all order of user-{userId} \nError: {err.Message}");
                throw new RpcException(new Status(StatusCode.Internal, "Internal Error"));
            }
        }

        public async Task<ResponseOrder?> GetOne(string orderId)
        {
            try
            {
                return await _context.Orders
                    .Where(o => o.Id == orderId)
                    .Select(o => new ResponseOrder
                    {
                        Id = o.Id,
                        AddressId = o.AddressId,
                        UserId = o.UserId,
                        SizeColorId = o.OptionSizeColorId,
                        Quantity = o.Quantity,
                        Discount = o.Discount,
                        Price = o.Price,
                        ShippingFee = o.ShippingFee,
                        Status = o.Status,
                        CreateAt = o.CreateAt,
                        UpdateAt = o.UpdateAt,
                    })
                    .FirstOrDefaultAsync();
            }
            catch (Exception err)
            {
                _logger.LogError($"Fail to get a order - {orderId} \nError: {err.Message}");
                throw new RpcException(new Status(StatusCode.Internal, "Internal Error"));
            }
        }

        public async Task<Response> UpdateOrder(RequestUpdateOrder updateOrder)
        {
            try
            {
                if (await GetOne(updateOrder.Id) == null)
                    return new Response { Message = "Order does not exist.", StatusCode = 404 };
                var order = new Domain.Entities.Order
                {
                    Id = updateOrder.Id,
                    UserId = updateOrder.UserId,
                    AddressId = updateOrder.AddressId,
                    OptionSizeColorId = updateOrder.ProductId,
                    Quantity = updateOrder.Quantity,
                    Discount = updateOrder.Discount,
                    Price = updateOrder.Price,
                    ShippingFee = updateOrder.ShippingFee,
                    Status = updateOrder.Status
                };
                _context.Orders.Update(order);
                await _context.SaveChangesAsync();
                return new Response { Message = $"_id: {order.Id}", StatusCode = 200 };
            }
            catch (Exception err)
            {
                _logger.LogError($"Fail to get all cart \nError: {err.Message}");
                throw new RpcException(new Status(StatusCode.Internal, "Internal Error"));
            }
        }
    }
}
