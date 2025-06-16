using Domain.Requests;
using Domain.Responses;
using GrpcServiceUser.Interface;
using Microsoft.EntityFrameworkCore;

namespace GrpcServiceUser.Data
{
    public class ShopRatingRepository : IShopRatingRepository
    {
        private AppDbContext _context;

        public ShopRatingRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentException(nameof(_context));
        }

        public async Task<Response> CreateShopRating(RequestCreateShopRating createShopRating)
        {
            try
            {
                var shopRating = new Domain.Entities.ShopRating
                {
                    UserId = createShopRating.UserId,
                    ShopId = createShopRating.ShopId,
                    Content = createShopRating.Content,
                    Point = createShopRating.Point,
                    CreateAt = DateTime.Now,
                };
                _context.ShopRatings.Add(shopRating);
                await _context.SaveChangesAsync();
                return new Response { Message = $"_id: {shopRating.Id}", StatusCode = 201 };
            }
            catch (Exception err)
            {
                return new Response { Message = err.Message, StatusCode = 500 };
            }
        }

        public async Task<Response> DeleteShopRating(string shopId)
        {
            if (await GetOne(shopId) == null)
                return new Response { Message = "Shop rating does not exist.", StatusCode = 404 };
            try
            {
                var shopRating = await _context.ShopRatings.FindAsync(shopId);
                _context.ShopRatings.Remove(shopRating!);
                await _context.SaveChangesAsync();
                return new Response { StatusCode = 204 };
            }
            catch (Exception err)
            {
                return new Response
                {
                    Message = err.Message,
                    StatusCode = 500
                };
            }
        }

        public async Task<IEnumerable<ResponseShopRating>> GetAllOfShop(string shopId)
        {
            try
            {
                return await _context.ShopRatings
                    .Where(sr => sr.ShopId == shopId)
                    .Select(sr => new ResponseShopRating
                    {
                        Id = sr.ShopId,
                        UserId = sr.UserId,
                        ShopId = sr.ShopId,
                        Content = sr.Content,
                        Point = sr.Point,
                        CreateAt = sr.CreateAt,
                        UpdateAt = sr.UpdateAt
                    })
                    .ToListAsync();
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

        public async Task<ResponseShopRating?> GetOne(string ratingId)
        {
            try
            {
                return await _context.ShopRatings
                    .Where(sr => sr.Id == ratingId)
                    .Select(sr => new ResponseShopRating
                    {
                        Id = sr.ShopId,
                        UserId = sr.UserId,
                        ShopId = sr.ShopId,
                        Content = sr.Content,
                        Point = sr.Point,
                        CreateAt = sr.CreateAt,
                        UpdateAt = sr.UpdateAt
                    })
                    .FirstOrDefaultAsync();
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

        public async Task<Response> UpdateShopRating(RequestUpdateShopRating updateShopRating)
        {
            if (await GetOne(updateShopRating.Id) == null)
                return new Response { Message = "Shop rating does not exist.", StatusCode = 404 };
            try
            {
                var shopRating = new Domain.Entities.ShopRating
                {
                    Id = updateShopRating.Id,
                    ShopId = updateShopRating.ShopId,
                    Content = updateShopRating.Content,
                    Point = updateShopRating.Point,
                    UpdateAt = DateTime.Now
                };
                _context.ShopRatings.Update(shopRating);
                await _context.SaveChangesAsync();
                return new Response { Message = $"_id: {updateShopRating.Id}", StatusCode = 200 };
            }
            catch (Exception err)
            {
                return new Response { Message = err.Message, StatusCode = 500 };
            }
        }
    }
}
