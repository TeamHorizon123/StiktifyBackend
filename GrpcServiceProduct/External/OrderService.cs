using Domain.Responses;
using GrpcServiceProduct.External.IExternal;
using GrpcServiceProduct.Order;

namespace GrpcServiceProduct.External
{
    public class OrderService : IOrderService
    {
        private readonly OrderGrpc.OrderGrpcClient _client;
        public OrderService(OrderGrpc.OrderGrpcClient client)
        {
            _client = client ?? throw new ArgumentException(nameof(client));
        }
        public IEnumerable<ResponseOrder> GetAllOfProduct(string productId)
        {
            var list = _client.GetAllOfProduct(new Id { SearchId = productId });
            return list.Item.Select(order => new ResponseOrder
            {
                Id = order.Id,
                UserId = order.UserId,
                AddressId = order.AddressId,
                ProductId = order.ProductId,
                Quantity = order.Quantity,
                Price = order.Price,
                ShippingFee = order.ShippingFee,
                Status = order.Status,
                CreateAt = order.CreateAt.ToDateTime(),
                UpdateAt = order.UpdateAt.ToDateTime()
            });
        }
    }
}
