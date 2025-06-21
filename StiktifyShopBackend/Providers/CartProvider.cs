using Domain.Requests;
using Domain.Responses;
using StiktifyShopBackend.Cart;
using StiktifyShopBackend.Interfaces;

namespace StiktifyShopBackend.Providers
{
    public class CartProvider : ICartProvider
    {
        private CartGrpc.CartGrpcClient _client;

        public CartProvider(CartGrpc.CartGrpcClient client)
        {
            _client = client ?? throw new ArgumentException(nameof(_client));
        }

        public async Task<Domain.Responses.Response> CreateCart(RequestCreateCart createCart)
        {
            var createGrpc = new CreateCart
            {
                OptionId = createCart.OptionId,
                ProductId = createCart.ProductId,
                Quantity = createCart.Quantity,
                UserId = createCart.UserId
            };
            var response = await _client.CreateAsync(createGrpc);
            return new Domain.Responses.Response { Message = response.Message, StatusCode = response.StatusCode };
        }

        public async Task<Domain.Responses.Response> DeleteCart(string cartId)
        {
            var response = await _client.DeleteAsync(new Id { SearchId = cartId });
            return new Domain.Responses.Response { Message = response.Message, StatusCode = response.StatusCode };
        }

        public async Task<Domain.Responses.Response> DeleteManyCart(ICollection<string> ids)
        {
            var listRemove = new Ids();
            listRemove.Item.AddRange(ids.Select(item => new Id { SearchId = item }));
            var response = await _client.DeleteManyAsync(listRemove);
            return new Domain.Responses.Response { Message = response.Message, StatusCode = response.StatusCode };
        }

        public IQueryable<ResponseCart> GetAll()
        {
            var listGrpc = _client.GetAll(new Cart.Empty());
            var list = listGrpc.Item.Select(item => new ResponseCart
            {
                Id = item.Id,
                OptionId = item.OptionId,
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                UserId = item.UserId,
                CreateAt = item.CreateAt.ToDateTime(),
                UpdateAt = item.UpdateAt.ToDateTime(),
            });
            return list.AsQueryable();
        }

        public IQueryable<ResponseCart> GetAllOfProduct(string productID)
        {
            var listGrpc = _client.GetAllOfProduct(new Id { SearchId = productID });
            var list = listGrpc.Item.Select(item => new ResponseCart
            {
                Id = item.Id,
                OptionId = item.OptionId,
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                UserId = item.UserId,
                CreateAt = item.CreateAt.ToDateTime(),
                UpdateAt = item.UpdateAt.ToDateTime(),
            });
            return list.AsQueryable();
        }

        public IQueryable<ResponseCart> GetAllOfUser(string userID)
        {
            var listGrpc = _client.GetAllOfUser(new Id { SearchId = userID });
            var list = listGrpc.Item.Select(item => new ResponseCart
            {
                Id = item.Id,
                OptionId = item.OptionId,
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                UserId = item.UserId,
                CreateAt = item.CreateAt.ToDateTime(),
                UpdateAt = item.UpdateAt.ToDateTime(),
            });
            return list.AsQueryable();
        }

        public async Task<ResponseCart?> GetOne(string cartId)
        {
            var cartGrpc = await _client.GetOneAsync(new Id { SearchId = cartId });
            return new ResponseCart
            {
                Id = cartGrpc.Id,
                OptionId = cartGrpc.OptionId,
                ProductId = cartGrpc.ProductId,
                Quantity = cartGrpc.Quantity,
                UserId = cartGrpc.UserId,
                CreateAt = cartGrpc.CreateAt.ToDateTime(),
                UpdateAt = cartGrpc.UpdateAt.ToDateTime(),
            };
        }

        public async Task<Domain.Responses.Response> UpdateCart(RequestUpdateCart updateCart)
        {
            var updateGrpc = new Cart.Cart
            {
                Id = updateCart.Id,
                UserId = updateCart.UserId,
                Quantity = updateCart.Quantity,
                ProductId = updateCart.ProductId,
                OptionId = updateCart.OptionId
            };
            var response = await _client.UpdateAsync(updateGrpc);
            return new Domain.Responses.Response { Message = response.Message, StatusCode = response.StatusCode };
        }
    }
}
