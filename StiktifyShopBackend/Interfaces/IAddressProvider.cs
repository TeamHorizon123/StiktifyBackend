using Domain.Requests;
using Domain.Responses;

namespace StiktifyShopBackend.Interfaces
{
    public interface IAddressProvider
    {
        IQueryable<ResponseReceiveAddress> GetAllOfUser(string userId);
        Task<ResponseReceiveAddress?> GetOne(string addressId);
        Task<Response> CreateAddress(RequestCreateAddress address);
        Task<Response> UpdateAddress(RequestUpdateAddress address);
        Task<Response> DeleteAddress(string addressId);
    }
}
