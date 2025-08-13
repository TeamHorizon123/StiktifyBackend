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
                ShopId = createOrder.ShopId,
                AddressId = createOrder.AddressId,
                TotalAmount = createOrder.TotalAmount,
                OrderItems = createOrder.OrderItems.Select(item => new OrderItem
                {
                    ImageUri = item.ImageUri,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    ProductVariantId = item.ProductVariantId,
                }).ToList(),
                ShippingFee = createOrder.ShippingFee,
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
                    AvatarUri = order.Shop.AvatarUri,
                    ShopName = order.Shop.ShopName,
                    ShopType = order.Shop.ShopType,
                    Description = order.Shop.Description,
                    CreateAt = order.Shop.CreatedAt,
                    UpdateAt = order.Shop.UpdatedAt
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
                Total = order.TotalAmount,
                ShippingFee = order.ShippingFee,
                OrderItems = order.OrderItems.Select(item => new ResponseOrderItem
                {
                    Id = item.Id,
                    ImageURI = item.ImageUri,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    ProductVariantId = item.ProductVariantId,
                    Product = item.Product != null ? new ResponseProduct
                    {
                        Id = item.Product.Id,
                        Name = item.Product.Name,
                        ImageUri = item.Product.ImageUri,
                    } : null,
                    ProductVariant = item.ProductVariant != null ? new ResponseProductVariant
                    {
                        Id = item.ProductVariant.Id,
                        Price = item.ProductVariant.Price,
                        ProductOptionId = item.ProductVariant.ProductOptionId,
                        Quantity = item.ProductVariant.Quantity,
                        SizeId = item.ProductVariant.SizeId,
                    } : null
                }).ToList() ?? [],
                Note = order.Note,
                CreateAt = order.CreatedAt,
                UpdateAt = order.UpdatedAt,
            };
        }
    }
}
