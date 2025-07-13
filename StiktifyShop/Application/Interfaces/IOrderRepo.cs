using StiktifyShop.Application.DTOs.Requests;
using StiktifyShop.Application.DTOs.Responses;

namespace StiktifyShop.Application.Interfaces
{
    public interface IOrderRepo
    {
        IQueryable<ResponseOrder> GetAll();
        Task<ResponseOrder?> Get(string orderId);
        Task<Response> Create(CreateOrder order);
        Task<Response> Update(UpdateOrder order);
    }
}
