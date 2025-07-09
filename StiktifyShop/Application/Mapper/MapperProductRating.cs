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
                ProductItemId = createRating.ProductItemId,
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
                ProductItemId = productRating.ProductItemId,
                Content = productRating.Content,
                Product = new ResponseProduct
                {
                    Name = productRating.Product.Name,
                },
                ProductItem = new ResponseProductItem
                {
                    Id = productRating.ProductItem.Id,
                    Size = productRating.ProductItem.Size,
                    Color = productRating.ProductItem.Color,
                    Type = productRating.ProductItem.Type,
                    Image = productRating.ProductItem.Image,
                },
                CreateAt = productRating.CreatedAt,
                UpdateAt = productRating.UpdatedAt
            };
        }
    }
}
