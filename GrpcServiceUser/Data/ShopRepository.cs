using Domain.Requests;
using Domain.Responses;
using GrpcServiceUser.Interface;
using Microsoft.EntityFrameworkCore;

namespace GrpcServiceUser.Data
{
    public class ShopRepository : IShopRepository
    {
        private AppDbContext _context;
        private IProductService _productService;

        public ShopRepository(AppDbContext context, IProductService productService)
        {
            _context = context ?? throw new ArgumentException(nameof(_context));
            _productService = productService ?? throw new ArgumentException(nameof(_productService));
        }

        private double AverageShopRating(string ShopId)
        {
            return _context.ShopRatings.Any(shopRating => shopRating.ShopId == ShopId) ?
                _context.ShopRatings
                .Where(shopRating => shopRating.ShopId == ShopId)
                .Average(shopRating => shopRating.Point)
                : 0;
        }

        public async Task<Response> CreateShop(RequestCreateShop shop)
        {
            try
            {
                var shopData = new Domain.Entities.Shop
                {
                    UserId = shop.UserId,
                    ShopName = shop.ShopName,
                    IsBanned = shop.IsBanned,
                    Location = shop.Location,
                    ShopType = shop.ShopType,
                    Avatar = shop.Avatar,
                    CreateAt = DateTime.Now
                };
                _context.Shops.Add(shopData);
                await _context.SaveChangesAsync();
                return new Response { Message = $"_id: {shopData.Id}", StatusCode = 201 };
            }
            catch (Exception err)
            {
                return new Response { Message = err.Message, StatusCode = 500 };
            }
        }

        public async Task<Response> DeleteShop(string shopId)
        {
            var shop = await GetOne(shopId);
            if (shop == null)
                return new Response { Message = "Shop does not exist.", StatusCode = 404 };
            try
            {
                var responseDeleteProduct = await _productService.DeleteAllOfShop(shopId);
                if (responseDeleteProduct.StatusCode != 204)
                    throw new Exception(responseDeleteProduct.Message);
                var shopData = new Domain.Entities.Shop
                {
                    Id = shop.Id!,
                    UserId = shop.UserId!,
                    Avatar = shop.Avatar!,
                    ShopName = shop.ShopName!,
                    ShopType = shop.ShopType!,
                    Location = shop.Location!,
                    IsBanned = shop.IsBanned,
                    CreateAt = shop.CreateAt!.Value,
                    UpdateAt = shop.UpdateAt!.Value,
                };
                _context.Shops.Remove(shopData);
                await _context.SaveChangesAsync();
                return new Response { StatusCode = 204 };
            }
            catch (Exception err)
            {
                return new Response { Message = err.Message, StatusCode = 500 };
            }
        }

        public async Task<IEnumerable<ResponseShop>> GetAll()
        {
            try
            {
                return await _context.Shops
                    .Where(s => s.IsBanned == false)
                    .Select(s => new ResponseShop
                    {
                        Id = s.Id,
                        ShopName = s.ShopName,
                        ShopType = s.ShopType,
                        IsBanned = s.IsBanned,
                        Avatar = s.Avatar,
                        Location = s.Location,
                        UserId = s.UserId,
                        CreateAt = s.CreateAt,
                        UpdateAt = s.UpdateAt,
                        Rating = Math.Round(AverageShopRating(s.Id), 1),
                    })
                    .ToListAsync();
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

        public async Task<ResponseShop?> GetOfUser(string userId)
        {
            try
            {
                return await _context.Shops
                    .Where(s => s.IsBanned == false && s.UserId == userId)
                    .Select(s => new ResponseShop
                    {
                        Id = s.Id,
                        ShopName = s.ShopName,
                        ShopType = s.ShopType,
                        IsBanned = s.IsBanned,
                        Avatar = s.Avatar,
                        Location = s.Location,
                        UserId = s.UserId,
                        CreateAt = s.CreateAt,
                        UpdateAt = s.UpdateAt,
                        Rating = AverageShopRating(s.Id),
                    })
                    .FirstOrDefaultAsync();
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

        public async Task<ResponseShop?> GetOne(string shopId)
        {
            try
            {
                return await _context.Shops
                    .Where(s => s.IsBanned == false && s.Id == shopId)
                    .Select(s => new ResponseShop
                    {
                        Id = s.Id,
                        ShopName = s.ShopName,
                        ShopType = s.ShopType,
                        IsBanned = s.IsBanned,
                        Avatar = s.Avatar,
                        Location = s.Location,
                        UserId = s.UserId,
                        CreateAt = s.CreateAt,
                        UpdateAt = s.UpdateAt,
                        Rating = Math.Round(AverageShopRating(s.Id), 1),
                    })
                    .FirstOrDefaultAsync();
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

        public async Task<Response> UpdateShop(RequestUpdateShop shop)
        {
            if (await GetOne(shop.Id) == null)
                return new Response { Message = "Shop does not exist.", StatusCode = 404 };
            try
            {
                var updateShop = new Domain.Entities.Shop
                {
                    Id = shop.Id,
                    ShopName = shop.ShopName,
                    Avatar = shop.Avatar,
                    IsBanned = shop.IsBanned,
                    Location = shop.Location,
                    ShopType = shop.ShopType,
                    UserId = shop.UserId,
                    CreateAt = shop.CreateAt
                };
                _context.Shops.Update(updateShop);
                await _context.SaveChangesAsync();
                return new Response { Message = $"_id: {shop.Id}", StatusCode = 200 };
            }
            catch (Exception err)
            {
                return new Response { Message = err.Message, StatusCode = 500 };
            }
        }
    }
}
