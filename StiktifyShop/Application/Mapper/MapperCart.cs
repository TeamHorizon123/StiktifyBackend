using StiktifyShop.Application.DTOs.Requests;
using StiktifyShop.Application.DTOs.Responses;
using StiktifyShop.Domain.Entity;

namespace StiktifyShop.Application.Mapper
{
    public class MapperCart
    {
        public Cart MapCreate(CreateCart createCart)
        {
            return new Cart
            {
                ProductItemId = createCart.ProductItemId,
                Quantity = createCart.Quantity,
                UserId = createCart.UserId,
            };
        }

        public ResponseCart MapResponse(Cart cart)
        {
            return new ResponseCart
            {
                Id = cart.Id,
                ProductItemId = cart.ProductItemId,
                UserId = cart.UserId,
                Quantity = cart.Quantity,
                ProductItem = new ResponseProductItem
                {
                    Id = cart.ProductItem.Id,
                    Image = cart.ProductItem.Image,
                    ProductId = cart.ProductItem.ProductId,
                    Price = cart.ProductItem.Price,
                    Quantity = cart.ProductItem.Quantity,
                    Size = cart.ProductItem.Size,
                    Type = cart.ProductItem.Type,
                    Color = cart.ProductItem.Color,
                    CreateAt = cart.ProductItem.CreatedAt,
                    UpdateAt = cart.ProductItem.UpdatedAt,
                },
                CreateAt = cart.CreatedAt,
                UpdateAt = cart.UpdatedAt,
            };
        }
    }
}
