using Domain.Entities;
using Domain.Requests;
using Domain.Responses;
using Grpc.Core;
using GrpcServiceProduct.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GrpcServiceProduct.Data
{
    public class ProductRepository : IProductRepository
    {
        private AppDbContext _context;
        private ILogger _logger;

        public ProductRepository(AppDbContext context, ILogger logger)
        {
            _context = context ?? throw new ArgumentException(nameof(_context));
            _logger = logger ?? throw new ArgumentException(nameof(_logger));
        }

        private double AverageProductRating(string productId)
        {
            return _context.ProductRatings.Any(rating => rating.ProductId == productId) ?
                _context.ProductRatings.Where(rating => rating.ProductId == productId)
                .Average(rating => rating.Point) : 0;
        }

        public async Task<Response> Create(RequestCreateProduct createProduct)
        {
            try
            {
                var listCategory = new List<Domain.Entities.Category>();
                foreach (var item in createProduct.CategoryId)
                {
                    var category = await _context.Categories.FindAsync(item);
                    listCategory.Add(category!);
                }

                var product = new Domain.Entities.Product
                {
                    Name = createProduct.Name,
                    Description = createProduct.Description,
                    Discount = createProduct.Discount,
                    Price = createProduct.Price,
                    ShopId = createProduct.ShopId,
                    Thumbnail = createProduct.Thumbnail,
                    Categories = listCategory,
                    CreateAt = DateTime.Now,
                };
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return new Response { StatusCode = 201, Message = $"_id: {product.Id}" };
            }
            catch (Exception err)
            {
                _logger.LogError($"Fail to add a new product \nError: {err.Message}");
                return new Response { StatusCode = 500, Message = "Fail to add a new product" };
            }
        }

        public async Task<Response> Delete(string productId)
        {
            try
            {
                var product = await _context.Products.FindAsync(productId);
                if (product == null)
                    return new Response { StatusCode = 404, Message = "Product does not exist." };

                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                return new Response { StatusCode = 204 };
            }
            catch (Exception err)
            {
                _logger.LogError($"Fail to delete a product \nError: {err.Message}");
                return new Response { StatusCode = 500, Message = "Fail to delete a product" };
            }
        }

        public async Task<Response> DeleteMany(string[] listProductId)
        {
            try
            {
                var listProduct = await _context.Products
                    .Where(product => listProductId.Contains(product.Id))
                    .ToListAsync();

                if (listProduct == null)
                    return new Response { Message = "Nothing in list to delete.", StatusCode = 404 };

                _context.Products.RemoveRange(listProduct);
                await _context.SaveChangesAsync();
                return new Response { StatusCode = 204 };
            }
            catch (Exception err)
            {
                _logger.LogError($"Fail to delete a product \nError: {err.Message}");
                return new Response { StatusCode = 500, Message = "Fail to delete a product" };
            }
        }

        public async Task<IEnumerable<ResponseProduct>> GetAll()
        {
            try
            {
                return await _context.Products
                    .Select(
                    product => new ResponseProduct
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Description = product.Description,
                        Discount = product.Discount,
                        Thumbnail = product.Thumbnail,
                        Price = product.Price,
                        ShopId = product.ShopId,
                        Order = 0,
                        Rating = Math.Round(AverageProductRating(product.Id), 1),
                        CreateAt = product.CreateAt,
                        UpdateAt = product.UpdateAt,
                    })
                    .ToListAsync();
            }
            catch (Exception err)
            {
                _logger.LogError($"Fail to get all product \nError: {err.Message}");
                throw new RpcException(new Status(StatusCode.Internal, "Internal Error"));
            }
        }

        public async Task<IEnumerable<string>> GetAllImage(string productId)
        {
            try
            {
                var product = await _context.Products
                    .Where(product => product.Id == productId)
                    .Select(product
                    => new
                    {
                        product.Thumbnail,
                        optionsImage = product.Options.Select(option => option.Image)
                    })
                    .FirstOrDefaultAsync();

                return product == null ? Enumerable.Empty<string>() : new[] { product.Thumbnail }.Concat(product.optionsImage);
            }
            catch (Exception err)
            {
                _logger.LogError($"Fail to get all product \nError: {err.Message}");
                throw new RpcException(new Status(StatusCode.Internal, "Internal Error"));
            }
        }

        public async Task<IEnumerable<ResponseProduct>> GetAllOfCategory(string categoryId)
        {
            try
            {
                return await _context.Products
                    .Where(product => product.Categories.Any(category => category.Id == categoryId))
                    .Select(
                    product => new ResponseProduct
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Description = product.Description,
                        Discount = product.Discount,
                        Thumbnail = product.Thumbnail,
                        Price = product.Price,
                        ShopId = product.ShopId,
                        Order = 0,
                        Rating = Math.Round(AverageProductRating(product.Id), 1),
                        CreateAt = product.CreateAt,
                        UpdateAt = product.UpdateAt,
                    })
                    .ToListAsync();
            }
            catch (Exception err)
            {
                _logger.LogError($"Fail to get all product of a category has id-{categoryId} \nError: {err.Message}");
                throw new RpcException(new Status(StatusCode.Internal, "Internal Error"));
            }
        }

        public async Task<IEnumerable<ResponseProduct>> GetAllOfShop(string shopId)
        {
            try
            {
                return await _context.Products
                    .Where(product => product.ShopId == shopId)
                    .Select(
                    product => new ResponseProduct
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Description = product.Description,
                        Discount = product.Discount,
                        Thumbnail = product.Thumbnail,
                        Price = product.Price,
                        ShopId = product.ShopId,
                        Order = 0,
                        Rating = Math.Round(AverageProductRating(product.Id), 1),
                        CreateAt = product.CreateAt,
                        UpdateAt = product.UpdateAt,
                    })
                    .ToListAsync();
            }
            catch (Exception err)
            {
                _logger.LogError($"Fail to get all product of a shop has id-{shopId} \nError: {err.Message}");
                throw new RpcException(new Status(StatusCode.Internal, "Internal Error"));
            }
        }

        public async Task<ResponseProduct?> GetOne(string productId)
        {
            try
            {
                return await _context.Products
                    .Where(product => product.Id == productId)
                    .Select(
                    product => new ResponseProduct
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Description = product.Description,
                        Discount = product.Discount,
                        Thumbnail = product.Thumbnail,
                        Price = product.Price,
                        ShopId = product.ShopId,
                        Order = 0,
                        Rating = Math.Round(AverageProductRating(product.Id), 1),
                        CreateAt = product.CreateAt,
                        UpdateAt = product.UpdateAt,
                    })
                    .FirstOrDefaultAsync();
            }
            catch (Exception err)
            {
                _logger.LogError($"Fail to get a product has id-{productId} \nError: {err.Message}");
                throw new RpcException(new Status(StatusCode.Internal, "Internal Error"));
            }
        }

        public async Task<Response> Update(RequestUpdateProduct updateProduct)
        {
            try
            {
                if (await GetOne(updateProduct.Id) == null)
                    return new Response { StatusCode = 404, Message = "Product does not exist." };

                var listCategory = new List<Domain.Entities.Category>();
                foreach (var item in updateProduct.CategoryId)
                {
                    var category = await _context.Categories.FindAsync(item);
                    listCategory.Add(category!);
                }

                var product = new Domain.Entities.Product
                {
                    Id = updateProduct.Id,
                    Name = updateProduct.Name,
                    Description = updateProduct.Description,
                    Discount = updateProduct.Discount,
                    Price = updateProduct.Price,
                    ShopId = updateProduct.ShopId,
                    Thumbnail = updateProduct.Thumbnail,
                    Categories = listCategory,
                };
                _context.Products.Update(product);
                await _context.SaveChangesAsync();
                return new Response { StatusCode = 200, Message = $"_id: {product.Id}" };
            }
            catch (Exception err)
            {
                _logger.LogError($"Fail to update a product \nError: {err.Message}");
                return new Response { StatusCode = 500, Message = "Fail to update a product" };
            }
        }
    }
}
