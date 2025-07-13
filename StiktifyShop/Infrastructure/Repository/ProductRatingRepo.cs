using Microsoft.EntityFrameworkCore;
using StiktifyShop.Application.DTOs.Requests;
using StiktifyShop.Application.DTOs.Responses;
using StiktifyShop.Application.Interfaces;
using StiktifyShop.Application.Mapper;

namespace StiktifyShop.Infrastructure.Repository
{
    public class ProductRatingRepo : IProductRatingRepo
    {
        private AppDbContext _context;
        public ProductRatingRepo(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<Response> Create(CreateProducRating rating)
        {
            try
            {
                var existingRating = await _context.ProductRatings
                    .FirstOrDefaultAsync(r => r.ProductId == rating.ProductId && r.UserId == rating.UserId);
                if (existingRating != null)
                    return new Response
                    {
                        StatusCode = 409,
                        Message = "Rating already exists for this product by the user."
                    };
                var newRating = MapperSingleton<MapperProductRating>.Instance.MapCreate(rating);
                _context.ProductRatings.Add(newRating);
                await _context.SaveChangesAsync();
                return new Response
                {
                    StatusCode = 201,
                    Message = "Product rating created successfully.",
                    Data = new { Id = newRating.Id }
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

        public async Task<Response> Delete(string ratingId)
        {
            try
            {
                var existingRating = await _context.ProductRatings
                    .FirstOrDefaultAsync(r => r.Id == ratingId);
                if (existingRating == null)
                    return new Response
                    {
                        StatusCode = 404,
                        Message = "Product rating not found."
                    };
                _context.ProductRatings.Remove(existingRating);
                await _context.SaveChangesAsync();
                return new Response
                {
                    StatusCode = 204,
                    Message = "Product rating deleted successfully."
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

        public async Task<ResponseProductRating?> Get(string ratingId)
        {
            try
            {
                return await _context.ProductRatings
                    .Where(r => r.Id == ratingId)
                    .Select(r => MapperSingleton<MapperProductRating>.Instance.MapResponse(r))
                    .FirstOrDefaultAsync();
            }
            catch (Exception err)
            {
                return null;
                throw new Exception(err.Message);
            }
        }

        public IQueryable<ResponseProductRating> GetAll(string productId)
        {
            try
            {
                return _context.ProductRatings
                    .Where(r => r.ProductId == productId)
                    .Select(r => MapperSingleton<MapperProductRating>.Instance.MapResponse(r))
                    .AsQueryable();
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }
    }
}
