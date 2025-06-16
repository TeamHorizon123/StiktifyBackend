using Domain.Requests;
using Domain.Responses;

namespace GrpcServiceProduct.Interfaces
{
    public interface IProductRatingRepository
    {
        Task<IEnumerable<Domain.Entities.ProductRating>> GetAll();
        Task<IEnumerable<Domain.Entities.ProductRating>> GetAllOfProduct(string productId);
        Task<IEnumerable<Domain.Entities.ProductRating>> GetAllOfOption(string optionId);
        Task<Domain.Entities.ProductRating?> GetOne(string productId);
        Task<Response> CreateRating(RequestCreateRating createRating);
        Task<Response> UpdateRating(RequestUpdateRating updateRating);
        Task<Response> DeleteRating(string productId);
    }
}
