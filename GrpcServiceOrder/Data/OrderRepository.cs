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

        public OrderRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentException(nameof(_context));
        }

        public async Task<Response> CreateOrder(RequestCreateOrder createOrder)
        {
            try
            {
                var order = new Domain.Entities.Order
                {
                    UserId = createOrder.UserId,
                    AddressId = createOrder.AddressId,
                    Quantity = createOrder.Quantity,
                    Price = createOrder.Price,
                    ProductId = createOrder.ProductId,
                    ProductItemId = createOrder.ProductId,
                    ShippingFee = createOrder.ShippingFee,
                    Status = createOrder.Status
                };
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();
                return new Response { Message = order.Id, StatusCode = 201 };
            }
            catch (Exception err)
            {
                Console.WriteLine($"Fail to create order \nError: {err.Message}");
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
                        ProductId = o.ProductId,
                        ProductItemId = o.ProductItemId,
                        Quantity = o.Quantity,
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
                Console.WriteLine($"Fail to get all order \nError: {err.Message}");
                throw new RpcException(new Status(StatusCode.Internal, "Internal Error"));
            }
        }

        public async Task<IEnumerable<ResponseOrder>> GetAllOfProduct(string productId)
        {
            try
            {
                return await _context.Orders
                    .Where(o => o.ProductId == productId)
                    .Select(o => new ResponseOrder
                    {
                        Id = o.Id,
                        AddressId = o.AddressId,
                        UserId = o.UserId,
                        ProductId = o.ProductId,
                        ProductItemId = o.ProductItemId,
                        Quantity = o.Quantity,
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
                Console.WriteLine($"Fail to get all order of product-{productId} \nError: {err.Message}");
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
                        ProductId = o.ProductId,
                        ProductItemId = o.ProductItemId,
                        Quantity = o.Quantity,
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
                Console.WriteLine($"Fail to get all order of user-{userId} \nError: {err.Message}");
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
                        ProductId = o.ProductId,
                        ProductItemId = o.ProductItemId,
                        Quantity = o.Quantity,
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
                Console.WriteLine($"Fail to get a order - {orderId} \nError: {err.Message}");
                throw new RpcException(new Status(StatusCode.Internal, "Internal Error"));
            }
        }

        public async Task<Response> UpdateOrder(RequestUpdateOrder updateOrder)
        {
            try
            {
                var order = await _context.Orders.FindAsync(updateOrder.Id);
                if (order == null)
                    return new Response { Message = "Order not found", StatusCode = 404 };

                order.Status = updateOrder.Status;

                _context.Orders.Update(order);
                await _context.SaveChangesAsync();
                return new Response { Message = order.Id, StatusCode = 200 };
            }
            catch (Exception err)
            {
                Console.WriteLine($"Fail to update order \nError: {err.Message}");
                throw new RpcException(new Status(StatusCode.Internal, "Internal Error"));
            }
        }
    }
}
