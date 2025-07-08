using Domain.Requests;
using Domain.Responses;
using Grpc.Core;
using GrpcServiceProduct.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GrpcServiceProduct.Data
{
    public class ProductOptionRepository : IProductOptionRepository
    {
        private AppDbContext _context;

        public ProductOptionRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentException(nameof(_context));
        }

        public async Task<Response> CreateManyProductOption(ICollection<RequestCreateOption> createOption)
        {
            try
            {
                var listCreateOptions = new List<Domain.Entities.ProductOption>();
                foreach (var item in createOption)
                {
                    listCreateOptions.Add(new Domain.Entities.ProductOption
                    {
                        ProductId = item.ProductId,
                        Image = item.Image,
                        Color = item.Color,
                        Type = item.Type,
                        CreateAt = DateTime.Now,
                    });
                }
                _context.ProductOptions.AddRange(listCreateOptions);
                await _context.SaveChangesAsync();
                return new Response { Message = "Add list product options success.", StatusCode = 201 };
            }
            catch (Exception err)
            {
                Console.WriteLine($"Fail to add a list new product option \nError: {err.Message}");
                return new Response { Message = "Fail to add a list new product option", StatusCode = 500 };
            }
        }

        public async Task<Response> CreateProductOption(RequestCreateOption createOption)
        {
            try
            {
                var option = new Domain.Entities.ProductOption
                {
                    ProductId = createOption.ProductId,
                    Image = createOption.Image,
                    Color = createOption.Color,
                    Type = createOption.Type,
                    CreateAt = DateTime.Now,
                };
                _context.ProductOptions.Add(option);
                await _context.SaveChangesAsync();
                return new Response { Message = $"_id: {option.Id}", StatusCode = 201 };
            }
            catch (Exception err)
            {
                Console.WriteLine($"Fail to add a new product option \nError: {err.Message}");
                return new Response { Message = "Fail to add a new product option", StatusCode = 500 };
            }
        }

        public async Task<Response> DeleteManyProductOption(ICollection<string> listOptionId)
        {
            try
            {
                var listOptionRemove = new List<Domain.Entities.ProductOption>();
                foreach (var optionId in listOptionId)
                {
                    var option = await _context.ProductOptions.FindAsync(optionId);
                    if (option != null)
                        listOptionRemove.Add(option);
                }

                if (listOptionRemove == null)
                    return new Response { Message = "Nothing in list to remove.", StatusCode = 404 };

                _context.ProductOptions.RemoveRange(listOptionRemove);
                await _context.SaveChangesAsync();
                return new Response { StatusCode = 204 };
            }
            catch (Exception err)
            {
                Console.WriteLine($"Fail to remove a list product options  \nError: {err.Message}");
                return new Response { Message = "Fail to remove a list product options", StatusCode = 500 };
            }
        }

        public async Task<Response> DeleteProductOption(string optionId)
        {
            try
            {
                var option = await _context.ProductOptions.FindAsync(optionId);
                if (option == null)
                    return new Response { Message = "Product option does not exist.", StatusCode = 404 };

                _context.ProductOptions.Remove(option);
                await _context.SaveChangesAsync();
                return new Response { StatusCode = 204 };
            }
            catch (Exception err)
            {
                Console.WriteLine($"Fail to remove a product option has id-{optionId} \nError: {err.Message}");
                return new Response { Message = $"Fail to remove a product option has id-{optionId}", StatusCode = 500 };
            }
        }

        public async Task<IEnumerable<ResponseProductOption>> GetAll()
        {
            try
            {
                return await _context.ProductOptions
                    .Select(
                    option => new ResponseProductOption
                    {
                        Id = option.Id,
                        ProductId = option.ProductId,
                        Image = option.Image,
                        Color = option.Color,
                        Type = option.Type,
                        CreateAt = option.CreateAt,
                        UpdateAt = option.UpdateAt
                    })
                    .ToListAsync();
            }
            catch (Exception err)
            {
                Console.WriteLine($"Fail to get all product option \nError: {err.Message}");
                throw new RpcException(new Status(StatusCode.Internal, "Internal Error"));
            }
        }

        public async Task<IEnumerable<ResponseProductOption>> GetAllOfProduct(string productId)
        {
            try
            {
                return await _context.ProductOptions
                    .Where(option => option.ProductId == productId)
                    .Select(
                    option => new ResponseProductOption
                    {
                        Id = option.Id,
                        ProductId = option.ProductId,
                        Image = option.Image,
                        Color = option.Color,
                        Type = option.Type,
                        CreateAt = option.CreateAt,
                        UpdateAt = option.UpdateAt
                    })
                    .ToListAsync();
            }
            catch (Exception err)
            {
                Console.WriteLine($"Fail to get all product option of product has id-{productId} \nError: {err.Message}");
                throw new RpcException(new Status(StatusCode.Internal, "Internal Error"));
            }
        }

        public async Task<ResponseProductOption?> GetOne(string optionId)
        {
            try
            {
                return await _context.ProductOptions
                    .Where(option => option.Id == optionId)
                    .Select(
                    option => new ResponseProductOption
                    {
                        Id = option.Id,
                        ProductId = option.ProductId,
                        Image = option.Image,
                        Color = option.Color,
                        Type = option.Type,
                        CreateAt = option.CreateAt,
                        UpdateAt = option.UpdateAt
                    })
                    .FirstOrDefaultAsync();
            }
            catch (Exception err)
            {
                Console.WriteLine($"Fail to get a product option has id-{optionId} \nError: {err.Message}");
                throw new RpcException(new Status(StatusCode.Internal, "Internal Error"));
            }
        }

        public async Task<Response> UpdateProductOption(RequestUpdateOption updateOption)
        {
            try
            {
                var option = await _context.ProductOptions.FindAsync(updateOption.Id);
                if (option == null)
                    return new Response { Message = "Product option does not exist.", StatusCode = 404 };

                option.Image = updateOption.Image;
                option.Color = updateOption.Color;
                option.Type = updateOption.Type;
                option.UpdateAt = DateTime.Now;

                _context.ProductOptions.Update(option);
                await _context.SaveChangesAsync();
                return new Response { Message = "Update product options success.", StatusCode = 200 };
            }
            catch (Exception err)
            {
                Console.WriteLine($"Fail to update a product option has id-{updateOption.Id} \nError: {err.Message}");
                return new Response { Message = $"Fail to update a product option has id-{updateOption.Id}", StatusCode = 500 };
            }
        }
    }
}
