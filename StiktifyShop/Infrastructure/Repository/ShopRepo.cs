using Microsoft.EntityFrameworkCore;
using StiktifyShop.Application.DTOs.Requests;
using StiktifyShop.Application.DTOs.Responses;
using StiktifyShop.Application.Interfaces;
using StiktifyShop.Application.Mapper;

namespace StiktifyShop.Infrastructure.Repository
{
    public class ShopRepo : IShopRepo
    {
        private AppDbContext _context;
        public ShopRepo(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        private double AveragePoint(string shopId)
        {
            var points = _context.ShopRatings
                .Where(sr => sr.ShopId == shopId)
                .Select(sr => sr.Point)
                .ToList();
            return points.Any()
                ? points.Average()
                : 0.0;
        }

        public async Task<Response> Create(CreateShop shop)
        {
            try
            {
                var existingShop = await _context.Shops
                    .FirstOrDefaultAsync(s => s.UserId == shop.UserId);
                if (existingShop != null)
                    return new Response
                    {
                        StatusCode = 400,
                        Message = "User already have store."
                    };

                var newShop = MapperSingleton<MapperShop>.Instance.MapCreate(shop);
                _context.Shops.Add(newShop);
                await _context.SaveChangesAsync();
                return new Response
                {
                    StatusCode = 201,
                    Message = "Shop created successfully.",
                    Data = new { Id = newShop.Id }
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

        public async Task<Response> Delete(string shopId)
        {
            try
            {
                var shop = await _context.Shops
                    .FirstOrDefaultAsync(s => s.Id == shopId);
                if (shop == null)
                    return new Response
                    {
                        StatusCode = 404,
                        Message = "Shop not found."
                    };
                _context.Shops.Remove(shop);
                await _context.SaveChangesAsync();
                return new Response
                {
                    StatusCode = 204,
                    Message = "Shop deleted successfully."
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

        public async Task<ResponseShop?> Get(string shopId)
        {
            try
            {
                var shop = await _context.Shops
                    .Include(s => s.ShopRatings)
                    .FirstOrDefaultAsync(s => s.Id == shopId);
                if (shop == null)
                    return null;

                var responseShop = MapperSingleton<MapperShop>.Instance.MapResponse(shop);
                responseShop.AveragePoint = Math.Round(AveragePoint(shopId), 1);
                return responseShop;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

        public IQueryable<ResponseShop> GetAll()
        {
            try
            {
                var list = _context.Shops
                    .Include(s => s.ShopRatings)
                    .Select(shop => MapperSingleton<MapperShop>.Instance.MapResponse(shop))
                    .ToList();
                return list.Select(shop =>
                {
                    shop.AveragePoint = Math.Round(AveragePoint(shop.Id!), 1);
                    return shop;
                }).AsQueryable();
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

        public async Task<Response> Update(UpdateShop shop)
        {
            try
            {
                var existingShop = await _context.Shops
                    .FirstOrDefaultAsync(s => s.Id == shop.Id);
                if (existingShop == null)
                    return new Response
                    {
                        StatusCode = 404,
                        Message = "Shop not found."
                    };

                existingShop.ShopName = shop.ShopName;
                existingShop.Description = shop.Description;
                existingShop.AvatarUri = shop.AvatarUri;
                existingShop.Email = shop.Email;
                existingShop.Phone = shop.Phone;
                existingShop.Status = shop.Status;
                existingShop.Address = shop.Address;
                existingShop.ShopType = shop.ShopType;
                existingShop.UpdatedAt = DateTime.Now;

                _context.Shops.Update(existingShop);
                await _context.SaveChangesAsync();
                return new Response
                {
                    StatusCode = 200,
                    Message = "Shop updated successfully.",
                    Data = new { value = MapperSingleton<MapperShop>.Instance.MapResponse(existingShop) }
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
