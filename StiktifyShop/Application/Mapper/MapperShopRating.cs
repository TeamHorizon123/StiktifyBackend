using StiktifyShop.Application.DTOs.Requests;
using StiktifyShop.Application.DTOs.Responses;
using StiktifyShop.Domain.Entity;

namespace StiktifyShop.Application.Mapper
{
    public class MapperShopRating
    {
        public ShopRating MapCreate(CreateShopRating createRating)
        {
            return new ShopRating
            {
                ShopId = createRating.ShopId,
                UserId = createRating.UserId,
                Content = createRating.Content,
                Point = createRating.Point,
            };
        }

        public ResponseShopRating MapResponse(ShopRating shopRating)
        {
            return new ResponseShopRating
            {
                Id = shopRating.Id,
                ShopId = shopRating.ShopId,
                UserId = shopRating.UserId,
                Content = shopRating.Content,
                Point = shopRating.Point,
                CreateAt = shopRating.CreatedAt,
                UpdateAt = shopRating.UpdatedAt
            };
        }
    }
}
