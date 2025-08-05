using StiktifyShop.Application.DTOs.Requests;
using StiktifyShop.Application.DTOs.Responses;

namespace StiktifyShop.Application.Interfaces
{
    public interface ICartRepo
    {
        IQueryable<ResponseCart> GetAll();
        Task<Response> Create(CreateCart cart);
        Task<Response> Update(UpdateCart cart);
        Task<Response> Delete(string cartId);
        Task<Response> DeleteMany(ICollection<DeleteCart> ids);
    }
}
