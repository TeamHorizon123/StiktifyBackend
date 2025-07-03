using Domain.Requests;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcServiceOrder.Cart;
using GrpcServiceOrder.Interfaces;

namespace GrpcServiceOrder.Services
{
    public class CartGrpcService : CartGrpc.CartGrpcBase
    {
        private ICartRepository _repo;

        public CartGrpcService(ICartRepository repo)
        {
            _repo = repo ?? throw new ArgumentException(nameof(_repo));
        }

        public override async Task<Carts> GetAll(Cart.Empty request, ServerCallContext context)
        {
            var listCart = await _repo.GetAll();
            Carts grpcList = new Carts();
            grpcList.Item.AddRange(listCart.Select(cart => new Cart.Cart
            {
                Id = cart.Id,
                OptionId = cart.SizeColoId,
                UserId = cart.UserId,
                Quantity = cart.Quantity,
                CreateAt = Timestamp.FromDateTime(cart.CreateAt!.Value.ToUniversalTime()),
                UpdateAt = Timestamp.FromDateTime(cart.UpdateAt!.Value.ToUniversalTime()),
            }));
            return grpcList;
        }

        public override async Task<Carts> GetAllOfProduct(Id request, ServerCallContext context)
        {
            var listCart = await _repo.GetAllOfProduct(request.SearchId);
            Carts grpcList = new Carts();
            grpcList.Item.AddRange(listCart.Select(cart => new Cart.Cart
            {
                Id = cart.Id,
                OptionId = cart.SizeColoId,
                UserId = cart.UserId,
                Quantity = cart.Quantity,
                CreateAt = Timestamp.FromDateTime(cart.CreateAt!.Value.ToUniversalTime()),
                UpdateAt = Timestamp.FromDateTime(cart.UpdateAt!.Value.ToUniversalTime()),
            }));
            return grpcList;
        }

        public override async Task<Carts> GetAllOfUser(Id request, ServerCallContext context)
        {
            var listCart = await _repo.GetAllOfUser(request.SearchId);
            Carts grpcList = new Carts();
            grpcList.Item.AddRange(listCart.Select(cart => new Cart.Cart
            {
                Id = cart.Id,
                OptionId = cart.SizeColoId,
                UserId = cart.UserId,
                Quantity = cart.Quantity,
                CreateAt = Timestamp.FromDateTime(cart.CreateAt!.Value.ToUniversalTime()),
                UpdateAt = Timestamp.FromDateTime(cart.UpdateAt!.Value.ToUniversalTime()),
            }));
            return grpcList;
        }

        public override async Task<Cart.Cart> GetOne(Id request, ServerCallContext context)
        {
            var cart = await _repo.GetOne(request.SearchId);
            if (cart == null)
                return new Cart.Cart();
            return new Cart.Cart
            {
                Id = cart.Id,
                OptionId = cart.SizeColoId,
                UserId = cart.UserId,
                Quantity = cart.Quantity,
                CreateAt = Timestamp.FromDateTime(cart.CreateAt!.Value.ToUniversalTime()),
                UpdateAt = Timestamp.FromDateTime(cart.UpdateAt!.Value.ToUniversalTime()),
            };
        }

        public override async Task<Response> Create(CreateCart request, ServerCallContext context)
        {
            var createCart = new RequestCreateCart
            {
                UserId = request.UserId,
                SizeColor = request.OptionId,
                Quantity = request.Quantity,
            };
            var response = await _repo.CreateCart(createCart);
            return new Response { Message = response.Message, StatusCode = response.StatusCode };
        }

        public override async Task<Response> Update(Cart.Cart request, ServerCallContext context)
        {
            var updateCart = new RequestUpdateCart
            {
                Id = request.Id,
                SizeColor = request.OptionId,
                UserId = request.UserId,
                Quantity = request.Quantity
            };
            var response = await _repo.UpdateCart(updateCart);
            return new Response { Message = response.Message, StatusCode = response.StatusCode };
        }

        public override async Task<Response> Delete(Id request, ServerCallContext context)
        {
            var response = await _repo.DeleteCart(request.SearchId);
            return new Response { Message = response.Message, StatusCode = response.StatusCode };
        }

        public override async Task<Response> DeleteMany(Ids request, ServerCallContext context)
        {
            var listRemove = request.Item.Select(cart => cart.SearchId).ToList();
            var response = await _repo.DeleteManyCart(listRemove);
            return new Response { Message = response.Message, StatusCode = response.StatusCode };
        }
    }
}
