using Domain.Requests;
using Domain.Responses;
using StiktifyShopBackend.Interfaces;
using StiktifyShopBackend.Order;

namespace StiktifyShopBackend.Providers
{
    public class OrderProvider : IOrderProvider
    {
        private OrderGrpc.OrderGrpcClient _client;

        public OrderProvider(OrderGrpc.OrderGrpcClient client)
        {
            _client = client ?? throw new ArgumentException(nameof(_client));
        }

        public async Task<Domain.Responses.Response> CreateOrder(RequestCreateOrder createOrder)
        {
            var createGrpc = new CreateOrder
            {
                UserId = createOrder.UserId,
                AddressId = createOrder.AddressId,
                OptionId = createOrder.SizeColor,
                ProductId = createOrder.ProductId,
                Discount = createOrder.Discount,
                Price = createOrder.Price,
                Quantity = createOrder.Quantity,
                ShippingFee = createOrder.ShippingFee,
                Status = createOrder.Status
            };
            var response = await _client.CreateAsync(createGrpc);
            return new Domain.Responses.Response { Message = response.Message, StatusCode = response.StatusCode };
        }

        public IQueryable<ResponseOrder> GetAll()
        {
            var grpcList = _client.GetAll(new Order.Empty());
            var list = grpcList.Item.Select(item => new ResponseOrder
            {
                Id = item.Id,
                AddressId = item.AddressId,
                SizeColorId = item.OptionId,
                ShippingFee = item.ShippingFee,
                Discount = item.Discount,
                Price = item.Price,
                Quantity = item.Quantity,
                Status = item.Status,
                UserId = item.UserId,
                CreateAt = item.CreateAt.ToDateTime(),
                UpdateAt = item.UpdateAt.ToDateTime(),
            });
            return list.AsQueryable();
        }

        public IQueryable<ResponseOrder> GetAllOfProduct(string productId)
        {
            var grpcList = _client.GetAllOfProduct(new Id { SearchId = productId });
            var list = grpcList.Item.Select(item => new ResponseOrder
            {
                Id = item.Id,
                AddressId = item.AddressId,
                SizeColorId = item.OptionId,
                ShippingFee = item.ShippingFee,
                Discount = item.Discount,
                Price = item.Price,
                Quantity = item.Quantity,
                Status = item.Status,
                UserId = item.UserId,
                CreateAt = item.CreateAt.ToDateTime(),
                UpdateAt = item.UpdateAt.ToDateTime(),
            });
            return list.AsQueryable();
        }

        public IQueryable<ResponseOrder> GetAllOfShop(string shopId)
        {
            var grpcList = _client.GetAllOfShop(new Id { SearchId = shopId });
            var list = grpcList.Item.Select(item => new ResponseOrder
            {
                Id = item.Id,
                AddressId = item.AddressId,
                SizeColorId = item.OptionId,
                ShippingFee = item.ShippingFee,
                Discount = item.Discount,
                Price = item.Price,
                Quantity = item.Quantity,
                Status = item.Status,
                UserId = item.UserId,
                CreateAt = item.CreateAt.ToDateTime(),
                UpdateAt = item.UpdateAt.ToDateTime(),
            });
            return list.AsQueryable();
        }

        public IQueryable<ResponseOrder> GetAllOfUser(string userId)
        {
            var grpcList = _client.GetAllOfUser(new Id { SearchId = userId });
            var list = grpcList.Item.Select(item => new ResponseOrder
            {
                Id = item.Id,
                AddressId = item.AddressId,
                SizeColorId = item.OptionId,
                ShippingFee = item.ShippingFee,
                Discount = item.Discount,
                Price = item.Price,
                Quantity = item.Quantity,
                Status = item.Status,
                UserId = item.UserId,
                CreateAt = item.CreateAt.ToDateTime(),
                UpdateAt = item.UpdateAt.ToDateTime(),
            });
            return list.AsQueryable();
        }

        public async Task<ResponseOrder?> GetOne(string orderId)
        {
            var orderGrpc = await _client.GetOneAsync(new Id { SearchId = orderId });
            return new ResponseOrder
            {
                Id = orderGrpc.Id,
                AddressId = orderGrpc.AddressId,
                SizeColorId = orderGrpc.OptionId,
                ShippingFee = orderGrpc.ShippingFee,
                Discount = orderGrpc.Discount,
                Price = orderGrpc.Price,
                Quantity = orderGrpc.Quantity,
                Status = orderGrpc.Status,
                UserId = orderGrpc.UserId,
                CreateAt = orderGrpc.CreateAt.ToDateTime(),
                UpdateAt = orderGrpc.UpdateAt.ToDateTime(),
            };
        }

        public async Task<Domain.Responses.Response> UpdateOrder(RequestUpdateOrder updateOrder)
        {
            var updateGrpc = new Order.Order
            {
                Id = updateOrder.Id,
                AddressId = updateOrder.AddressId,
                OptionId = updateOrder.SizeColor,
                ProductId = updateOrder.ProductId,
                ShippingFee = updateOrder.ShippingFee,
                Discount = updateOrder.Discount,
                Price = updateOrder.Price,
                Quantity = updateOrder.Quantity,
                Status = updateOrder.Status,
                UserId = updateOrder.UserId,
            };
            var response = await _client.UpdateAsync(updateGrpc);
            return new Domain.Responses.Response { Message = response.Message, StatusCode = response.StatusCode };
        }
    }
}
