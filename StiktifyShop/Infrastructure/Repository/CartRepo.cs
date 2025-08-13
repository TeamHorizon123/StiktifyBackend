using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StiktifyShop.Application.DTOs.Requests;
using StiktifyShop.Application.DTOs.Responses;
using StiktifyShop.Application.Interfaces;
using StiktifyShop.Application.Mapper;
using StiktifyShop.Domain.Entity;

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
                    .FirstOrDefaultAsync(c => c.UserId == cart.UserId
                    && c.ProductId == cart.ProductId
                    && c.VariantId == cart.VariantId);

                if (existingCart != null)
                    return await Update(new UpdateCart
                    {
                        Id = existingCart.Id,
                        ProductId = existingCart.ProductId,
                        OptionId = existingCart.OptionId,
                        VariantId = existingCart.VariantId,
                        ImageUri = existingCart.ImageUri,
                        UserId = existingCart.UserId,
                        Quantity = existingCart.Quantity + cart.Quantity
                    });
                var newCart = MapperSingleton<MapperCart>.Instance.MapCreate(cart);
                _context.Carts.Add(newCart);
                await _context.SaveChangesAsync();
                return new Response
                {
                    StatusCode = 201,
                    Message = "Cart created successfully."
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

        public async Task<Response> DeleteMany(ICollection<DeleteCart> ids)
        {
            try
            {
                var list = new List<Cart>();
                foreach (var item in ids)
                {
                    var existingCart = await _context.Carts
                        .FirstOrDefaultAsync(c => c.Id == item.Id);
                    if (existingCart != null)
                        list.Add(existingCart);
                }
                if (list.IsNullOrEmpty())
                    return new Response
                    {
                        StatusCode = 404,
                        Message = "No carts found to delete."
                    };
                _context.Carts.RemoveRange(list);
                await _context.SaveChangesAsync();
                return new Response
                {
                    StatusCode = 200,
                    Message = "Carts deleted successfully."
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

        public IQueryable<ResponseCart> GetAll()
        {
            try
            {
                var listCart = _context.Carts
                    .Include(c => c.Product)
                    .Include(c => c.Option)
                    .Include(c => c.Variant)
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
                existingCart.ImageUri = cart.ImageUri;
                existingCart.ProductId = cart.ProductId;
                existingCart.OptionId = cart.OptionId;
                existingCart.VariantId = cart.VariantId;
                existingCart.UserId = cart.UserId;
                existingCart.UpdatedAt = DateTime.Now;

                _context.Carts.Update(existingCart);
                await _context.SaveChangesAsync();
                return new Response
                {
                    StatusCode = 200,
                    Message = "Cart updated successfully."
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
