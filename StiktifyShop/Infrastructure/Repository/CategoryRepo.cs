using Microsoft.EntityFrameworkCore;
using StiktifyShop.Application.DTOs.Requests;
using StiktifyShop.Application.DTOs.Responses;
using StiktifyShop.Application.Interfaces;
using StiktifyShop.Application.Mapper;

namespace StiktifyShop.Infrastructure.Repository
{
    public class CategoryRepo : ICategoryRepo
    {
        private AppDbContext _context;
        public CategoryRepo(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        private ICollection<ResponseCategory> GetChildren(string id)
        {
            var children = _context.Categories
                .Where(c => c.ParentId == id)
                .Select(c => MapperSingleton<MapperCategory>.Instance.MapResponse(c))
                .ToList();
            return children;
        }

        public async Task<Response> Create(CreateCategory category)
        {
            try
            {
                var existingCategory = await _context.Categories
                    .FirstOrDefaultAsync(c => c.Name == category.Name);
                if (existingCategory != null)
                    return new Response
                    {
                        StatusCode = 400,
                        Message = "Category already exists."
                    };
                var newCategory = MapperSingleton<MapperCategory>.Instance.MapCreate(category);
                _context.Categories.Add(newCategory);
                await _context.SaveChangesAsync();
                return new Response
                {
                    StatusCode = 201,
                    Message = "Category created successfully.",
                    Data = new { Id = newCategory.Id }
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

        public async Task<Response> Delete(string categoryId)
        {
            try
            {
                var existingCategory = await _context.Categories
                    .FirstOrDefaultAsync(c => c.Id == categoryId);
                if (existingCategory == null)
                    return new Response
                    {
                        StatusCode = 404,
                        Message = "Category not found."
                    };
                await _context.SaveChangesAsync();
                return new Response
                {
                    StatusCode = 204
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

        public IQueryable<ResponseCategory> GetAll()
        {
            try
            {
                var list = _context.Categories.
                    Select(category
                    => MapperSingleton<MapperCategory>.Instance.MapResponse(category)
                    )
                    .ToList();
                return list.Select(category
                    =>
                {
                    category.Children = GetChildren(category.Id!);
                    return category;
                }
                ).AsQueryable();
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

        public async Task<ResponseCategory?> GetById(string categoryId)
        {
            try
            {
                var category = await _context.Categories
                    .FirstOrDefaultAsync(c => c.Id == categoryId);
                if (category == null)
                    return null;
                var response = MapperSingleton<MapperCategory>.Instance.MapResponse(category);
                response.Children = GetChildren(categoryId);
                return response;
            }
            catch (Exception err)
            {
                return null;
                throw new Exception(err.Message);
            }
        }

        public async Task<Response> Update(UpdateCategory category)
        {
            try
            {
                var existingCategory = await _context.Categories
                    .FirstOrDefaultAsync(c => c.Id == category.Id);
                if (existingCategory == null)
                    return new Response
                    {
                        StatusCode = 404,
                        Message = "Category not found."
                    };

                existingCategory.Name = category.Name;
                existingCategory.UpdatedAt = DateTime.Now;

                _context.Categories.Update(existingCategory);
                await _context.SaveChangesAsync();
                return new Response
                {
                    StatusCode = 200,
                    Message = "Category updated successfully.",
                    Data = new { value = MapperSingleton<MapperCategory>.Instance.MapResponse(existingCategory) }
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
