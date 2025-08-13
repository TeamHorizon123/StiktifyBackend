using Microsoft.EntityFrameworkCore;
using StiktifyShop.Application.DTOs.Requests;
using StiktifyShop.Application.DTOs.Responses;
using StiktifyShop.Application.Interfaces;
using StiktifyShop.Application.Mapper;

namespace StiktifyShop.Infrastructure.Repository
{
    public class ProductSizeRepo : IProductSizeRepo
    {
        private AppDbContext _context;
        public ProductSizeRepo(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<Response> Create(CreateProductSize productSize)
        {
            try
            {
                var existingSize = await _context.ProductSizes
                    .FirstOrDefaultAsync(s => s.Size == productSize.Size);
                if (existingSize != null)
                    return new Response
                    {
                        StatusCode = 400,
                        Message = "Size already exists."
                    };
                var newSize = MapperSingleton<MapperProductSize>.Instance.MapCreate(productSize);
                _context.ProductSizes.Add(newSize);
                await _context.SaveChangesAsync();
                return new Response
                {
                    StatusCode = 201,
                    Message = "Product size created successfully.",
                    Data = new { Id = newSize.Id }
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
                var existingSize = await _context.ProductSizes
                    .FirstOrDefaultAsync(s => s.Id == variantId);
                if (existingSize == null)
                    return new Response
                    {
                        StatusCode = 404,
                        Message = "Product size not found."
                    };
                _context.ProductSizes.Remove(existingSize);
                await _context.SaveChangesAsync();
                return new Response
                {
                    StatusCode = 204,
                    Message = "Product size deleted successfully."
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

        public async Task<ResponseProductSize?> Get(string variantId)
        {
            try
            {
                var size = await _context.ProductSizes
                    .FirstOrDefaultAsync(s => s.Id == variantId);
                return size != null
                    ? MapperSingleton<MapperProductSize>.Instance.MapResponse(size)
                    : null;
            }
            catch (Exception err)
            {
                return null;
                throw new Exception(err.Message);
            }
        }

        public IQueryable<ResponseProductSize> GetAll()
        {
            try
            {
                var listSize = _context.ProductSizes
                    .Include(s => s.Category)
                    .Include(s => s.Variants)
                    .ToList();
                return listSize.Select(size
                    => MapperSingleton<MapperProductSize>.Instance.MapResponse(size)
                ).AsQueryable();
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

        public async Task<Response> Update(UpdateProductSize productSize)
        {
            try
            {
                var existingSize = await _context.ProductSizes
                    .FirstOrDefaultAsync(s => s.Id == productSize.Id);
                if (existingSize == null)
                    return new Response
                    {
                        StatusCode = 404,
                        Message = "Product size not found."
                    };

                existingSize.Size = productSize.Size;
                existingSize.CategoryId = productSize.CategoryId;
                existingSize.UpdatedAt = DateTime.Now;

                _context.ProductSizes.Update(existingSize);
                await _context.SaveChangesAsync();
                return new Response
                {
                    StatusCode = 200,
                    Message = "Product size updated successfully.",
                    Data = new { value = MapperSingleton<MapperProductSize>.Instance.MapResponse(existingSize) }
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
