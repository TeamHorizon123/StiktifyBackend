using Microsoft.EntityFrameworkCore;
using StiktifyShop.Application.DTOs.Requests;
using StiktifyShop.Application.DTOs.Responses;
using StiktifyShop.Application.Interfaces;
using StiktifyShop.Application.Mapper;

namespace StiktifyShop.Infrastructure.Repository
{
    public class ProductRepo : IProductRepo
    {
        private AppDbContext _context;
        public ProductRepo(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        private double AveragePoint(string productId)
        {
            var points = _context.ProductRatings
                .Where(pr => pr.ProductId == productId)
                .Select(pr => pr.Point)
                .ToList();
            return points.Any() ? points.Average()
                : 0.0;
        }

        private int CountOrder(string productId)
        {
            return _context.OrderItems.Count(o => o.ProductId == productId);
        }

        private int CountRating(string productId)
        {
            return _context.ProductRatings.Count(pr => pr.ProductId == productId);
        }

        private string PriceRange(string productId)
        {
            var listOption = _context.ProductVariants
                .Where(option => option.ProductOption.ProductId == productId)
                .Select(option => option.Price)
                .ToList();

            if (listOption.Count == 0)
                return "NA";
            var minPrice = listOption.Min();
            var maxPrice = listOption.Max();
            return minPrice == maxPrice ? $"{minPrice}" : $"{minPrice} - {maxPrice}";
        }

        private double Price(string productId)
        {
            var listOption = _context.ProductVariants
                .Where(option => option.ProductOption.ProductId == productId)
                .Select(option => option.Price)
                .ToList();
            if (listOption.Count == 0)
                return 0;
            return listOption.Min();
        }

        public async Task<Response> Create(CreateProduct product)
        {
            try
            {
                var newProduct = MapperSingleton<MapperProduct>.Instance.MapCreate(product);
                _context.Products.Add(newProduct);
                await _context.SaveChangesAsync();
                return new Response
                {
                    StatusCode = 201,
                    Message = "Product created successfully.",
                    Data = new { Id = newProduct.Id }
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

        public async Task<Response> Delete(string productId)
        {
            try
            {
                var existingProduct = await _context.Products
                    .FirstOrDefaultAsync(p => p.Id == productId);
                if (existingProduct == null)
                    return new Response
                    {
                        StatusCode = 404,
                        Message = "Product not found."
                    };
                _context.Products.Remove(existingProduct);
                await _context.SaveChangesAsync();
                return new Response
                {
                    StatusCode = 204,
                    Message = "Product deleted successfully."
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

        public async Task<ResponseProduct?> Get(string productId)
        {
            try
            {
                var product = await _context.Products
                    .Include(p => p.Category)
                    .Include(p => p.Shop)
                    .FirstOrDefaultAsync(p => p.Id == productId);
                if (product == null)
                    return null;
                var responseProduct = MapperSingleton<MapperProduct>.Instance.MapResponse(product);
                responseProduct.AveragePoint = AveragePoint(productId);
                responseProduct.Order = CountOrder(productId);
                responseProduct.Price = Price(productId);
                responseProduct.PriceRange = PriceRange(productId);
                responseProduct.RateTurn = CountRating(productId);
                return responseProduct;
            }
            catch (Exception err)
            {
                return null;
                throw new Exception(err.Message);
            }
        }

        public IQueryable<ResponseProduct> GetAll()
        {
            try
            {
                var list = _context.Products
                    .Include(p => p.Shop)
                    .Include(p => p.Category)
                    .Include(p => p.ProductRatings)
                    .Include(p => p.ProductOptions)
                    .Select(product => MapperSingleton<MapperProduct>.Instance.MapResponse(product))
                    .ToList();
                return list.Select(product
                    =>
                {
                    product.AveragePoint = Math.Round(AveragePoint(product.Id!), 1);
                    product.Order = CountOrder(product.Id!);
                    product.Price = Price(product.Id!);
                    product.PriceRange = PriceRange(product.Id!);
                    product.RateTurn = CountRating(product.Id!);
                    return product;
                }
                ).AsQueryable();
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

        public async Task<Response> Update(UpdateProduct product)
        {
            try
            {
                var existProduct = await _context.Products
                    .FirstOrDefaultAsync(p => p.Id == product.Id);
                if (existProduct == null)
                    return new Response
                    {
                        StatusCode = 404,
                        Message = "Product not found."
                    };

                existProduct.Name = product.Name;
                existProduct.Description = product.Description;
                existProduct.CategoryId = product.CategoryId;
                existProduct.ImageUri = product.ImageUri;
                existProduct.ShopId = product.ShopId;
                existProduct.IsHidden = product.IsHidden;

                _context.Products.Update(existProduct);
                await _context.SaveChangesAsync();
                return new Response
                {
                    StatusCode = 200,
                    Message = "Product updated successfully.",
                    Data = new { value = existProduct }
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
