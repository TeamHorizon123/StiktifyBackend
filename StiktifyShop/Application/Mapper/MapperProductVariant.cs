using StiktifyShop.Application.DTOs.Requests;
using StiktifyShop.Application.DTOs.Responses;
using StiktifyShop.Domain.Entity;

namespace StiktifyShop.Application.Mapper
{
    public class MapperProductVariant
    {
        public ProductVariant MapCreate(CreateProductVariant createVariant)
        {
            return new ProductVariant
            {
                SizeId = createVariant.SizeId,
                Price = createVariant.Price,
                Quantity = createVariant.Quantity
            };
        }

        public ResponseProductVariant MapResponse(ProductVariant productVariant)
        {
            return new ResponseProductVariant
            {
                Id = productVariant.Id,
                SizeId = productVariant.SizeId,
                ProductOptionId = productVariant.ProductOptionId,
                Price = productVariant.Price,
                Quantity = productVariant.Quantity,
                ProductOption = new ResponseProductOption
                {
                    Id = productVariant.ProductOption.Id,
                    ProductId = productVariant.ProductOption.ProductId,
                    Image = productVariant.ProductOption.Image,
                    Color = productVariant.ProductOption.Color,
                    Type = productVariant.ProductOption.Type,
                    Price = productVariant.ProductOption.Price,
                    Quantity = productVariant.ProductOption.Quantity,
                },
                Size = new ResponseProductSize
                {
                    Size = productVariant.Size?.Size,
                },
                CreateAt = productVariant.CreatedAt,
                UpdateAt = productVariant.UpdatedAt
            };
        }
    }
}
