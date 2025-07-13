using StiktifyShop.Application.DTOs.Requests;
using StiktifyShop.Application.DTOs.Responses;
using StiktifyShop.Domain.Entity;

namespace StiktifyShop.Application.Mapper
{
    public class MapperProductOption
    {
        public ProductOption MapCreate(CreateProductOption createOption)
        {
            return new ProductOption
            {
                Image = createOption.Image,
                Price = createOption.Price,
                Color = createOption.Color,
                Quantity = createOption.Quantity,
                Type = createOption.Type,
                ProductVariants = createOption.ProductVariants?.Select(v => new ProductVariant
                {
                    SizeId = v.SizeId,
                    Price = v.Price,
                    Quantity = v.Quantity
                }).ToList() ?? []
            };
        }

        public ResponseProductOption MapResponse(ProductOption productOption)
        {
            return new ResponseProductOption
            {
                Id = productOption.Id,
                ProductId = productOption.ProductId,
                Image = productOption.Image,
                Price = productOption.Price,
                Color = productOption.Color,
                Quantity = productOption.Quantity,
                Type = productOption.Type,
                ProductVariants = productOption.ProductVariants?.Select(v => new ResponseProductVariant
                {
                    Id = v.Id,
                    SizeId = v.SizeId,
                    Price = v.Price,
                    Quantity = v.Quantity
                }).ToList() ?? [],
                CreateAt = productOption.CreatedAt,
                UpdateAt = productOption.UpdatedAt
            };
        }
    }
}
