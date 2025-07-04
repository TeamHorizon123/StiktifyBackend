using Domain.Requests;
using Domain.Responses;
using StiktifyShopBackend.Interfaces;
using StiktifyShopBackend.ReceiveAddress;
using System.Net;

namespace StiktifyShopBackend.Providers
{
    public class AddressProvider : IAddressProvider
    {
        private AddressGrpc.AddressGrpcClient _client;

        public AddressProvider(AddressGrpc.AddressGrpcClient client)
        {
            _client = client ?? throw new ArgumentException(nameof(_client));
        }

        public async Task<Domain.Responses.Response> CreateAddress(RequestCreateAddress address)
        {
            var createGrpc = new CreateReceiveAddress
            {
                UserId = address.UserId,
                Address = address.Address,
                PhoneReceive = address.PhoneReceive,
                Note = address.Note,
            };
            var response = await _client.CreateAsync(createGrpc);
            return new Domain.Responses.Response { Message = response.Message, StatusCode = response.StatusCode };
        }

        public async Task<Domain.Responses.Response> DeleteAddress(string addressId)
        {
            var response = await _client.DeleteAsync(new Id { SearchId = addressId });
            return new Domain.Responses.Response { Message = response.Message, StatusCode = response.StatusCode };
        }

        public IQueryable<ResponseReceiveAddress> GetAllOfUser(string userId)
        {
            var listGrpc = _client.GetAllOfUser(new Id { SearchId = userId });
            var listAddresses = listGrpc.Item.Select(address
                => new ResponseReceiveAddress
                {
                    Id = address.Id,
                    UserId = userId,
                    Note = address.Note,
                    Address = address.Address,
                    Receiver = address.Receiver,
                    PhoneReceive = address.PhoneReceive,
                    CreateAt = address.CreateAt.ToDateTime(),
                    UpdateAt = address.UpdateAt.ToDateTime(),
                });
            return listAddresses.AsQueryable();
        }

        public async Task<ResponseReceiveAddress?> GetOne(string addressId)
        {
            var addressGrpc = await _client.GetOneAsync(new Id { SearchId = addressId });
            return new ResponseReceiveAddress
            {
                Id = addressGrpc.Id,
                UserId = addressGrpc.UserId,
                Note = addressGrpc.Note,
                Address = addressGrpc.Address,
                Receiver = addressGrpc.Receiver,
                PhoneReceive = addressGrpc.PhoneReceive,
                CreateAt = addressGrpc.CreateAt.ToDateTime(),
                UpdateAt = addressGrpc.UpdateAt.ToDateTime(),
            };
        }

        public async Task<Domain.Responses.Response> UpdateAddress(RequestUpdateAddress address)
        {
            var updateGprc = new ReceiveAddress.ReceiveAddress
            {
                Id = address.Id,
                UserId = address.UserId,
                Note = address.Note,
                Address = address.Address,
                Receiver = address.Receiver,
                PhoneReceive = address.PhoneReceive,
            };
            var response = await _client.UpdateAsync(updateGprc);
            return new Domain.Responses.Response { Message = response.Message, StatusCode = response.StatusCode };
        }
    }
}
