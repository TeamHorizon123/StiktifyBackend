using Domain.Requests;
using Domain.Responses;

namespace GrpcServiceProduct.Interfaces
{
    public interface IProductOptionRepository
    {
        Task<IEnumerable<ResponseProductOption>> GetAll();
        Task<IEnumerable<ResponseProductOption>> GetAllOfProduct(string productId);
        Task<ResponseProductOption?> GetOne(string optionId);
        Task<Response> CreateProductOption(RequestCreateOption createOption);
        Task<Response> CreateManyProductOption(RequestCreateOption[] createOption);
        Task<Response> UpdateProductOption(RequestUpdateOption updateOption);
        Task<Response> DeleteProductOption(string optionId);
        Task<Response> DeleteManyProductOption(string[] listOptionId);
    }
}
