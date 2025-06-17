using Domain.Entities;
using Domain.Requests;
using Domain.Responses;
using Grpc.Core;
using GrpcServiceProduct.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GrpcServiceProduct.Data
{
    public class CategoryRepository : ICategoryRepository
    {
        private AppDbContext _context;
        private ILogger _logger;

        public CategoryRepository(AppDbContext context, ILogger logger)
        {
            _context = context ?? throw new ArgumentException(nameof(_context));
            _logger = logger ?? throw new ArgumentException(nameof(logger));
        }

        public async Task<Response> CreateCategory(RequestCreateCategory createCategory)
        {
            try
            {
                var category = new Domain.Entities.Category
                {
                    Name = createCategory.Name,
                    ParentId = createCategory.ParentId,
                    CreateAt = DateTime.Now,
                };
                _context.Categories.Add(category);
                await _context.SaveChangesAsync();
                return new Response { Message = $"_id: {category.Id}", StatusCode = 201 };
            }
            catch (Exception err)
            {
                _logger.LogError($"Fail to add a new category \nError: {err.Message}");
                return new Response { Message = "Fail to add a new category", StatusCode = 500 };
            }
        }

        public async Task<Response> DeleteCategory(string categoryId)
        {
            try
            {
                var category = await _context.Categories.FindAsync(categoryId);
                if (category == null)
                    return new Response { Message = "Category does not exist.", StatusCode = 404 };
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
                return new Response { StatusCode = 204 };
            }
            catch (Exception err)
            {
                _logger.LogError($"Fail to delete a category \nError: {err.Message}");
                return new Response { Message = "Fail to delete a category", StatusCode = 500 };
            }
        }

        public async Task<Response> DeleteMany(string[] listCategoryId)
        {
            try
            {
                var categories = await _context.Categories
                    .Where(c => listCategoryId.Contains(c.Id))
                    .ToListAsync();

                if (categories == null)
                    return new Response { Message = "Nothing in list to delete.", StatusCode = 404 };
                _context.Categories.RemoveRange(categories);
                await _context.SaveChangesAsync();
                return new Response { StatusCode = 204 };
            }
            catch (Exception err)
            {
                _logger.LogError($"Fail to delete a list categories \nError: {err.Message}");
                return new Response { Message = "Fail to delete a list categories", StatusCode = 500 };
            }
        }

        public async Task<IEnumerable<ResponseCategory>> GetAll()
        {
            try
            {
                return await _context.Categories
                    .Select(
                    c => new ResponseCategory
                    {
                        Id = c.Id,
                        Name = c.Name,
                        ParentId = c.ParentId,
                        CreateAt = c.CreateAt,
                        UpdateAt = c.UpdateAt,
                    })
                    .ToListAsync();
            }
            catch (Exception err)
            {
                _logger.LogError($"Fail to get all category \nError: {err.Message}");
                throw new RpcException(new Status(StatusCode.Internal, "Internal Error"));
            }
        }

        public async Task<IEnumerable<ResponseCategory>> GetAllChildren(string parentId)
        {
            try
            {
                return await _context.Categories
                    .Where(c => c.ParentId == parentId)
                    .Select(
                    c => new ResponseCategory
                    {
                        Id = c.Id,
                        Name = c.Name,
                        ParentId = c.ParentId,
                        CreateAt = c.CreateAt,
                        UpdateAt = c.UpdateAt,
                    })
                    .ToListAsync();
            }
            catch (Exception err)
            {
                _logger.LogError($"Fail to get all category children \nError: {err.Message}");
                throw new RpcException(new Status(StatusCode.Internal, "Internal Error"));
            }
        }

        public async Task<IEnumerable<ResponseCategory>> GetAllOfProduct(string productId)
        {
            try
            {
                return await _context.Categories
                    .Where(c => c.Products.Any(p => p.Id == productId))
                    .Select(
                    c => new ResponseCategory
                    {
                        Id = c.Id,
                        Name = c.Name,
                        ParentId = c.ParentId,
                        CreateAt = c.CreateAt,
                        UpdateAt = c.UpdateAt,
                    })
                    .ToListAsync();
            }
            catch (Exception err)
            {
                _logger.LogError($"Fail to get all category of product \nError: {err.Message}");
                throw new RpcException(new Status(StatusCode.Internal, "Internal Error"));
            }
        }

        public async Task<ResponseCategory?> GetOne(string categoryId)
        {
            try
            {
                return await _context.Categories
                    .Where(c => c.Id == categoryId)
                    .Select(
                    c => new ResponseCategory
                    {
                        Id = c.Id,
                        Name = c.Name,
                        ParentId = c.ParentId,
                        CreateAt = c.CreateAt,
                        UpdateAt = c.UpdateAt,
                    })
                    .FirstOrDefaultAsync();
            }
            catch (Exception err)
            {
                _logger.LogError($"Fail to get a category has id-{categoryId} \nError: {err.Message}");
                throw new RpcException(new Status(StatusCode.Internal, "Internal Error"));
            }
        }

        public async Task<Response> UpdateCategory(RequestUpdateCategory updateCategory)
        {
            if (await GetOne(updateCategory.Id) == null)
                return new Response { Message = "Category does not exist.", StatusCode = 404 };
            try
            {
                var category = new Domain.Entities.Category
                {
                    Id = updateCategory.Id,
                    Name = updateCategory.Name,
                    ParentId = updateCategory.Id,
                    UpdateAt = DateTime.Now
                };
                _context.Categories.Update(category);
                await _context.SaveChangesAsync();
                return new Response { Message = $"_id: {category.Id}", StatusCode = 200 };
            }
            catch (Exception err)
            {
                _logger.LogError($"Fail to update a category \nError: {err.Message}");
                return new Response { Message = "Fail to update a category", StatusCode = 500 };
            }
        }
    }
}
