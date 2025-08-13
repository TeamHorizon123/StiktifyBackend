using Microsoft.EntityFrameworkCore;
using StiktifyShop.Application.DTOs.Requests;
using StiktifyShop.Application.DTOs.Responses;
using StiktifyShop.Application.Interfaces;
using StiktifyShop.Application.Mapper;

namespace StiktifyShop.Infrastructure.Repository
{
    public class ProductVariantRepo : IProductVariantRepo
    {
        private AppDbContext _context;
        public ProductVariantRepo(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Response> Create(CreateProductVariant variant)
        {
            try
            {
                var exist = await _context.ProductVariants
                    .AnyAsync(v => v.ProductOptionId == variant.ProductOptionId && v.SizeId == variant.SizeId);
                if (exist)
                    return new Response
                    {
                        StatusCode = 409,
                        Message = "Variant with the same option and size already exists."
                    };
                var newVariant = MapperSingleton<MapperProductVariant>.Instance.MapCreate(variant);
                _context.ProductVariants.Add(newVariant);
                await _context.SaveChangesAsync();
                return new Response
                {
                    StatusCode = 201,
                    Message = "Product variant created successfully.",
                    Data = new { Id = newVariant.Id }
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

        public async Task<Response> Delete(string variantId)
        {
            try
            {
                var existingVariant = await _context.ProductVariants
                    .FirstOrDefaultAsync(v => v.Id == variantId);
                if (existingVariant == null)
                    return new Response
                    {
                        StatusCode = 404,
                        Message = "Product variant not found."
                    };
                _context.ProductVariants.Remove(existingVariant);
                await _context.SaveChangesAsync();
                return new Response
                {
                    StatusCode = 204,
                    Message = "Product variant deleted successfully."
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

        public async Task<ResponseProductVariant?> Get(string variantId)
        {
            try
            {
                return await _context.ProductVariants
                    .Include(v => v.ProductSize)
                    .Include(v => v.ProductOption)
                    .Where(v => v.Id == variantId)
                    .Select(v => MapperSingleton<MapperProductVariant>.Instance.MapResponse(v))
                    .FirstOrDefaultAsync();
            }
            catch (Exception err)
            {
                return null;
                throw new Exception(err.Message);
            }
        }

        public IQueryable<ResponseProductVariant> GetAll()
        {
            try
            {
                var listVariant = _context.ProductVariants
                    .Include(v => v.ProductSize)
                    .Include(v => v.ProductOption)
                    .Select(v => MapperSingleton<MapperProductVariant>.Instance.MapResponse(v))
                    .ToList();
                return listVariant.AsQueryable();
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

        public async Task<Response> Update(UpdateProductVariant variant)
        {
            try
            {
                var existingVariant = await _context.ProductVariants
                    .FirstOrDefaultAsync(v => v.Id == variant.Id);
                if (existingVariant == null)
                    return new Response
                    {
                        StatusCode = 404,
                        Message = "Product variant not found."
                    };

                existingVariant.Quantity = variant.Quantity;
                existingVariant.Price = variant.Price;
                existingVariant.UpdatedAt = DateTime.Now;

                _context.ProductVariants.Update(existingVariant);
                await _context.SaveChangesAsync();
                return new Response
                {
                    StatusCode = 200,
                    Message = "Product variant updated successfully.",
                    Data = new { value = existingVariant }
                };
            }
            catch (Exception err)
            {
                return new Response
                {
                    StatusCode = 500,
                    Message = err.ToString()
                };
            }
        }
    }
}
