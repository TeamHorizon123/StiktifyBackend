using Domain.Requests;
using Domain.Responses;
using Grpc.Core;
using GrpcServiceOrder.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GrpcServiceOrder.Data
{
    public class CartRepository : ICartRepository
    {
        private AppDbContext _context;
        private ILogger _logger;

        public CartRepository(AppDbContext context, ILogger<CartRepository> logger)
        {
            _context = context ?? throw new ArgumentException(nameof(_context));
            _logger = logger ?? throw new ArgumentException(nameof(_logger));
        }

        public async Task<Response> CreateCart(RequestCreateCart createCart)
        {
            try
            {
                var exist = await _context.Carts
                    .Where(cart => cart.UserId == createCart.UserId && cart.OptionId == createCart.OptionId)
                    .FirstOrDefaultAsync();

                if (exist != null)
                {
                    exist.Quantity += createCart.Quantity;
                    return await UpdateCart(new RequestUpdateCart
                    {
                        Id = exist.Id,
                        UserId = createCart.UserId,
                        ProductId = createCart.ProductId,
                        OptionId = createCart.OptionId,
                        Quantity = exist.Quantity
                    });
                }

                var cart = new Domain.Entities.Cart
                {
                    UserId = createCart.UserId,
                    Quantity = createCart.Quantity,
                    OptionId = createCart.OptionId,
                    ProductId = createCart.ProductId,
                    CreateAt = DateTime.Now,
                };
                _context.Carts.Add(cart);
                await _context.SaveChangesAsync();
                return new Response { Message = $"_id: {cart.Id}", StatusCode = 201 };
            }
            catch (Exception err)
            {
                _logger.LogError($"Fail to add a cart \nError: {err.Message}");
                throw new RpcException(new Status(StatusCode.Internal, "Internal Error"));
            }
        }

        public async Task<Response> DeleteCart(string cartId)
        {
            try
            {
                var exist = await _context.Carts
                    .Where(cart => cart.Id == cartId)
                    .FirstOrDefaultAsync();

                if (exist == null)
                    return new Response { StatusCode = 404, Message = "Cart does not exist" };

                _context.Carts.Remove(exist);
                await _context.SaveChangesAsync();
                return new Response { StatusCode = 204 };
            }
            catch (Exception err)
            {
                _logger.LogError($"Fail to delete a cart \nError: {err.Message}");
                throw new RpcException(new Status(StatusCode.Internal, "Internal Error"));
            }
        }

        public async Task<Response> DeleteManyCart(ICollection<string> ids)
        {
            try
            {
                var listProduct = await _context.Carts
                    .Where(cart => ids.Contains(cart.Id))
                    .ToListAsync();

                if (listProduct == null)
                    return new Response { StatusCode = 404, Message = "Nothing in list to delete." };

                _context.Carts.RemoveRange(listProduct);
                await _context.SaveChangesAsync();
                return new Response { StatusCode = 204 };
            }
            catch (Exception err)
            {
                _logger.LogError($"Fail to delete a list of cart \nError: {err.Message}");
                throw new RpcException(new Status(StatusCode.Internal, "Internal Error"));
            }
        }

        public async Task<IEnumerable<ResponseCart>> GetAll()
        {
            try
            {
                return await _context.Carts
                    .Select(
                    c => new ResponseCart
                    {
                        Id = c.Id,
                        UserId = c.UserId,
                        OptionId = c.OptionId,
                        ProductId = c.ProductId,
                        Quantity = c.Quantity,
                        CreateAt = c.CreateAt,
                        UpdateAt = c.UpdateAt,
                    })
                    .ToListAsync();
            }
            catch (Exception err)
            {
                _logger.LogError($"Fail to get all cart \nError: {err.Message}");
                throw new RpcException(new Status(StatusCode.Internal, "Internal Error"));
            }
        }

        public async Task<IEnumerable<ResponseCart>> GetAllOfProduct(string productID)
        {
            try
            {
                return await _context.Carts
                    .Where(cart => cart.ProductId == productID)
                    .Select(
                    c => new ResponseCart
                    {
                        Id = c.Id,
                        UserId = c.UserId,
                        OptionId = c.OptionId,
                        ProductId = c.ProductId,
                        Quantity = c.Quantity,
                        CreateAt = c.CreateAt,
                        UpdateAt = c.UpdateAt,
                    })
                    .ToListAsync();
            }
            catch (Exception err)
            {
                _logger.LogError($"Fail to get all cart of user has id-{productID} \nError: {err.Message}");
                throw new RpcException(new Status(StatusCode.Internal, "Internal Error"));
            }
        }

        public async Task<IEnumerable<ResponseCart>> GetAllOfUser(string userID)
        {
            try
            {
                return await _context.Carts
                    .Where(cart => cart.UserId == userID)
                    .Select(
                    c => new ResponseCart
                    {
                        Id = c.Id,
                        UserId = c.UserId,
                        OptionId = c.OptionId,
                        ProductId = c.ProductId,
                        Quantity = c.Quantity,
                        CreateAt = c.CreateAt,
                        UpdateAt = c.UpdateAt,
                    })
                    .ToListAsync();
            }
            catch (Exception err)
            {
                _logger.LogError($"Fail to get all cart of user has id-{userID} \nError: {err.Message}");
                throw new RpcException(new Status(StatusCode.Internal, "Internal Error"));
            }
        }

        public async Task<ResponseCart?> GetOne(string cartId)
        {
            try
            {
                return await _context.Carts
                    .Where(cart => cart.Id == cartId)
                    .Select(cart => new ResponseCart
                    {
                        Id = cart.Id,
                        UserId = cart.UserId,
                        OptionId = cart.OptionId,
                        ProductId = cart.ProductId,
                        Quantity = cart.Quantity,
                        CreateAt = cart.CreateAt,
                        UpdateAt = cart.UpdateAt,
                    })
                    .FirstOrDefaultAsync();
            }
            catch (Exception err)
            {
                _logger.LogError($"Fail to get a cart has id-{cartId} \nError: {err.Message}");
                throw new RpcException(new Status(StatusCode.Internal, "Internal Error"));
            }
        }

        public async Task<Response> UpdateCart(RequestUpdateCart updateCart)
        {
            try
            {
                if (await GetOne(updateCart.Id) == null)
                    return new Response { Message = "Cart does not exist.", StatusCode = 404 };
                var cart = new Domain.Entities.Cart
                {
                    Id = updateCart.Id,
                    UserId = updateCart.UserId,
                    ProductId= updateCart.ProductId,
                    Quantity = updateCart.Quantity,
                    OptionId = updateCart.OptionId
                };
                _context.Carts.Update(cart);
                await _context.SaveChangesAsync();
                return new Response { Message = $"_id: {cart.Id}", StatusCode = 200 };
            }
            catch (Exception err)
            {
                _logger.LogError($"Fail to update a cart \nError: {err.Message}");
                throw new RpcException(new Status(StatusCode.Internal, "Internal Error"));
            }
        }
    }
}
