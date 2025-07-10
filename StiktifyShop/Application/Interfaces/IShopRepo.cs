using StiktifyShop.Application.DTOs.Requests;
using StiktifyShop.Application.DTOs.Responses;

namespace StiktifyShop.Application.Interfaces
{
    public interface IShopRepo
    {
        IQueryable<ResponseShop> GetAll();
        Task<ResponseShop?> Get(string shopId);
        Task<Response> Create(CreateShop shop);
        Task<Response> Update(UpdateShop shop);
        Task<Response> Delete(string shopId);
    }
}
