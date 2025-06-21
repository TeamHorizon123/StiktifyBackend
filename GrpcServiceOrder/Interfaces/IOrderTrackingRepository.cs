using Domain.Requests;
using Domain.Responses;

namespace GrpcServiceOrder.Interfaces
{
    public interface IOrderTrackingRepository
    {
        Task<IEnumerable<ResponseOrderTracking>> GetAllOfOrder(string orderId);
        Task<Response> CreateTracking(RequestCreateTracking createTracking);
    }
}
