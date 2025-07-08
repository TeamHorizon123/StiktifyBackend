using Domain.Requests;
using Domain.Responses;
using StiktifyShopBackend.Interfaces;
using StiktifyShopBackend.Order;

namespace StiktifyShopBackend.Providers
{
    public class OrderProvider : IOrderProvider
    {
        private OrderGrpc.OrderGrpcClient _client;
        private IAddressProvider _addressProvider;
        private IProductItemProvider _productItemProvider;

        public OrderProvider
            (OrderGrpc.OrderGrpcClient client,
            IAddressProvider addressProvider,
            IProductItemProvider productItemProvider)
        {
            _client = client ?? throw new ArgumentException(nameof(_client));
            _addressProvider = addressProvider ?? throw new ArgumentException(nameof(addressProvider));
            _productItemProvider = productItemProvider ?? throw new ArgumentException(nameof(productItemProvider));
        }

        public async Task<Domain.Responses.Response> CreateOrder(RequestCreateOrder createOrder)
        {
            var createGrpc = new CreateOrder
            {
                UserId = createOrder.UserId,
                AddressId = createOrder.AddressId,
                ProductItemId = createOrder.ProductItemId,
                ProductId = createOrder.ProductId,
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
                ProductId = item.ProductId,
                ProductItemId = item.ProductItemId,
                Price = item.Price,
                Quantity = item.Quantity,
                Status = item.Status,
                UserId = item.UserId,
                ShippingFee = item.ShippingFee,
                Address = _addressProvider.GetOne(item.AddressId).Result,
                ProductItem = _productItemProvider.GetOne(item.ProductItemId).Result,
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
                ProductId = item.ProductId,
                ProductItemId = item.ProductItemId,
                ShippingFee = item.ShippingFee,
                Price = item.Price,
                Quantity = item.Quantity,
                Status = item.Status,
                UserId = item.UserId,
                Address = _addressProvider.GetOne(item.AddressId).Result,
                ProductItem = _productItemProvider.GetOne(item.ProductItemId).Result,
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
                ProductId = item.ProductId,
                ProductItemId = item.ProductItemId,
                ShippingFee = item.ShippingFee,
                Price = item.Price,
                Quantity = item.Quantity,
                Status = item.Status,
                UserId = item.UserId,
                Address = _addressProvider.GetOne(item.AddressId).Result,
                ProductItem = _productItemProvider.GetOne(item.ProductItemId).Result,
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
                ProductId = item.ProductId,
                ProductItemId = item.ProductItemId,
                ShippingFee = item.ShippingFee,
                Price = item.Price,
                Quantity = item.Quantity,
                Status = item.Status,
                UserId = item.UserId,
                Address = _addressProvider.GetOne(item.AddressId).Result,
                ProductItem = _productItemProvider.GetOne(item.ProductItemId).Result,
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
                ProductId = orderGrpc.ProductId,
                ProductItemId = orderGrpc.ProductItemId,
                ShippingFee = orderGrpc.ShippingFee,
                Price = orderGrpc.Price,
                Quantity = orderGrpc.Quantity,
                Status = orderGrpc.Status,
                UserId = orderGrpc.UserId,
                Address = await _addressProvider.GetOne(orderGrpc.AddressId),
                ProductItem = await _productItemProvider.GetOne(orderGrpc.ProductItemId),
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
                ProductItemId = updateOrder.ProductItemId,
                ProductId = updateOrder.ProductId,
                ShippingFee = updateOrder.ShippingFee,
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
