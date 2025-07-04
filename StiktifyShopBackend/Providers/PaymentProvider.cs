using Domain.Requests;
using Domain.Responses;
using Google.Protobuf.WellKnownTypes;
using StiktifyShopBackend.Interfaces;
using StiktifyShopBackend.Payment;

namespace StiktifyShopBackend.Providers
{
    public class PaymentProvider : IPaymentProvider
    {
        private PaymentGrpc.PaymentGrpcClient _client;

        public PaymentProvider(PaymentGrpc.PaymentGrpcClient client)
        {
            _client = client ?? throw new ArgumentException(nameof(_client));
        }

        public async Task<Domain.Responses.Response> Create(RequestCreatePayment createPayment)
        {
            var createGrpc = new CreatePayment
            {
                MethodId = createPayment.MethodId,
                OrderId = createPayment.OrderId,
                UserId = createPayment.UserId,
                Amount = createPayment.Amount,
                Status = createPayment.Status,
                TxnRef = createPayment.TxnRef,
                PaidAt = Timestamp.FromDateTime(createPayment.PaidAt.ToUniversalTime()),
            };
            var response = await _client.CreateAsync(createGrpc);
            return new Domain.Responses.Response { Message = response.Message, StatusCode = response.StatusCode };
        }

        public IQueryable<ResponsePayment> GetAll()
        {
            var grpcList = _client.GetAll(new Payment.Empty());
            var list = grpcList.Item.Select(item => new ResponsePayment
            {
                Id = item.Id,
                Amount = item.Amount,
                MethodId = item.MethodId,
                OrderId = item.OrderId,
                PaidAt = item.PaidAt.ToDateTime(),
                Status = item.Status,
                TxnRef = item.TxnRef,
                UserId = item.UserId,
                CreateAt = item.CreateAt.ToDateTime(),
                UpdateAt = item.UpdateAt.ToDateTime(),
            });
            return list.AsQueryable();
        }

        public IQueryable<ResponsePayment> GetAllOfProduct(string productId)
        {
            var grpcList = _client.GetAllOfProduct(new Id { SearchId = productId });
            var list = grpcList.Item.Select(item => new ResponsePayment
            {
                Id = item.Id,
                Amount = item.Amount,
                MethodId = item.MethodId,
                OrderId = item.OrderId,
                PaidAt = item.PaidAt.ToDateTime(),
                Status = item.Status,
                TxnRef = item.TxnRef,
                UserId = item.UserId,
                CreateAt = item.CreateAt.ToDateTime(),
                UpdateAt = item.UpdateAt.ToDateTime(),
            });
            return list.AsQueryable();
        }

        public IQueryable<ResponsePayment> GetAllOfUser(string userId)
        {
            var grpcList = _client.GetAllOfUser(new Id { SearchId = userId });
            var list = grpcList.Item.Select(item => new ResponsePayment
            {
                Id = item.Id,
                Amount = item.Amount,
                MethodId = item.MethodId,
                OrderId = item.OrderId,
                PaidAt = item.PaidAt.ToDateTime(),
                Status = item.Status,
                TxnRef = item.TxnRef,
                UserId = item.UserId,
                CreateAt = item.CreateAt.ToDateTime(),
                UpdateAt = item.UpdateAt.ToDateTime(),
            });
            return list.AsQueryable();
        }

        public async Task<ResponsePayment?> GetOne(string paymentId)
        {
            var paymentGrpc = await _client.GetOneAsync(new Id { SearchId = paymentId });
            return new ResponsePayment
            {
                Id = paymentGrpc.Id,
                Amount = paymentGrpc.Amount,
                MethodId = paymentGrpc.MethodId,
                OrderId = paymentGrpc.OrderId,
                PaidAt = paymentGrpc.PaidAt.ToDateTime(),
                Status = paymentGrpc.Status,
                TxnRef = paymentGrpc.TxnRef,
                UserId = paymentGrpc.UserId,
                CreateAt = paymentGrpc.CreateAt.ToDateTime(),
                UpdateAt = paymentGrpc.UpdateAt.ToDateTime(),
            };
        }

        public async Task<Domain.Responses.Response> Update(RequestUpdatePayment updatePayment)
        {
            var updateGrpc = new Payment.Payment
            {
                Id = updatePayment.Id,
                MethodId = updatePayment.MethodId,
                OrderId = updatePayment.OrderId,
                UserId = updatePayment.UserId,
                Amount = updatePayment.Amount,
                Status = updatePayment.Status,
                TxnRef = updatePayment.TxnRef,
                PaidAt = Timestamp.FromDateTime(updatePayment.PaidAt.ToUniversalTime()),
            };
            var response = await _client.UpdateAsync(updateGrpc);
            return new Domain.Responses.Response { Message = response.Message, StatusCode = response.StatusCode };
        }
    }
}
