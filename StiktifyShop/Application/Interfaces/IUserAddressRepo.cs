using StiktifyShop.Application.DTOs.Requests;
using StiktifyShop.Application.DTOs.Responses;
using StiktifyShop.Domain.Entity;

namespace StiktifyShop.Application.Interfaces
{
    public interface IUserAddressRepo
    {
        IQueryable<ResponseUserAddress> GetAll();
        Task<Response> Create(CreateUserAddress userAddress);
        Task<Response> Update(UpdateUserAddress userAddress);
        Task<Response> Delete(string id);
    }
}
