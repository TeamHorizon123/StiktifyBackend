using Domain.Requests;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcServiceUser.Interface;
using GrpcServiceUser.Shop;

namespace GrpcServiceUser.Services
{
    public class ShopGrpcService : ShopGrpc.ShopGrpcBase
    {
        private IShopRepository _repo;

        public ShopGrpcService(IShopRepository repo)
        {
            _repo = repo ?? throw new ArgumentException(nameof(_repo));
        }

        public override async Task<Shops> GetAll(Shop.Empty request, ServerCallContext context)
        {
            var listShop = await _repo.GetAll();
            Shop.Shops grpcList = new Shop.Shops();
            grpcList.Item.AddRange(listShop.Select(shop
                => new Shop.Shop
                {
                    Id = shop.Id,
                    UserId = shop.UserId,
                    ShopName = shop.ShopName,
                    ShopType = shop.ShopType,
                    Location = shop.Location,
                    IsBanned = shop.IsBanned,
                    Avatar = shop.Avatar,
                    Rating = shop.Rating,
                    CreateAt = Timestamp.FromDateTime(shop.CreateAt!.Value.ToUniversalTime()),
                    UpdateAt = Timestamp.FromDateTime(shop.UpdateAt!.Value.ToUniversalTime())
                }));
            return grpcList;
        }

        public override async Task<Shop.Shop> GetOne(Id request, ServerCallContext context)
        {
            var shop = await _repo.GetOne(request.SearchId);
            if (shop == null)
                return new Shop.Shop();
            Shop.Shop grpcShop = new Shop.Shop
            {
                Id = shop.Id,
                UserId = shop.UserId,
                ShopName = shop.ShopName,
                ShopType = shop.ShopType,
                Location = shop.Location,
                IsBanned = shop.IsBanned,
                Avatar = shop.Avatar,
                Rating = shop.Rating,
                CreateAt = Timestamp.FromDateTime(shop.CreateAt!.Value.ToUniversalTime()),
                UpdateAt = Timestamp.FromDateTime(shop.UpdateAt!.Value.ToUniversalTime())
            };

            return grpcShop;
        }

        public override async Task<Shop.Shop> GetOfUser(Id request, ServerCallContext context)
        {
            var shop = await _repo.GetOfUser(request.SearchId);
            if (shop == null)
                return new Shop.Shop();
            Shop.Shop grpcShop = new Shop.Shop
            {
                Id = shop.Id,
                UserId = shop.UserId,
                ShopName = shop.ShopName,
                ShopType = shop.ShopType,
                Location = shop.Location,
                IsBanned = shop.IsBanned,
                Avatar = shop.Avatar,
                Rating = shop.Rating,
                CreateAt = Timestamp.FromDateTime(shop.CreateAt!.Value.ToUniversalTime()),
                UpdateAt = Timestamp.FromDateTime(shop.UpdateAt!.Value.ToUniversalTime())
            };

            return grpcShop;
        }

        public override async Task<Response> Create(CreateShop request, ServerCallContext context)
        {
            var createShop = new RequestCreateShop
            {
                UserId = request.UserId,
                ShopName = request.ShopName,
                ShopType = request.ShopType,
                Location = request.Location,
                Avatar = request.Avatar,
                IsBanned = request.IsBanned,
            };

            var response = await _repo.CreateShop(createShop);
            return new Response { Message = response.Message, StatusCode = response.StatusCode };
        }

        public override async Task<Response> Update(Shop.Shop request, ServerCallContext context)
        {
            var updateShop = new RequestUpdateShop
            {
                Id = request.Id,
                UserId = request.UserId,
                ShopName = request.ShopName,
                ShopType = request.ShopType,
                Location = request.Location,
                IsBanned = request.IsBanned,
                Avatar = request.Avatar,
                CreateAt = request.CreateAt.ToDateTime(),
            };

            var response = await _repo.UpdateShop(updateShop);
            return new Response { Message = response.Message, StatusCode = response.StatusCode };
        }

        public override async Task<Response> Delete(Id request, ServerCallContext context)
        {
            var response = await _repo.DeleteShop(request.SearchId);
            return new Response { Message = response.Message, StatusCode = response.StatusCode };
        }
    }
}
