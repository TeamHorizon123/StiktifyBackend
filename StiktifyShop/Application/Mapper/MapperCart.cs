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
                ImageUri = createCart.ImageUri,
                OptionId = createCart.OptionId,
                ProductId = createCart.ProductId,
                VariantId = createCart.VariantId,
                Quantity = createCart.Quantity,
                UserId = createCart.UserId,
            };
        }

        public ResponseCart MapResponse(Cart cart)
        {
            return new ResponseCart
            {
                Id = cart.Id,
                ProductId = cart.ProductId,
                ImageUri = cart.ImageUri,
                OptionId = cart.OptionId,
                VariantId = cart.VariantId,
                UserId = cart.UserId,
                Quantity = cart.Quantity,
                Product = new ResponseProduct
                {
                    Id = cart.Product?.Id,
                    ShopId = cart.Product?.ShopId,
                    Name = cart.Product?.Name
                },
                Option = new ResponseProductOption
                {
                    Id = cart.Option?.Id,
                    Color = cart.Option?.Color,
                    Type = cart.Option?.Type,
                    Quantity = cart.Option?.Quantity,
                    Image = cart.Option?.Image,
                    Price = cart.Option?.Price,
                },
                Variant = new ResponseProductVariant
                {
                    Id = cart.Variant?.Id,
                    SizeId = cart.Variant?.SizeId,
                    Price = cart.Variant!.Price,
                    Quantity = cart.Variant.Quantity,
                },
                CreateAt = cart.CreatedAt,
                UpdateAt = cart.UpdatedAt,
            };
        }
    }
}
