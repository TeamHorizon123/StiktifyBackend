using StiktifyShop.Application.DTOs.Requests;
using StiktifyShop.Application.DTOs.Responses;
using StiktifyShop.Domain.Entity;

namespace StiktifyShop.Application.Mapper
{
    public class MapperShop
    {
        public Shop MapCreate(CreateShop createShop)
        {
            return new Shop
            {
                Id = createShop.UserId,
                Address = createShop.Address,
                Description = createShop.Description,
                AvatarUri = createShop.AvatarUri,
                Email = createShop.Email,
                Phone = createShop.Phone,
                ShopName = createShop.ShopName,
                Status = createShop.Status,
                UserId = createShop.UserId,
                ShopType = createShop.ShopType,
            };
        }

        public ResponseShop MapResponse(Shop shop)
        {
            return new ResponseShop
            {
                Id = shop.Id,
                Address = shop.Address,
                Description = shop.Description,
                AvatarUri = shop.AvatarUri,
                Email = shop.Email,
                Phone = shop.Phone,
                ShopName = shop.ShopName,
                Status = shop.Status,
                UserId = shop.UserId,
                ShopType = shop.ShopType,
                CreateAt = shop.CreatedAt,
                UpdateAt = shop.UpdatedAt
            };
        }
    }
}
