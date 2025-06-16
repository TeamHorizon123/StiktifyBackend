using Domain.Requests;
using Domain.Responses;

namespace GrpcServiceUser.Interface
{
    public interface IAddressRepository
    {
        Task<IEnumerable<ResponseReceiveAddress>> GetAllOfUser(string userId);
        Task<ResponseReceiveAddress?> GetOne(string addressId);
        Task<Response> CreateAddress(RequestCreateAddress address);
        Task<Response> UpdateAddress(RequestUpdateAddress address);
        Task<Response> DeleteAddress(string addressId);
    }
}
