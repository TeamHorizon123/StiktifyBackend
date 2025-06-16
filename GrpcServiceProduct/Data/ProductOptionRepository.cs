using Domain.Entities;
using Domain.Requests;
using Domain.Responses;
using GrpcServiceProduct.Interfaces;

namespace GrpcServiceProduct.Data
{
    public class ProductOptionRepository : IProductOptionRepository
    {
        public Task<Response> CreateManyProductOption(RequestCreateOption[] createOption)
        {
            throw new NotImplementedException();
        }

        public Task<Response> CreateProductOption(RequestCreateOption createOption)
        {
            throw new NotImplementedException();
        }

        public Task<Response> DeleteManyProductOption(string[] listOptionId)
        {
            throw new NotImplementedException();
        }

        public Task<Response> DeleteProductOption(string optionId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductOption>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductOption>> GetAllOfProduct(string productId)
        {
            throw new NotImplementedException();
        }

        public Task<ProductOption?> GetOne(string optionId)
        {
            throw new NotImplementedException();
        }

        public Task<Response> UpdateProductOption(RequestUpdateOption updateOption)
        {
            throw new NotImplementedException();
        }
    }
}
