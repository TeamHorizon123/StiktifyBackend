using Domain.Requests;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcServiceUser.Interface;
using GrpcServiceUser.ReceiveAddress;
using System.Net;

namespace GrpcServiceUser.Services
{
    public class AddressGrpcService : AddressGrpc.AddressGrpcBase
    {
        private IAddressRepository _repo;

        public AddressGrpcService(IAddressRepository repo)
        {
            _repo = repo ?? throw new ArgumentException(nameof(_repo));
        }

        public override async Task<ReceiveAddresses> GetAllOfUser(Id request, ServerCallContext context)
        {
            var listAddress = await _repo.GetAllOfUser(request.SearchId);
            ReceiveAddresses receiveAddresses = new ReceiveAddresses();
            receiveAddresses.Item.AddRange(listAddress.Select(address
                => new ReceiveAddress.ReceiveAddress
                {
                    Id = address.Id,
                    UserId = address.UserId,
                    Address = address.Address,
                    Note = address.Note,
                    PhoneReceive = address.PhoneReceive,
                    Receiver = address.Receiver,
                    CreateAt = Timestamp.FromDateTime(address.CreateAt!.Value.ToUniversalTime()),
                    UpdateAt = Timestamp.FromDateTime(address.UpdateAt!.Value.ToUniversalTime()),
                }));
            return receiveAddresses;
        }

        public override async Task<ReceiveAddress.ReceiveAddress> GetOne(Id request, ServerCallContext context)
        {
            var address = await _repo.GetOne(request.SearchId);
            ReceiveAddress.ReceiveAddress receiveAddress = new ReceiveAddress.ReceiveAddress
            {
                Id = address!.Id,
                UserId = address.UserId,
                Address = address.Address,
                Note = address.Note,
                PhoneReceive = address.PhoneReceive,
                Receiver = address.Receiver,
                CreateAt = Timestamp.FromDateTime(address.CreateAt!.Value.ToUniversalTime()),
                UpdateAt = Timestamp.FromDateTime(address.UpdateAt!.Value.ToUniversalTime()),
            };
            return receiveAddress;
        }

        public override async Task<Response> Create(CreateReceiveAddress request, ServerCallContext context)
        {
            var createAddress = new RequestCreateAddress
            {
                UserId = request.UserId,
                Address = request.Address,
                Note = request.Note,
                PhoneReceive = request.PhoneReceive,
                Receiver = request.Receiver,
            };
            var response = await _repo.CreateAddress(createAddress);
            return new Response { Message = response.Message, StatusCode = response.StatusCode };
        }

        public override async Task<Response> Update(ReceiveAddress.ReceiveAddress request, ServerCallContext context)
        {
            var updateAddress = new RequestUpdateAddress
            {
                Id = request.Id,
                UserId = request.UserId,
                Address = request.Address,
                Note = request.Note,
                PhoneReceive = request.PhoneReceive,
                Receiver = request.Receiver,
                CreateAt = request.CreateAt!.ToDateTime()
            };
            var response = await _repo.UpdateAddress(updateAddress);
            return new Response { Message = response.Message, StatusCode = response.StatusCode };
        }

        public override async Task<Response> Delete(Id request, ServerCallContext context)
        {
            var response = await _repo.DeleteAddress(request.SearchId);
            return new Response { Message = response.Message, StatusCode = response.StatusCode };
        }
    }
}
