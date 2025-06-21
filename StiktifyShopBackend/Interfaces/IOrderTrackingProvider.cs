using Domain.Requests;
using Domain.Responses;

namespace StiktifyShopBackend.Interfaces
{
    public interface IOrderTrackingProvider
    {
        IQueryable<ResponseOrderTracking> GetAllOfOrder(string orderId);
        Task<Response> CreateTracking(RequestCreateTracking createTracking);
    }
}
