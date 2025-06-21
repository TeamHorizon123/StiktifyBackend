using Domain.Requests;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcServicePurchase.Interfaces;
using GrpcServicePurchase.Payment;

namespace GrpcServicePurchase.Services
{
    public class PaymentGrpcService : PaymentGrpc.PaymentGrpcBase
    {
        private IPaymentRepository _repo;

        public PaymentGrpcService(IPaymentRepository repo)
        {
            _repo = repo ?? throw new ArgumentException(nameof(_repo));
        }

        public override async Task<Payments> GetAll(Payment.Empty request, ServerCallContext context)
        {
            var listPayment = await _repo.GetAll();
            Payments payments = new Payments();
            payments.Item.AddRange(listPayment.Select(payment
                => new Payment.Payment
                {
                    Id = payment.Id,
                    MethodId = payment.MethodId,
                    OrderId = payment.OrderId,
                    UserId = payment.UserId,
                    Amount = payment.Amount,
                    Status = payment.Status,
                    TxnRef = payment.TxnRef,
                    PaidAt = Timestamp.FromDateTime(payment.PaidAt!.Value.ToUniversalTime()),
                    CreateAt = Timestamp.FromDateTime(payment.CreateAt!.Value.ToUniversalTime()),
                    UpdateAt = Timestamp.FromDateTime(payment.UpdateAt!.Value.ToUniversalTime()),
                }));
            return payments;
        }

        public override async Task<Payments> GetAllOfUser(Id request, ServerCallContext context)
        {
            var listPayment = await _repo.GetAllOfUser(request.SearchId);
            Payments payments = new Payments();
            payments.Item.AddRange(listPayment.Select(payment
                => new Payment.Payment
                {
                    Id = payment.Id,
                    MethodId = payment.MethodId,
                    OrderId = payment.OrderId,
                    UserId = payment.UserId,
                    Amount = payment.Amount,
                    Status = payment.Status,
                    TxnRef = payment.TxnRef,
                    PaidAt = Timestamp.FromDateTime(payment.PaidAt!.Value.ToUniversalTime()),
                    CreateAt = Timestamp.FromDateTime(payment.CreateAt!.Value.ToUniversalTime()),
                    UpdateAt = Timestamp.FromDateTime(payment.UpdateAt!.Value.ToUniversalTime()),
                }));
            return payments;
        }

        public override async Task<Payment.Payment> GetOne(Id request, ServerCallContext context)
        {
            var payment = await _repo.GetOne(request.SearchId);
            if (payment == null)
                return new Payment.Payment();
            return new Payment.Payment
            {
                Id = payment.Id,
                MethodId = payment.MethodId,
                OrderId = payment.OrderId,
                UserId = payment.UserId,
                Amount = payment.Amount,
                Status = payment.Status,
                TxnRef = payment.TxnRef,
                PaidAt = Timestamp.FromDateTime(payment.PaidAt!.Value.ToUniversalTime()),
                CreateAt = Timestamp.FromDateTime(payment.CreateAt!.Value.ToUniversalTime()),
                UpdateAt = Timestamp.FromDateTime(payment.UpdateAt!.Value.ToUniversalTime()),
            };
        }

        public override async Task<Response> Create(CreatePayment request, ServerCallContext context)
        {
            var createPayment = new RequestCreatePayment
            {
                UserId = request.UserId,
                OrderId = request.OrderId,
                MethodId = request.MethodId,
                Amount = request.Amount,
                Status = request.Status,
                PaidAt = request.PaidAt.ToDateTime(),
                TxnRef = request.TxnRef,
            };
            var response = await _repo.Create(createPayment);
            return new Response { Message = response.Message, StatusCode = response.StatusCode };
        }

        public override async Task<Response> Update(Payment.Payment request, ServerCallContext context)
        {
            var updatePayment = new RequestUpdatePayment
            {
                Id = request.Id,
                UserId = request.UserId,
                OrderId = request.OrderId,
                MethodId = request.MethodId,
                Amount = request.Amount,
                Status = request.Status,
                PaidAt = request.PaidAt.ToDateTime(),
                TxnRef = request.TxnRef,
            };
            var response = await _repo.Update(updatePayment);
            return new Response { Message = response.Message, StatusCode = response.StatusCode };
        }
    }
}
