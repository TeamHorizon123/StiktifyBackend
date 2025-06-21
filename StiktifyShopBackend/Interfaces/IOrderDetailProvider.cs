using Domain.Requests;
using Domain.Responses;

namespace StiktifyShopBackend.Interfaces
{
    public interface IOrderDetailProvider
    {
        Task<ResponseOrderDetail?> GetOne(string detailsId);
        Task<Response> CreateDetail(RequestCreateOrderDetail createOrderDetail);
        Task<Response> UpdateDetail(RequestUpdateOrderDetail updateOrderDetail);
    }
}
