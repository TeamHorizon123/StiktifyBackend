using StiktifyShop.Application.DTOs.Requests;
using StiktifyShop.Application.DTOs.Responses;
using StiktifyShop.Domain.Entity;

namespace StiktifyShop.Application.Mapper
{
    public class MapperOrder
    {
        public Order MapCreate(CreateOrder createOrder)
        {
            return new Order
            {
                UserId = createOrder.UserId,
                AddressId = createOrder.AddressId,
                ProductId = createOrder.ProductId,
                ShopId = createOrder.ShopId,
                Price = createOrder.Price,
                Quantity = createOrder.Quantity,
                ShippingFee = createOrder.ShippingFee,
                ProductItemId = createOrder.ProductItemId,
                Status = createOrder.Status,
            };
        }

        public ResponseOrder MapResponse(Order order)
        {
            return new ResponseOrder
            {
                Id = order.Id,
                UserId = order.UserId,
                ShopId = order.ShopId,
                Shop = new ResponseShop
                {
                    Id = order.Shop.Id,
                    AvartarUri = order.Shop.AvartarUri,
                    ShopName = order.Shop.ShopName,
                    ShopType = order.Shop.ShopType,
                    Description = order.Shop.Description,
                    CreateAt = order.Shop.CreatedAt,
                    UpdateAt = order.Shop.UpdatedAt
                },
                ProductId = order.ProductId,
                Product = new ResponseProduct
                {
                    Id = order.Product.Id,
                    Name = order.Product.Name,
                    Description = order.Product.Description,
                    ImageUri = order.Product.ImageUri,
                    CategoryId = order.Product.CategoryId,
                    CreateAt = order.Product.CreatedAt,
                    UpdateAt = order.Product.UpdatedAt
                },
                ProductItemId = order.ProductItemId,
                ProductItem = new ResponseProductItem
                {
                    Id = order.ProductItem.Id,
                    Image = order.ProductItem.Image,
                    ProductId = order.ProductItem.ProductId,
                    Price = order.ProductItem.Price,
                    Size = order.ProductItem.Size,
                    Type = order.ProductItem.Type,
                    Color = order.ProductItem.Color,
                    CreateAt = order.ProductItem.CreatedAt,
                    UpdateAt = order.ProductItem.UpdatedAt
                },
                AddressId = order.AddressId,
                Address = new ResponseUserAddress
                {
                    Id = order.Address.Id,
                    UserId = order.Address.UserId,
                    Address = order.Address.Address,
                    PhoneReceive = order.Address.PhoneReceive,
                    Receiver = order.Address.Receiver,
                    CreateAt = order.Address.CreatedAt,
                    UpdateAt = order.Address.UpdatedAt
                },
                Status = order.Status,
                Quantity = order.Quantity,
                Price = order.Price,
                ShippingFee = order.ShippingFee,
            };
        }
    }
}
