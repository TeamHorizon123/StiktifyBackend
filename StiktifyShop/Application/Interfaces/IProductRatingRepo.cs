using StiktifyShop.Application.DTOs.Requests;
using StiktifyShop.Application.DTOs.Responses;

namespace StiktifyShop.Application.Interfaces
{
    public interface IProductRatingRepo
    {
        IQueryable<ResponseProductRating> GetAll(string productId);
        Task<ResponseProductRating?> Get(string ratingId);
        Task<Response> Create(CreateProducRating rating);
        Task<Response> Delete(string ratingId);
    }
}
