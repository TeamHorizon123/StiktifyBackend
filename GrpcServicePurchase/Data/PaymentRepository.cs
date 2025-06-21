using Domain.Requests;
using Domain.Responses;
using Grpc.Core;
using GrpcServicePurchase.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GrpcServicePurchase.Data
{
    public class PaymentRepository : IPaymentRepository
    {
        private AppDbContext _context;
        private ILogger _logger;

        public PaymentRepository(AppDbContext context, ILogger logger)
        {
            _context = context ?? throw new ArgumentException(nameof(_context));
            _logger = logger ?? throw new ArgumentException(nameof(_logger));
        }

        public async Task<Response> Create(RequestCreatePayment createPayment)
        {
            try
            {
                var existPayMethod = await _context.PaymentMethods
                    .AnyAsync(method => method.Id == createPayment.MethodId && method.Enable == true);
                if (!existPayMethod)
                    throw new Exception("Payment method does not exist or enable.");
                var payment = new Domain.Entities.Payment
                {
                    UserId = createPayment.UserId,
                    MethodId = createPayment.MethodId,
                    OrderId = createPayment.OrderId,
                    Amount = createPayment.Amount,
                    PaidAt = createPayment.PaidAt,
                    Status = createPayment.Status,
                    TxnRef = createPayment.TxnRef,
                    CreateAt = DateTime.Now,
                };
                _context.Payments.Add(payment);
                await _context.SaveChangesAsync();
                return new Response { Message = $"_id: {payment.Id}", StatusCode = 201 };
            }
            catch (Exception err)
            {
                _logger.LogError($"Fail to add a payment \nError: {err.Message}");
                throw new RpcException(new Status(StatusCode.Internal, "Internal Error"));
            }
        }

        public async Task<IEnumerable<ResponsePayment>> GetAll()
        {
            try
            {
                return await _context.Payments
                    .Select(
                    payment => new ResponsePayment
                    {
                        Id = payment.Id,
                        OrderId = payment.OrderId,
                        MethodId = payment.MethodId,
                        Amount = payment.Amount,
                        PaidAt = payment.PaidAt,
                        Status = payment.Status,
                        TxnRef = payment.TxnRef,
                        UserId = payment.UserId,
                        CreateAt = payment.CreateAt,
                        UpdateAt = payment.UpdateAt,
                    })
                    .ToListAsync();
            }
            catch (Exception err)
            {
                _logger.LogError($"Fail to get all payment \nError: {err.Message}");
                throw new RpcException(new Status(StatusCode.Internal, "Internal Error"));
            }
        }

        public Task<IEnumerable<ResponsePayment>> GetAllOfProduct(string productId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ResponsePayment>> GetAllOfUser(string userId)
        {
            try
            {
                return await _context.Payments
                    .Where(payment => payment.UserId == userId)
                    .Select(
                    payment => new ResponsePayment
                    {
                        Id = payment.Id,
                        OrderId = payment.OrderId,
                        MethodId = payment.MethodId,
                        Amount = payment.Amount,
                        PaidAt = payment.PaidAt,
                        Status = payment.Status,
                        TxnRef = payment.TxnRef,
                        UserId = payment.UserId,
                        CreateAt = payment.CreateAt,
                        UpdateAt = payment.UpdateAt,
                    })
                    .ToListAsync();
            }
            catch (Exception err)
            {
                _logger.LogError($"Fail to get all payment of user - {userId} \nError: {err.Message}");
                throw new RpcException(new Status(StatusCode.Internal, "Internal Error"));
            }
        }

        public async Task<ResponsePayment?> GetOne(string paymentId)
        {
            try
            {
                return await _context.Payments
                    .Where(payment => payment.Id == paymentId)
                    .Select(
                    payment => new ResponsePayment
                    {
                        Id = payment.Id,
                        OrderId = payment.OrderId,
                        MethodId = payment.MethodId,
                        Amount = payment.Amount,
                        PaidAt = payment.PaidAt,
                        Status = payment.Status,
                        TxnRef = payment.TxnRef,
                        UserId = payment.UserId,
                        CreateAt = payment.CreateAt,
                        UpdateAt = payment.UpdateAt,
                    })
                    .FirstOrDefaultAsync();
            }
            catch (Exception err)
            {
                _logger.LogError($"Fail to get a payment - {paymentId} \nError: {err.Message}");
                throw new RpcException(new Status(StatusCode.Internal, "Internal Error"));
            }
        }

        public async Task<Response> Update(RequestUpdatePayment updatePayment)
        {
            try
            {
                var existPayment = await _context.Payments.AnyAsync(payment => payment.Id == updatePayment.Id);
                if (!existPayment)
                    throw new Exception("Payment does not exist.");
                var existPayMethod = await _context.PaymentMethods
                    .AnyAsync(method => method.Id == updatePayment.MethodId && method.Enable == true);
                if (!existPayMethod)
                    throw new Exception("Payment method does not exist or enable.");
                var payment = new Domain.Entities.Payment
                {
                    Id = updatePayment.Id,
                    Amount = updatePayment.Amount,
                    MethodId = updatePayment.MethodId,
                    OrderId = updatePayment.OrderId,
                    PaidAt = updatePayment.PaidAt,
                    Status = updatePayment.Status,
                    UserId = updatePayment.UserId,
                    TxnRef = updatePayment.TxnRef
                };
                _context.Payments.Update(payment);
                await _context.SaveChangesAsync();
                return new Response { Message = $"_id: {payment.Id}", StatusCode = 200 };
            }
            catch (Exception err)
            {
                _logger.LogError($"Fail to get all payment \nError: {err.Message}");
                throw new RpcException(new Status(StatusCode.Internal, "Internal Error"));
            }
        }
    }
}
