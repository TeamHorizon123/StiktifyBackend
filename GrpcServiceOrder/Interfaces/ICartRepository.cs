using Domain.Requests;
using Domain.Responses;

namespace GrpcServiceOrder.Interfaces
{
    public interface ICartRepository
    {
        Task<IEnumerable<ResponseCart>> GetAll();
        Task<IEnumerable<ResponseCart>> GetAllOfUser(string userID);
        Task<ResponseCart?> GetOne(string cartId);
        Task<Response> CreateCart(RequestCreateCart createCart);
        Task<Response> UpdateCart(RequestUpdateCart updateCart);
        Task<Response> DeleteCart(string cartId);
        Task<Response> DeleteManyCart(ICollection<string> ids);
    }
}
