using Microsoft.EntityFrameworkCore;
using StiktifyShop.Application.DTOs.Requests;
using StiktifyShop.Application.DTOs.Responses;
using StiktifyShop.Application.Interfaces;
using StiktifyShop.Application.Mapper;

namespace StiktifyShop.Infrastructure.Repository
{
    public class CartRepo : ICartRepo
    {
        private AppDbContext _context;
        public CartRepo(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Response> Create(CreateCart cart)
        {
            try
            {
                var existingCart = await _context.Carts
                    .FirstOrDefaultAsync(c => c.UserId == cart.UserId && c.ProductItemId == cart.ProductItemId);
                if (existingCart != null)
                    return await Update(new UpdateCart
                    {
                        Id = existingCart.Id,
                        ProductItemId = existingCart.ProductItemId,
                        UserId = existingCart.UserId,
                        Quantity = existingCart.Quantity + cart.Quantity
                    });
                var newCart = MapperSingleton<MapperCart>.Instance.MapCreate(cart);
                _context.Carts.Add(newCart);
                await _context.SaveChangesAsync();
                return new Response
                {
                    StatusCode = 201,
                    Message = "Cart created successfully.",
                    Data = new { Id = cart.ProductItemId }
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

        public async Task<Response> Delete(string cartId)
        {
            try
            {
                var existingCart = await _context.Carts
                    .FirstOrDefaultAsync(c => c.Id == cartId);
                if (existingCart == null)
                    return new Response
                    {
                        StatusCode = 404,
                        Message = "Cart not found."
                    };
                _context.Carts.Remove(existingCart);
                await _context.SaveChangesAsync();
                return new Response
                {
                    StatusCode = 204
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

        public IQueryable<ResponseCart> GetAll(string userId)
        {
            try
            {
                var listCart = _context.Carts
                    .Include(c => c.ProductItem)
                    .Select(cart => MapperSingleton<MapperCart>.Instance.MapResponse(cart))
                    .ToList();

                return listCart.AsQueryable();
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

        public async Task<Response> Update(UpdateCart cart)
        {
            try
            {
                var existingCart = await _context.Carts
                    .FirstOrDefaultAsync(c => c.Id == cart.Id);
                if (existingCart == null)
                    return new Response
                    {
                        StatusCode = 404,
                        Message = "Cart not found."
                    };
                existingCart.Quantity = cart.Quantity;
                existingCart.ProductItemId = cart.ProductItemId;
                existingCart.UserId = cart.UserId;
                existingCart.UpdatedAt = DateTime.Now;

                _context.Carts.Update(existingCart);
                await _context.SaveChangesAsync();
                return new Response
                {
                    StatusCode = 200,
                    Message = "Cart updated successfully.",
                    Data = new { value = MapperSingleton<MapperCart>.Instance.MapResponse(existingCart) }
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
    }
}
