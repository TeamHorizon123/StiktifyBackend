using StiktifyShop.Application.DTOs.Requests;
using StiktifyShop.Application.DTOs.Responses;
using StiktifyShop.Domain.Entity;

namespace StiktifyShop.Application.Mapper
{
    public class MapperCategory
    {
        public Category MapCreate(CreateCategory createCategory)
        {
            return new Category
            {
                Name = createCategory.Name,
                ParentId = createCategory.ParentId,
            };
        }
        public ResponseCategory MapResponse(Category category)
        {
            return new ResponseCategory
            {
                Id = category.Id,
                Name = category.Name,
                ParentId = category.ParentId,
                Children = category.Children.Select(c => new ResponseCategory
                {
                    Id = c.Id,
                    Name = c.Name,
                    ParentId = c.ParentId,
                }).ToList()
            };
        }
    }
}
