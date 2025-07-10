using Microsoft.EntityFrameworkCore;
using StiktifyShop.Application.DTOs.Requests;
using StiktifyShop.Application.DTOs.Responses;
using StiktifyShop.Application.Interfaces;
using StiktifyShop.Application.Mapper;

namespace StiktifyShop.Infrastructure.Repository
{
    public class OrderRepo : IOrderRepo
    {
        private AppDbContext _context;
        public OrderRepo(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<Response> Create(CreateOrder order)
        {
            try
            {
                var newOrder = MapperSingleton<MapperOrder>.Instance.MapCreate(order);
                _context.Orders.Add(newOrder);
                await _context.SaveChangesAsync();
                return new Response
                {
                    StatusCode = 201,
                    Message = "Order created successfully.",
                    Data = new { Id = newOrder.Id }
                };
            }
            catch (Exception err)
            {
                return new Response
                {
                    StatusCode = 500,
                    Message = err.Message
                };
            }
        }

        public async Task<Response> Update(UpdateOrder order)
        {
            try
            {
                var existOrder = await _context.Orders
                    .FirstOrDefaultAsync(o => o.Id == order.Id);
                if (existOrder == null)
                {
                    return new Response
                    {
                        StatusCode = 404,
                        Message = "Order not found."
                    };
                }
                _context.Orders.Update(existOrder);
                await _context.SaveChangesAsync();
                return new Response
                {
                    StatusCode = 204,
                    Message = "Order updated successfully."
                };
            }
            catch (Exception err)
            {
                return new Response
                {
                    StatusCode = 500,
                    Message = err.Message
                };
            }
        }

        public async Task<ResponseOrder?> Get(string orderId)
        {
            try
            {
                var order = await _context.Orders
                    .Include(o => o.OrderTrackings)
                    .Include(o => o.Product)
                    .Include(o => o.Address)
                    .FirstOrDefaultAsync(o => o.Id == orderId);
                return order == null
                    ? null
                    : MapperSingleton<MapperOrder>.Instance.MapResponse(order);
            }
            catch (Exception err)
            {
                return null;
                throw new Exception(err.Message);
            }
        }

        public IQueryable<ResponseOrder> GetAll()
        {
            try
            {
                var listOrder = _context.Orders
                    .Include(o => o.OrderTrackings)
                    .Select(order
                    => MapperSingleton<MapperOrder>.Instance.MapResponse(order))
                    .ToList();
                return listOrder.AsQueryable();
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }
    }
}
