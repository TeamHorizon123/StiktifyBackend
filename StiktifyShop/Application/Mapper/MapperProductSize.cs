using StiktifyShop.Application.DTOs.Requests;
using StiktifyShop.Application.DTOs.Responses;
using StiktifyShop.Domain.Entity;

namespace StiktifyShop.Application.Mapper
{
    public class MapperProductSize
    {
        public ProductSize MapCreate(CreateProductSize productSize)
        {
            return new ProductSize
            {
                Size = productSize.Size,
                CategoryId = productSize.CategoryId,                
            };
        }

        public ResponseProductSize MapResponse(ProductSize productSize)
        {
            return new ResponseProductSize
            {
                Id = productSize.Id,
                Size = productSize.Size,
                CategoryId = productSize.CategoryId,
                CreateAt = productSize.CreatedAt,
                UpdateAt = productSize.UpdatedAt,
            };
        }
    }
}
