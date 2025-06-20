using Domain.Requests;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcServiceOrder.Interfaces;
using GrpcServiceOrder.Order;

namespace GrpcServiceOrder.Services
{
    public class OrderGrpcService : OrderGrpc.OrderGrpcBase
    {
        private IOrderRepository _repo;

        public OrderGrpcService(IOrderRepository repo)
        {
            _repo = repo ?? throw new ArgumentException(nameof(_repo));
        }

        public override async Task<Orders> GetAll(Order.Empty request, ServerCallContext context)
        {
            var listOrder = await _repo.GetAll();
            Orders orders = new Orders();
            orders.Item.AddRange(listOrder.Select(order
                => new Order.Order
                {
                    Id = order.Id,
                    AddressId = order.AddressId,
                    UserId = order.UserId,
                    ProductId = order.ProductId,
                    OptionId = order.OptionId,
                    Quantity = order.Quantity,
                    Price = order.Price,
                    Discount = order.Discount,
                    ShippingFee = order.ShippingFee,
                    Status = order.Status,
                    CreateAt = Timestamp.FromDateTime(order.CreateAt!.Value.ToUniversalTime()),
                    UpdateAt = Timestamp.FromDateTime(order.UpdateAt!.Value.ToUniversalTime())
                }));
            return orders;
        }

        public override async Task<Orders> GetAllOfProduct(Id request, ServerCallContext context)
        {
            var listOrder = await _repo.GetAllOfProduct(request.SearchId);
            Orders orders = new Orders();
            orders.Item.AddRange(listOrder.Select(order
                => new Order.Order
                {
                    Id = order.Id,
                    AddressId = order.AddressId,
                    UserId = order.UserId,
                    ProductId = order.ProductId,
                    OptionId = order.OptionId,
                    Quantity = order.Quantity,
                    Price = order.Price,
                    Discount = order.Discount,
                    ShippingFee = order.ShippingFee,
                    Status = order.Status,
                    CreateAt = Timestamp.FromDateTime(order.CreateAt!.Value.ToUniversalTime()),
                    UpdateAt = Timestamp.FromDateTime(order.UpdateAt!.Value.ToUniversalTime())
                }));
            return orders;
        }

        public override async Task<Orders> GetAllOfShop(Id request, ServerCallContext context)
        {
            var listOrder = await _repo.GetAllOfShop(request.SearchId);
            Orders orders = new Orders();
            orders.Item.AddRange(listOrder.Select(order
                => new Order.Order
                {
                    Id = order.Id,
                    AddressId = order.AddressId,
                    UserId = order.UserId,
                    ProductId = order.ProductId,
                    OptionId = order.OptionId,
                    Quantity = order.Quantity,
                    Price = order.Price,
                    Discount = order.Discount,
                    ShippingFee = order.ShippingFee,
                    Status = order.Status,
                    CreateAt = Timestamp.FromDateTime(order.CreateAt!.Value.ToUniversalTime()),
                    UpdateAt = Timestamp.FromDateTime(order.UpdateAt!.Value.ToUniversalTime())
                }));
            return orders;
        }

        public override async Task<Orders> GetAllOfUser(Id request, ServerCallContext context)
        {
            var listOrder = await _repo.GetAllOfUser(request.SearchId);
            Orders orders = new Orders();
            orders.Item.AddRange(listOrder.Select(order
                => new Order.Order
                {
                    Id = order.Id,
                    AddressId = order.AddressId,
                    UserId = order.UserId,
                    ProductId = order.ProductId,
                    OptionId = order.OptionId,
                    Quantity = order.Quantity,
                    Price = order.Price,
                    Discount = order.Discount,
                    ShippingFee = order.ShippingFee,
                    Status = order.Status,
                    CreateAt = Timestamp.FromDateTime(order.CreateAt!.Value.ToUniversalTime()),
                    UpdateAt = Timestamp.FromDateTime(order.UpdateAt!.Value.ToUniversalTime())
                }));
            return orders;
        }

        public override async Task<Order.Order> GetOne(Id request, ServerCallContext context)
        {
            var order = await _repo.GetOne(request.SearchId);
            if (order == null)
                return new Order.Order();
            return new Order.Order
            {
                Id = order.Id,
                UserId = order.UserId,
                AddressId = order.AddressId,
                ProductId = order.ProductId,
                OptionId = order.OptionId,
                Quantity = order.Quantity,
                Discount = order.Discount,
                ShippingFee = order.ShippingFee,
                Price = order.Price,
                Status = order.Status,
                CreateAt = Timestamp.FromDateTime(order.CreateAt!.Value.ToUniversalTime()),
                UpdateAt = Timestamp.FromDateTime(order.UpdateAt!.Value.ToUniversalTime())
            };
        }

        public override async Task<Response> Create(CreateOrder request, ServerCallContext context)
        {
            var createOrder = new RequestCreateOrder
            {
                UserId = request.UserId,
                AddressId = request.AddressId,
                ProductId = request.ProductId,
                OptionId = request.OptionId,
                Quantity = request.Quantity,
                Discount = request.Discount,
                ShippingFee = request.ShippingFee,
                Price = request.Price,
                Status = request.Status,
            };
            var response = await _repo.CreateOrder(createOrder);
            return new Response { Message = response.Message, StatusCode = response.StatusCode };
        }

        public override async Task<Response> Update(Order.Order request, ServerCallContext context)
        {
            var updateOrder = new RequestUpdateOrder
            {
                Id = request.Id,
                UserId = request.UserId,
                AddressId = request.AddressId,
                ProductId = request.ProductId,
                OptionId = request.OptionId,
                Quantity = request.Quantity,
                Discount = request.Discount,
                ShippingFee = request.ShippingFee,
                Price = request.Price,
                Status = request.Status
            };
            var response = await _repo.UpdateOrder(updateOrder);
            return new Response { Message = response.Message, StatusCode = response.StatusCode };
        }
    }
}
