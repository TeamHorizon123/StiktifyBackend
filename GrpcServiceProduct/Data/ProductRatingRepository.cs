using Domain.Requests;
using Domain.Responses;
using Grpc.Core;
using GrpcServiceProduct.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GrpcServiceProduct.Data
{
    public class ProductRatingRepository : IProductRatingRepository
    {
        private AppDbContext _context;

        public ProductRatingRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentException(nameof(_context));
        }

        public async Task<Response> CreateRating(RequestCreateRating createRating)
        {
            try
            {
                var createData = new Domain.Entities.ProductRating
                {
                    ProductId = createRating.ProductId,
                    ProductItemId = createRating.ProductItemId,
                    Content = createRating.Content,
                    UserId = createRating.UserId,
                    Image = createRating.Image,
                    Point = createRating.Point
                };
                _context.ProductRatings.Add(createData);
                await _context.SaveChangesAsync();
                return new Response { Message = createData.Id, StatusCode = 201 };
            }
            catch (Exception err)
            {
                Console.WriteLine($"Fail to add new product rating \nError: {err.Message}");
                return new Response { Message = "Internal Server Error", StatusCode = 500 };
            }
        }

        public async Task<Response> DeleteRating(string productRatingId)
        {
            try
            {
                var removeRating = await _context.ProductRatings.FindAsync(productRatingId);
                if (removeRating == null)
                    return new Response { Message = "Product rating does not exist.", StatusCode = 404 };

                _context.ProductRatings.Remove(removeRating);
                await _context.SaveChangesAsync();
                return new Response { StatusCode = 204 };
            }
            catch (Exception err)
            {
                Console.WriteLine($"Fail to remove a product rating \nError: {err.Message}");
                return new Response { Message = "Internal Server Error", StatusCode = 500 };
            }
        }

        public async Task<IEnumerable<ResponseProductRating>> GetAll()
        {
            try
            {
                return await _context.ProductRatings
                    .Select(rating => new ResponseProductRating
                    {
                        Id = rating.Id,
                        Content = rating.Content,
                        UserId = rating.UserId,
                        Image = rating.Image,
                        Point = rating.Point,
                        ProductItemId = rating.ProductItemId,
                        ProductId = rating.ProductId,
                        CreateAt = rating.CreateAt,
                        UpdateAt = rating.UpdateAt
                    })
                    .ToListAsync();
            }
            catch (Exception err)
            {
                Console.WriteLine($"Fail to get all product rating \nError: {err.Message}");
                throw new RpcException(new Status(StatusCode.Internal, "Internal Error"));
            }
        }

        public async Task<IEnumerable<ResponseProductRating>> GetAllOfOption(string itemId)
        {
            try
            {
                return await _context.ProductRatings
                    .Where(rating => rating.ProductItemId == itemId)
                    .Select(rating => new ResponseProductRating
                    {
                        Id = rating.Id,
                        Content = rating.Content,
                        UserId = rating.UserId,
                        Image = rating.Image,
                        ProductItemId = rating.ProductItemId,
                        Point = rating.Point,
                        ProductId = rating.ProductId,
                        CreateAt = rating.CreateAt,
                        UpdateAt = rating.UpdateAt
                    })
                    .ToListAsync();
            }
            catch (Exception err)
            {
                Console.WriteLine($"Fail to get all product rating of option has id-{itemId} \nError: {err.Message}");
                throw new RpcException(new Status(StatusCode.Internal, "Internal Error"));
            }
        }

        public async Task<IEnumerable<ResponseProductRating>> GetAllOfProduct(string productId)
        {
            try
            {
                return await _context.ProductRatings
                    .Where(rating => rating.ProductId == productId)
                    .Select(rating => new ResponseProductRating
                    {
                        Id = rating.Id,
                        Content = rating.Content,
                        UserId = rating.UserId,
                        Image = rating.Image,
                        ProductItemId = rating.ProductItemId,
                        Point = rating.Point,
                        ProductId = rating.ProductId,
                        CreateAt = rating.CreateAt,
                        UpdateAt = rating.UpdateAt
                    })
                    .ToListAsync();
            }
            catch (Exception err)
            {
                Console.WriteLine($"Fail to get all product rating of product has id-{productId} \nError: {err.Message}");
                throw new RpcException(new Status(StatusCode.Internal, "Internal Error"));
            }
        }

        public async Task<ResponseProductRating?> GetOne(string ratingId)
        {
            try
            {
                return await _context.ProductRatings
                    .Where(rating => rating.Id == ratingId)
                    .Select(rating => new ResponseProductRating
                    {
                        Id = rating.Id,
                        Content = rating.Content,
                        UserId = rating.UserId,
                        Image = rating.Image,
                        ProductItemId = rating.ProductItemId,
                        Point = rating.Point,
                        ProductId = rating.ProductId,
                        CreateAt = rating.CreateAt,
                        UpdateAt = rating.UpdateAt
                    })
                    .FirstOrDefaultAsync();
            }
            catch (Exception err)
            {
                Console.WriteLine($"Fail to get a product rating has id-{ratingId} \nError: {err.Message}");
                throw new RpcException(new Status(StatusCode.Internal, "Internal Error"));
            }
        }

        public async Task<Response> UpdateRating(RequestUpdateRating updateRating)
        {
            try
            {
                var updateData = await _context.ProductRatings.FindAsync(updateRating.Id);
                if (updateData == null)
                    return new Response { Message = "Product rating does not exist.", StatusCode = 404 };
                updateData.ProductId = updateRating.ProductId;
                updateData.ProductItemId = updateRating.ProductItemId;
                updateData.Content = updateRating.Content;
                updateData.UserId = updateRating.UserId;
                updateData.Image = updateRating.Image;
                updateData.Point = updateRating.Point;
                updateData.UpdateAt = DateTime.Now;
                _context.ProductRatings.Update(updateData);
                await _context.SaveChangesAsync();
                return new Response { Message = updateData.Id, StatusCode = 200 };
            }
            catch (Exception err)
            {
                Console.WriteLine($"Fail to update product rating \nError: {err.Message}");
                return new Response { Message = "Internal Server Error", StatusCode = 500 };
            }
        }
    }
}
