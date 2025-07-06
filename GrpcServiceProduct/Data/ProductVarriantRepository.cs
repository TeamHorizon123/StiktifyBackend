using Domain.Requests;
using Domain.Responses;
using Grpc.Core;
using GrpcServiceProduct.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GrpcServiceProduct.Data
{
    public class ProductVarriantRepository : IProductVarriantRepository
    {
        private AppDbContext _context;
        public ProductVarriantRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentException(nameof(context));
        }
        public async Task<Response> CreateProductVarriant(RequestCreateProductVarriant createProductVarriant)
        {
            try
            {
                var productVarriant = new Domain.Entities.ProductVarriant
                {
                    ProductOptionId = createProductVarriant.ProductOptionId,
                    SizeId = createProductVarriant.SizeId,
                    Quantity = createProductVarriant.Quantity,
                    Price = createProductVarriant.Price
                };
                _context.ProductVarriants.Add(productVarriant);
                await _context.SaveChangesAsync();
                return new Response
                {
                    StatusCode = 201,
                    Message = "Product varriant created successfully."
                };
            }
            catch (Exception err)
            {
                return new Response
                {
                    StatusCode = 500,
                    Message = $"Failed to create product varriant. Error: {err.Message}"
                };
            }
        }

        public async Task<Response> DeleteProductVarriant(string productVarriantId)
        {
            try
            {
                var productVarriant = await _context.ProductVarriants
                    .FirstOrDefaultAsync(pv => pv.ProductOptionId == productVarriantId);
                if (productVarriant == null)
                {
                    return new Response
                    {
                        StatusCode = 404,
                        Message = "Product varriant not found."
                    };
                }
                _context.ProductVarriants.Remove(productVarriant);
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
                    StatusCode = 500,
                    Message = $"Failed to delete product varriant. Error: {err.Message}"
                };
            }
        }

        public async Task<IEnumerable<ResponseProductVarriant>> GetAllOfProduct(string productId)
        {
            try
            {
                return await _context.ProductVarriants
                    .Where(pv => pv.ProductOption.ProductId == productId)
                    .Select(pv => new ResponseProductVarriant
                    {
                        ProductOptionId = pv.ProductOptionId,
                        SizeId = pv.SizeId,
                        Quantity = pv.Quantity,
                        Price = pv.Price,
                    }).ToListAsync();
            }
            catch (Exception err)
            {
                Console.WriteLine($"Fail to get all product varriants of product - {productId} \nError: {err.Message}");
                throw new RpcException(new Status(StatusCode.Internal, "Internal Error"));
            }
        }

        public async Task<IEnumerable<ResponseProductVarriant>> GetAllOfProductOption(string productOptionId)
        {
            try
            {
                return await _context.ProductVarriants
                    .Where(pv => pv.ProductOptionId == productOptionId)
                    .Select(pv => new ResponseProductVarriant
                    {
                        ProductOptionId = pv.ProductOptionId,
                        SizeId = pv.SizeId,
                        Quantity = pv.Quantity,
                        Price = pv.Price,
                    }).ToListAsync();
            }
            catch (Exception err)
            {
                Console.WriteLine($"Fail to get all product varriants of option - {productOptionId} \nError: {err.Message}");
                throw new RpcException(new Status(StatusCode.Internal, "Internal Error"));
            }
        }

        public async Task<IEnumerable<ResponseProductVarriant>> GetAllProductVarriants()
        {
            try
            {
                return await _context.ProductVarriants
                    .Select(pv => new ResponseProductVarriant
                    {
                        ProductOptionId = pv.ProductOptionId,
                        SizeId = pv.SizeId,
                        Quantity = pv.Quantity,
                        Price = pv.Price,
                    }).ToListAsync();
            }
            catch (Exception err)
            {
                Console.WriteLine($"Fail to get all product varriants \nError: {err.Message}");
                throw new RpcException(new Status(StatusCode.Internal, "Internal Error"));
            }
        }

        public async Task<ResponseProductVarriant?> GetProductVarriant(string optionId, string sizeId)
        {
            try
            {
                return await _context.ProductVarriants
                    .Where(pv => pv.ProductOptionId == optionId && pv.SizeId == sizeId)
                    .Select(pv => new ResponseProductVarriant
                    {
                        ProductOptionId = pv.ProductOptionId,
                        SizeId = pv.SizeId,
                        Quantity = pv.Quantity,
                        Price = pv.Price
                    }).FirstOrDefaultAsync();
            }
            catch (Exception err)
            {
                Console.WriteLine($"Fail to get a product varriants \nError: {err.Message}");
                return null;
                throw new RpcException(new Status(StatusCode.Internal, "Internal Error"));
            }
        }

        public async Task<Response> UpdateProductVarriant(RequestUpdateProductVarriant updateProductVarriant)
        {
            try
            {
                var productVarriant = await _context.ProductVarriants
                    .FirstOrDefaultAsync(pv => pv.ProductOptionId == updateProductVarriant.Id);
                if (productVarriant == null)
                {
                    return new Response
                    {
                        StatusCode = 404,
                        Message = "Product varriant not found."
                    };
                }
                productVarriant.SizeId = updateProductVarriant.SizeId;
                productVarriant.Quantity = updateProductVarriant.Quantity;
                productVarriant.Price = updateProductVarriant.Price;
                _context.ProductVarriants.Update(productVarriant);
                await _context.SaveChangesAsync();
                return new Response
                {
                    StatusCode = 200,
                    Message = "Product varriant updated successfully."
                };
            }
            catch (Exception err)
            {
                return new Response
                {
                    StatusCode = 500,
                    Message = $"Failed to update product varriant. Error: {err.Message}"
                };
            }
        }
    }
}
