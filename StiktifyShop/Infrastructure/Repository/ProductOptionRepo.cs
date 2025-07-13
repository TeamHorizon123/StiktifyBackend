using Microsoft.EntityFrameworkCore;
using StiktifyShop.Application.DTOs.Requests;
using StiktifyShop.Application.DTOs.Responses;
using StiktifyShop.Application.Interfaces;
using StiktifyShop.Application.Mapper;

namespace StiktifyShop.Infrastructure.Repository
{
    public class ProductOptionRepo : IProductOptionRepo
    {
        private AppDbContext _context;
        public ProductOptionRepo(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<Response> Create(CreateProductOption productOption)
        {
            try
            {
                var newOption = MapperSingleton<MapperProductOption>.Instance.MapCreate(productOption);
                _context.ProductOptions.Add(newOption);
                await _context.SaveChangesAsync();
                return new Response
                {
                    StatusCode = 201,
                    Message = "Product option created successfully.",
                    Data = new { Id = newOption.Id }
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

        public async Task<Response> Delete(string optionId)
        {
            try
            {
                var existingOption = await _context.ProductOptions
                    .FirstOrDefaultAsync(o => o.Id == optionId);
                if (existingOption == null)
                    return new Response
                    {
                        StatusCode = 404,
                        Message = "Product option not found."
                    };
                _context.ProductOptions.Remove(existingOption);
                await _context.SaveChangesAsync();
                return new Response
                {
                    StatusCode = 204,
                    Message = "Product option deleted successfully."
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

        public async Task<ResponseProductOption?> Get(string optionId)
        {
            try
            {
                var option = await _context.ProductOptions
                    .Include(o => o.ProductVariants)
                    .FirstOrDefaultAsync(o => o.Id == optionId);
                if (option == null)
                    return null;
                return MapperSingleton<MapperProductOption>.Instance.MapResponse(option);
            }
            catch (Exception err)
            {
                return null;
                throw new Exception(err.Message);
            }
        }

        public IQueryable<ResponseProductOption> GetAll()
        {
            try
            {
                var listOption = _context.ProductOptions
                    .Include(o => o.ProductVariants)
                    .Select(option
                    => MapperSingleton<MapperProductOption>.Instance.MapResponse(option))
                    .ToList();
                return listOption.AsQueryable();
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

        public async Task<Response> Update(UpdateProductOption productOption)
        {
            try
            {
                var existingOption = await _context.ProductOptions
                    .FirstOrDefaultAsync(o => o.Id == productOption.Id);
                if (existingOption == null)
                    return new Response
                    {
                        StatusCode = 404,
                        Message = "Product option not found."
                    };
                existingOption.Quantity = productOption.Quantity;
                existingOption.Price = productOption.Price;
                existingOption.Color = productOption.Color!;
                existingOption.Image = productOption.Image;
                existingOption.Type = productOption.Type!;
                existingOption.UpdatedAt = DateTime.Now;
                _context.ProductOptions.Update(existingOption);
                await _context.SaveChangesAsync();
                return new Response
                {
                    StatusCode = 200,
                    Message = "Product option updated successfully.",
                    Data = new { value = MapperSingleton<MapperProductOption>.Instance.MapResponse(existingOption) }
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
