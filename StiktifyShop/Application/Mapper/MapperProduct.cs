using StiktifyShop.Application.DTOs.Requests;
using StiktifyShop.Application.DTOs.Responses;
using StiktifyShop.Domain.Entity;

namespace StiktifyShop.Application.Mapper
{
    public class MapperProduct
    {
        public Product MapCreate(CreateProduct createProduct)
        {
            return new Product
            {
                Name = createProduct.Name,
                Description = createProduct.Description,
                CategoryId = createProduct.CategoryId,
                ImageUri = createProduct.ImageUri,
                ShopId = createProduct.ShopId,
                IsHidden = createProduct.IsHidden,
                ProductOptions = createProduct.Options?.Select(po => new ProductOption
                {
                    Image = po.Image,
                    Price = po.Price,
                    Color = po.Color,
                    Quantity = po.Quantity,
                    Type = po.Type,
                    ProductVariants = po.ProductVariants?.Select(v => new ProductVariant
                    {
                        SizeId = v.SizeId,
                        Price = v.Price,
                        Quantity = v.Quantity
                    }).ToList() ?? []
                }).ToList() ?? []
            };
        }

        public ResponseProduct MapResponse(Product product)
        {
            return new ResponseProduct
            {
                Id = product.Id,
                CategoryId = product.CategoryId,
                ImageUri = product.ImageUri,
                Description = product.Description,
                IsHidden = product.IsHidden,
                Name = product.Name,
                ShopId = product.ShopId,
                Shop = new ResponseShop
                {
                    Id = product.Shop.Id,
                    ShopName = product.Shop.ShopName,
                    Description = product.Shop.Description,
                    AvatarUri = product.Shop.AvatarUri,
                    ShopType = product.Shop.ShopType,
                    UpdateAt = product.Shop.UpdatedAt,
                },
                Category = new ResponseCategory
                {
                    Id = product.CategoryId,
                    Name = product.Category.Name,
                },
                CreateAt = product.CreatedAt,
                UpdateAt = product.UpdatedAt,
            };
        }
    }
}
