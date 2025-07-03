using Domain.Requests;
using Domain.Responses;

namespace GrpcServiceProduct.Interfaces
{
    public interface IProductRatingRepository
    {
        Task<IEnumerable<ResponseProductRating>> GetAll();
        Task<IEnumerable<ResponseProductRating>> GetAllOfProduct(string productId);
        Task<IEnumerable<ResponseProductRating>> GetAllOfOption(string itemId);
        Task<ResponseProductRating?> GetOne(string ratingId);
        Task<Response> CreateRating(RequestCreateRating createRating);
        Task<Response> UpdateRating(RequestUpdateRating updateRating);
        Task<Response> DeleteRating(string productId);
    }
}
