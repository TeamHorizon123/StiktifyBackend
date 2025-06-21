using Domain.Requests;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcServicePurchase.Interfaces;
using GrpcServicePurchase.PaymentMethod;

namespace GrpcServicePurchase.Services
{
    public class PaymentMethodGrpcService : PaymentMethodGrpc.PaymentMethodGrpcBase
    {
        private IPaymentMethodRepository _repo;

        public PaymentMethodGrpcService(IPaymentMethodRepository repo)
        {
            _repo = repo ?? throw new ArgumentException(nameof(_repo));
        }

        public override async Task<PaymentMethods> GetAll(PaymentMethod.Empty request, ServerCallContext context)
        {
            var listMethod = await _repo.GetAll();
            PaymentMethods methods = new PaymentMethods();
            methods.Item.AddRange(listMethod.Select(method => new PaymentMethod.PaymentMethod
            {
                Id = method.Id,
                Name = method.Name,
                Enable = method.Enable,
                CreateAt = Timestamp.FromDateTime(method.CreateAt!.Value.ToUniversalTime()),
                UpdateAt = Timestamp.FromDateTime(method.UpdateAt!.Value.ToUniversalTime())
            }));
            return methods;
        }

        public override async Task<PaymentMethod.PaymentMethod> GetOne(Id request, ServerCallContext context)
        {
            var method = await _repo.GetOne(request.SearchId);
            if (method == null)
                return new PaymentMethod.PaymentMethod();
            return new PaymentMethod.PaymentMethod
            {
                Id = method.Id,
                Name = method.Name,
                Enable = method.Enable,
                CreateAt = Timestamp.FromDateTime(method.CreateAt!.Value.ToUniversalTime()),
                UpdateAt = Timestamp.FromDateTime(method.UpdateAt!.Value.ToUniversalTime())
            };
        }

        public override async Task<Response> Create(CreateMethod request, ServerCallContext context)
        {
            var method = new RequestCreateMethod
            {
                Enable = request.Enable,
                Name = request.Name,
            };
            var response = await _repo.Create(method);
            return new Response { Message = response.Message, StatusCode = response.StatusCode };
        }

        public override async Task<Response> Update(PaymentMethod.PaymentMethod request, ServerCallContext context)
        {
            var method = new RequestUpdateMethod
            {
                Id = request.Id,
                Enable = request.Enable,
                Name = request.Name,
            };
            var response = await _repo.Update(method);
            return new Response { Message = response.Message, StatusCode = response.StatusCode };
        }
    }
}
