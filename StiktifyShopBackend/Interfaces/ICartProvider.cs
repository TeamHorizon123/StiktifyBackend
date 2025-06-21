using Domain.Requests;
using Domain.Responses;

namespace StiktifyShopBackend.Interfaces
{
    public interface ICartProvider
    {
        IQueryable<ResponseCart> GetAll();
        IQueryable<ResponseCart> GetAllOfUser(string userID);
        IQueryable<ResponseCart> GetAllOfProduct(string productID);
        Task<ResponseCart?> GetOne(string cartId);
        Task<Response> CreateCart(RequestCreateCart createCart);
        Task<Response> UpdateCart(RequestUpdateCart updateCart);
        Task<Response> DeleteCart(string cartId);
        Task<Response> DeleteManyCart(ICollection<string> ids);
    }
}
