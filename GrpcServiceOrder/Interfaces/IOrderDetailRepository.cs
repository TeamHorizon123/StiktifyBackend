using Domain.Requests;
using Domain.Responses;

namespace GrpcServiceOrder.Interfaces
{
    public interface IOrderDetailRepository
    {
        Task<IEnumerable<ResponseOrderDetail>> GetAllOfOrder(string orderId);
        Task<ResponseOrderDetail?> GetOne(string detailsId);
        Task<Response> CreateDetail(RequestCreateOrderDetail createOrderDetail);
        Task<Response> UpdateDetail(RequestUpdateOrderDetail updateOrderDetail);
    }
}
