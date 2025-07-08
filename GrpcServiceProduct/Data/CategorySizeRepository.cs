using Domain.Requests;
using Domain.Responses;
using Grpc.Core;
using GrpcServiceProduct.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GrpcServiceProduct.Data
{
    public class CategorySizeRepository : ICategorySizeRepository
    {
        private readonly AppDbContext _context;
        public CategorySizeRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentException(nameof(_context));
        }
        public async Task<Response> CreateCategorySize(RequestCreateCategorySize createCategorySize)
        {
            try
            {
                var categorySize = new Domain.Entities.CategorySize
                {
                    CategoryId = createCategorySize.CategoryId,
                    Size = createCategorySize.Size,
                    CreateAt = DateTime.Now
                };
                _context.CategorieSizes.Add(categorySize);
                await _context.SaveChangesAsync();
                return new Response
                {
                    Message = categorySize.Id,
                    StatusCode = 201
                };
            }
            catch (Exception err)
            {
                Console.WriteLine($"Fail to add a new category size \nError: {err.Message}");
                return new Response
                {
                    Message = $"Fail to add a new category size",
                    StatusCode = 500
                };
            }
        }

        public async Task<Response> DeleteCategorySize(string categorySizeId)
        {
            try
            {
                var categorySize = await _context.CategorieSizes.FindAsync(categorySizeId);
                if (categorySize == null)
                    return new Response { Message = "Category size does not exist.", StatusCode = 404 };
                _context.CategorieSizes.Remove(categorySize);
                await _context.SaveChangesAsync();
                return new Response { StatusCode = 204 };
            }
            catch (Exception err)
            {
                Console.WriteLine($"Fail to delete a category size \nError: {err.Message}");
                return new Response
                {
                    Message = $"Fail to delete a category size",
                    StatusCode = 500
                };
            }
        }

        public async Task<IEnumerable<ResponseCategorySize>> GetAll()
        {
            try
            {
                return await _context.CategorieSizes
                    .Select(cs => new ResponseCategorySize
                    {
                        Id = cs.Id,
                        Size = cs.Size,
                        CategoryId = cs.CategoryId,
                        UpdateAt = cs.UpdateAt,
                        CreateAt = cs.CreateAt
                    })
                    .ToListAsync();
            }
            catch (Exception err)
            {
                Console.WriteLine($"Fail to get all category sizes \nError: {err.Message}");
                throw new RpcException(new Status(StatusCode.Internal, "Internal Error"));
            }
        }

        public async Task<IEnumerable<ResponseCategorySize>> GetAllOfCategory(string categoryId)
        {
            try
            {
                return await _context.CategorieSizes
                    .Where(cs => cs.CategoryId == categoryId)
                    .Select(cs => new ResponseCategorySize
                    {
                        Id = cs.Id,
                        Size = cs.Size,
                        CategoryId = cs.CategoryId,
                        UpdateAt = cs.UpdateAt,
                        CreateAt = cs.CreateAt
                    })
                    .ToListAsync();
            }
            catch (Exception err)
            {
                Console.WriteLine($"Fail to get all size of a category - {categoryId}\nError: {err.Message}");
                throw new RpcException(new Status(StatusCode.Internal, "Internal Error"));
            }
        }

        public async Task<IEnumerable<ResponseCategorySize>> GetAllOfProduct(string productId)
        {
            try
            {
                var categorySizes = await _context.ProductOptions
                    .Where(po => po.ProductId == productId)
                    .SelectMany(po => po.CategorySizes)
                    .Distinct()
                    .ToListAsync();
                return categorySizes.Select(cs => new ResponseCategorySize
                {
                    Id = cs.Id,
                    Size = cs.Size,
                    CategoryId = cs.CategoryId,
                    UpdateAt = cs.UpdateAt,
                    CreateAt = cs.CreateAt
                });
            }
            catch (Exception err)
            {
                Console.WriteLine($"Fail to get all size of a product - {productId}\nError: {err.Message}");
                throw new RpcException(new Status(StatusCode.Internal, "Internal Error"));
            }
        }

        public async Task<IEnumerable<ResponseCategorySize>> GetAllOfProductOption(string optionId)
        {
            try
            {
                var sizes = await _context.ProductOptions
                        .Where(po => po.Id == optionId)
                        .SelectMany(po => po.CategorySizes)
                        .Select(cs => new ResponseCategorySize
                        {
                            Id = cs.Id,
                            Size = cs.Size,
                            CategoryId = cs.CategoryId,
                            UpdateAt = cs.UpdateAt,
                            CreateAt = cs.CreateAt
                        })
                        .ToListAsync();
                return sizes;
            }
            catch (Exception err)
            {
                Console.WriteLine($"Fail to get all size of a product option - {optionId}\nError: {err.Message}");
                throw new RpcException(new Status(StatusCode.Internal, "Internal Error"));
            }
        }

        public async Task<ResponseCategorySize?> GetOne(string categorySizeId)
        {
            try
            {
                return await _context.CategorieSizes
                    .Where(cs => cs.Id == categorySizeId)
                    .Select(cs => new ResponseCategorySize
                    {
                        Id = cs.Id,
                        Size = cs.Size,
                        CategoryId = cs.CategoryId,
                        UpdateAt = cs.UpdateAt,
                        CreateAt = cs.CreateAt
                    })
                    .FirstOrDefaultAsync();
            }
            catch (Exception err)
            {
                Console.WriteLine($"Fail to get all size of a category - {categorySizeId}\nError: {err.Message}");
                return null;
                throw new RpcException(new Status(StatusCode.Internal, "Internal Error"));
            }
        }

        public async Task<Response> UpdateCategorySize(RequestUpdateCategorySize updateCategorySize)
        {
            try
            {
                var categorySize = await _context.CategorieSizes.FindAsync(updateCategorySize.Id);
                if (categorySize == null)
                    return new Response { Message = "Category size does not exist.", StatusCode = 404 };

                categorySize.Size = updateCategorySize.Size;
                categorySize.UpdateAt = DateTime.Now;
                _context.CategorieSizes.Update(categorySize);
                await _context.SaveChangesAsync();
                return new Response { StatusCode = 200, Message = categorySize.Id };
            }
            catch (Exception err)
            {
                Console.WriteLine($"Fail to update a category size \nError: {err.Message}");
                return new Response
                {
                    Message = $"Fail to update a category size",
                    StatusCode = 500
                };
            }
        }
    }
}
