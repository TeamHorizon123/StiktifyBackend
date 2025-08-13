using Microsoft.EntityFrameworkCore;
using StiktifyShop.Application.DTOs.Requests;
using StiktifyShop.Application.DTOs.Responses;
using StiktifyShop.Application.Interfaces;
using StiktifyShop.Application.Mapper;

namespace StiktifyShop.Infrastructure.Repository
{
    public class ShopRatingRepo : IShopRatingRepo
    {
        private AppDbContext _context;

        public ShopRatingRepo(AppDbContext context)
        {
            _context = context ?? throw new ArgumentException(nameof(context));
        }

        public async Task<Response> Create(CreateShopRating shopRating)
        {
            try
            {
                var existingRating = await _context.ShopRatings
                    .Where(sr => sr.ShopId == shopRating.ShopId && sr.UserId == shopRating.UserId)
                    .FirstOrDefaultAsync();
                if (existingRating != null)
                    return new Response
                    {
                        StatusCode = 409,
                        Message = "Rating already exists for this shop by the user."
                    };
                var newRating = MapperSingleton<MapperShopRating>.Instance.MapCreate(shopRating);
                _context.ShopRatings.Add(newRating);
                await _context.SaveChangesAsync();

                return new Response
                {
                    StatusCode = 201,
                    Message = "Shop rating is created successfully.",
                    Data = new { Id = newRating.Id }
                };
            }
            catch (Exception err)
            {

                return new Response
                {
                    StatusCode = 500,
                    Message = err.Message,
                };
            }
        }

        public async Task<Response> Delete(string shopId)
        {
            try
            {
                var exist = await _context.ShopRatings.FirstOrDefaultAsync(sr => sr.Id == shopId);
                if (exist == null)
                    return new Response
                    {
                        StatusCode = 404,
                        Message = "Shop rating does not be found."
                    };
                _context.ShopRatings.Remove(exist);
                await _context.SaveChangesAsync();
                return new Response
                {
                    StatusCode = 204,
                    Message = "Shop rating is deleted successfully."
                };
            }
            catch (Exception err)
            {
                return new Response
                {
                    StatusCode = 500,
                    Message = err.Message,
                };
            }
        }

        public async Task<ResponseShopRating?> Get(string shopId)
        {
            try
            {
                var shopRating = await _context.ShopRatings
                    .Include(r => r.Shop)
                    .Where(sr => sr.Id == shopId)
                    .FirstOrDefaultAsync();
                return shopRating == null ? null : MapperSingleton<MapperShopRating>.Instance.MapResponse(shopRating);
            }
            catch (Exception err)
            {
                return null;
                throw new Exception(err.Message);
            }
        }

        public IQueryable<ResponseShopRating> GetAll(string shopId)
        {
            try
            {
                var listRating = _context.ShopRatings
                    .Include(sr => sr.Shop)
                    .Where(sr => sr.Id == shopId)
                    .Select(rating => MapperSingleton<MapperShopRating>.Instance.MapResponse(rating))
                    .ToList();
                return listRating.AsQueryable();
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }
    }
}
