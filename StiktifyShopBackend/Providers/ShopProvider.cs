using Domain.Requests;
using Domain.Responses;
using Google.Protobuf.WellKnownTypes;
using StiktifyShopBackend.Interfaces;
using StiktifyShopBackend.Shop;

namespace StiktifyShopBackend.Providers
{
    public class ShopProvider : IShopProvider
    {
        private ShopGrpc.ShopGrpcClient _client;

        public ShopProvider(ShopGrpc.ShopGrpcClient client)
        {
            _client = client;
        }

        public async Task<Domain.Responses.Response> CreateShop(RequestCreateShop shop)
        {
            var grpcCreateShop = new CreateShop
            {
                UserId = shop.UserId,
                ShopName = shop.ShopName,
                Avatar = shop.Avatar,
                IsBanned = shop.IsBanned,
                Location = shop.Location,
                ShopType = shop.ShopType,
            };
            var grpcResponse = await _client.CreateAsync(grpcCreateShop);
            return new Domain.Responses.Response { Message = grpcResponse.Message, StatusCode = grpcResponse.StatusCode };
        }

        public async Task<Domain.Responses.Response> DeleteShop(string shopId)
        {
            var grpcResponse = await _client.DeleteAsync(new Id { SearchId = shopId });
            return new Domain.Responses.Response { Message = grpcResponse.Message, StatusCode = grpcResponse.StatusCode };
        }

        public IQueryable<ResponseShop> GetAll()
        {
            var grpcList = _client.GetAll(new Shop.Empty());
            var shopList = grpcList.Item.Select(shop
                => new ResponseShop
                {
                    Id = shop.Id,
                    UserId = shop.UserId,
                    Avatar = shop.Avatar,
                    IsBanned = shop.IsBanned,
                    Location = shop.Location,
                    Rating = shop.Rating,
                    ShopName = shop.ShopName,
                    ShopType = shop.ShopType,
                    CreateAt = shop.CreateAt.ToDateTime(),
                    UpdateAt = shop.UpdateAt.ToDateTime(),
                });
            return shopList.AsQueryable();
        }

        public async Task<ResponseShop?> GetOfUser(string userId)
        {
            var grpcShop = await _client.GetOfUserAsync(new Id { SearchId = userId });
            if (grpcShop.Id == null)
                return null;
            var shop = new ResponseShop
            {
                Id = grpcShop.Id,
                UserId = grpcShop.UserId,
                Avatar = grpcShop.Avatar,
                IsBanned = grpcShop.IsBanned,
                Location = grpcShop.Location,
                Rating = grpcShop.Rating,
                ShopName = grpcShop.ShopName,
                ShopType = grpcShop.ShopType,
                CreateAt = grpcShop.CreateAt.ToDateTime(),
                UpdateAt = grpcShop.UpdateAt.ToDateTime(),
            };
            return shop;
        }

        public async Task<ResponseShop?> GetOne(string shopId)
        {
            var grpcShop = await _client.GetOneAsync(new Id { SearchId = shopId });
            if (grpcShop.Id == null)
                return null;
            var shop = new ResponseShop
            {
                Id = grpcShop.Id,
                UserId = grpcShop.UserId,
                Avatar = grpcShop.Avatar,
                IsBanned = grpcShop.IsBanned,
                Location = grpcShop.Location,
                Rating = grpcShop.Rating,
                ShopName = grpcShop.ShopName,
                ShopType = grpcShop.ShopType,
                CreateAt = grpcShop.CreateAt.ToDateTime(),
                UpdateAt = grpcShop.UpdateAt.ToDateTime(),
            };
            return shop;
        }

        public async Task<Domain.Responses.Response> UpdateShop(RequestUpdateShop shop)
        {
            var grpcUpdateShop = new Shop.Shop
            {
                Id = shop.Id,
                UserId = shop.UserId,
                Avatar = shop.Avatar,
                IsBanned = shop.IsBanned,
                Location = shop.Location,
                ShopName = shop.ShopName,
                ShopType = shop.ShopType,
                //CreateAt = Timestamp.FromDateTime(shop.CreateAt.ToUniversalTime())
            };
            var grpcResponse = await _client.UpdateAsync(grpcUpdateShop);
            return new Domain.Responses.Response { Message = grpcResponse.Message, StatusCode = grpcResponse.StatusCode };
        }
    }
}
