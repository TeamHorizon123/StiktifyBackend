using Domain.Requests;
using Domain.Responses;
using Grpc.Core;
using GrpcServiceProduct.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GrpcServiceProduct.Data
{
    public class ProductItemRepository : IProductItemRepository
    {
        private AppDbContext _context;
        public ProductItemRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentException(nameof(context));
        }

        public async Task<Response> AddProductItem(RequestCreateProductItem productItem)
        {
            try
            {
                var produdctItem = new Domain.Entities.ProductItem
                {
                    ProductId = productItem.ProductId,
                    Size = productItem.Size,
                    Color = productItem.Color,
                    Type = productItem.Type,
                    Price = productItem.Price,
                    Quantity = productItem.Quantity,
                    Image = productItem.Image,
                    CreateAt = DateTime.Now,
                };
                _context.ProductItems.Add(produdctItem);
                await _context.SaveChangesAsync();
                return new Response
                {
                    Message = produdctItem.Id,
                    StatusCode = 201
                };
            }
            catch (Exception err)
            {
                return new Response
                {
                    Message = $"Fail to add a new product item\nError: {err.Message}",
                    StatusCode = 500
                };
            }
        }

        public async Task<Response> DeleteProductItem(string productId)
        {
            try
            {
                var result = await _context.ProductItems.FindAsync(productId);
                if (result == null)
                    return new Response
                    {
                        Message = "Product item not found",
                        StatusCode = 404
                    };
                _context.ProductItems.Remove(result);
                await _context.SaveChangesAsync();
                return new Response
                {
                    StatusCode = 204
                };
            }
            catch (Exception err)
            {
                return new Response
                {
                    Message = $"Fail to delete product item\nError: {err.Message}",
                    StatusCode = 500
                };
            }
        }

        public async Task<IEnumerable<ResponseProductItem>> GetAllOfProduct(string productId)
        {
            try
            {
                return await _context.ProductItems
                    .Where(pi => pi.ProductId == productId)
                    .Select(pi => new ResponseProductItem
                    {
                        Id = pi.Id,
                        ProductId = pi.ProductId,
                        Size = pi.Size,
                        Color = pi.Color,
                        Type = pi.Type,
                        Quantity = pi.Quantity,
                        Price = pi.Price,
                        Image = pi.Image,
                        CreateAt = pi.CreateAt,
                        UpdateAt = pi.UpdateAt
                    })
                    .ToListAsync();
            }
            catch (Exception err)
            {
                Console.WriteLine($"Fail to get all of product\nError: {err.Message}");
                throw new RpcException(new Status(StatusCode.Internal, "Internal Error"));
            }
        }

        public async Task<IEnumerable<ResponseProductItem>> GetAllProductItems()
        {
            try
            {
                return await _context.ProductItems
                    .Select(pi => new ResponseProductItem
                    {
                        Id = pi.Id,
                        ProductId = pi.ProductId,
                        Size = pi.Size,
                        Color = pi.Color,
                        Type = pi.Type,
                        Quantity = pi.Quantity,
                        Price = pi.Price,
                        Image = pi.Image,
                        CreateAt = pi.CreateAt,
                        UpdateAt = pi.UpdateAt
                    })
                    .ToListAsync();
            }
            catch (Exception err)
            {
                Console.WriteLine($"Fail to get all product item\n Error: {err.Message}");
                throw new RpcException(new Status(StatusCode.Internal, "Internal Error"));
            }
        }

        public async Task<ResponseProductItem?> GetProductItem(string productId)
        {
            try
            {
                return await _context.ProductItems
                    .Select(pi => new ResponseProductItem
                    {
                        Id = pi.Id,
                        ProductId = pi.ProductId,
                        Size = pi.Size,
                        Color = pi.Color,
                        Type = pi.Type,
                        Quantity = pi.Quantity,
                        Price = pi.Price,
                        Image = pi.Image,
                        CreateAt = pi.CreateAt,
                        UpdateAt = pi.UpdateAt
                    })
                    .FirstOrDefaultAsync();
            }
            catch (Exception err)
            {
                Console.WriteLine($"Fail to get all product item\n Error: {err.Message}");
                throw new RpcException(new Status(StatusCode.Internal, "Internal Error"));
            }
        }

        public async Task<Response> UpdateProductItem(RequestUpdateProductItem productItem)
        {
            try
            {
                var updateItem = await _context.ProductItems.FindAsync(productItem.Id);
                if (updateItem == null)
                    return new Response
                    {
                        Message = "Product item not found",
                        StatusCode = 404
                    };
                updateItem.ProductId = productItem.ProductId;
                updateItem.Size = productItem.Size;
                updateItem.Color = productItem.Color;
                updateItem.Type = productItem.Type;
                updateItem.Price = productItem.Price;
                updateItem.Quantity = productItem.Quantity;
                updateItem.Image = productItem.Image;
                updateItem.UpdateAt = DateTime.Now;
                _context.ProductItems.Update(updateItem);
                await _context.SaveChangesAsync();
                return new Response
                {
                    Message = updateItem.Id,
                    StatusCode = 200
                };
            }
            catch (Exception err)
            {
                return new Response
                {
                    Message = $"Fail to update product item\nError: {err.Message}",
                    StatusCode = 500
                };
            }
        }
    }
}
