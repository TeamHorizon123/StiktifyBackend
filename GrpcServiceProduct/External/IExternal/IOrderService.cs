using Domain.Responses;

namespace GrpcServiceProduct.External.IExternal
{
    public interface IOrderService
    {
        IEnumerable<ResponseOrder> GetAllOfProduct(string productId);
    }
}
