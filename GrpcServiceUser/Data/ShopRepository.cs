using Domain.Requests;
using Domain.Responses;
using GrpcServiceUser.Interface;
using Microsoft.EntityFrameworkCore;
using static Google.Protobuf.Reflection.SourceCodeInfo.Types;
using System.Runtime.CompilerServices;

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
                var exist = await _context.Shops
                    .AnyAsync(s => s.ShopName.ToLower() == shop.ShopName.ToLower()
                    && s.Location.ToLower() == s.Location.ToLower());
                if (exist)
                    return new Response { Message = "Shop name has been exist.", StatusCode = 400 };

                exist = await _context.Shops.AnyAsync(s => s.UserId == shop.UserId);
                if (exist)
                    return new Response { Message = "This user account already has shop", StatusCode = 400 };
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
                var list = await _context.Shops
                    .ToListAsync();
                return list
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
                    .ToList();
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
                var shop = await _context.Shops
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
                        Rating = 0,
                    })
                    .FirstOrDefaultAsync();
                if (shop != null)
                    shop.Rating = Math.Round(AverageShopRating(shop.Id!), 1);
                return shop;
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
                var shop = await _context.Shops
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
                        Rating = 0,
                    })
                    .FirstOrDefaultAsync();
                if (shop != null)
                    shop.Rating = Math.Round(AverageShopRating(shop.Id!), 1);
                return shop;
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

                var exist = await _context.Shops.FindAsync(shop.Id);
                exist!.ShopName = shop.ShopName;
                exist!.Avatar = shop.Avatar;
                exist!.IsBanned = shop.IsBanned;
                exist!.Location = shop.Location;
                exist!.ShopType = shop.ShopType;
                _context.Shops.Update(exist);
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
