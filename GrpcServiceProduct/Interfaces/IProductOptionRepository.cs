using Domain.Requests;
using Domain.Responses;

namespace GrpcServiceProduct.Interfaces
{
    public interface IProductOptionRepository
    {
        Task<IEnumerable<Domain.Entities.ProductOption>> GetAll();
        Task<IEnumerable<Domain.Entities.ProductOption>> GetAllOfProduct(string productId);
        Task<Domain.Entities.ProductOption?> GetOne(string optionId);
        Task<Response> CreateProductOption(RequestCreateOption createOption);
        Task<Response> CreateManyProductOption(RequestCreateOption[] createOption);
        Task<Response> UpdateProductOption(RequestUpdateOption updateOption);
        Task<Response> DeleteProductOption(string optionId);
        Task<Response> DeleteManyProductOption(string[] listOptionId);
    }
}
