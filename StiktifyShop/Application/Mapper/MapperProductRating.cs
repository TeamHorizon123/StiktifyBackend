using StiktifyShop.Application.DTOs.Requests;
using StiktifyShop.Application.DTOs.Responses;
using StiktifyShop.Domain.Entity;

namespace StiktifyShop.Application.Mapper
{
    public class MapperProductRating
    {
        public ProductRating MapCreate(CreateProducRating createRating)
        {
            return new ProductRating
            {
                ProductId = createRating.ProductId,
                UserId = createRating.UserId,
                Point = createRating.Point,
                Files = createRating.Files,
                OptionId = createRating.OptionId,
                VariantId = createRating.VariantId,
                OrderId = createRating.OrderId,
                Content = createRating.Content,
            };
        }

        public ResponseProductRating MapResponse(ProductRating productRating)
        {
            return new ResponseProductRating
            {
                Id = productRating.Id,
                ProductId = productRating.ProductId,
                UserId = productRating.UserId,
                Point = productRating.Point,
                Files = productRating.Files,
                OptionId = productRating.OptionId,
                VariantId = productRating.VariantId,
                Content = productRating.Content,
                Product = new ResponseProduct
                {
                    Name = productRating.Product.Name,
                },
                Option = new ResponseProductOption
                {
                    Id = productRating.Option.Id,
                    Color = productRating.Option.Color,
                    Type = productRating.Option.Type,
                    Price = productRating.Option.Price,
                    Image = productRating.Option.Image
                },
                Variant = new ResponseProductVariant
                {
                    Id = productRating.Variant.Id,
                    Price = productRating.Variant.Price,
                    SizeId = productRating.Variant.SizeId,
                },
                OrderId = productRating.OrderId,
                CreateAt = productRating.CreatedAt,
                UpdateAt = productRating.UpdatedAt
            };
        }
    }
}
