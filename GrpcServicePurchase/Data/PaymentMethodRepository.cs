using Domain.Requests;
using Domain.Responses;
using Grpc.Core;
using GrpcServicePurchase.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GrpcServicePurchase.Data
{
    public class PaymentMethodRepository : IPaymentMethodRepository
    {
        private AppDbContext _context;
        private ILogger _logger;

        public PaymentMethodRepository(AppDbContext context, ILogger<PaymentMethodRepository> logger)
        {
            _context = context ?? throw new ArgumentException(nameof(_context));
            _logger = logger ?? throw new ArgumentException(nameof(_logger));
        }

        public async Task<Response> Create(RequestCreateMethod createMethod)
        {
            try
            {
                var method = new Domain.Entities.PaymentMethod
                {
                    CreateAt = DateTime.UtcNow,
                    Enable = createMethod.Enable,
                    Name = createMethod.Name,
                };
                _context.PaymentMethods.Add(method);
                await _context.SaveChangesAsync();
                return new Response { Message = $"_id: {method.Id}", StatusCode = 201 };
            }
            catch (Exception err)
            {
                _logger.LogError($"Fail to create a payment method \nError: {err.Message}");
                throw new RpcException(new Status(StatusCode.Internal, "Internal Error"));
            }
        }

        public async Task<IEnumerable<ResponsePaymentMethod>> GetAll()
        {
            try
            {
                return await _context.PaymentMethods
                    .Select(method => new ResponsePaymentMethod
                    {
                        Id = method.Id,
                        Name = method.Name,
                        Enable = method.Enable,
                        CreateAt = method.CreateAt,
                        UpdateAt = method.UpdateAt
                    })
                    .ToListAsync();
            }
            catch (Exception err)
            {
                _logger.LogError($"Fail to get all payment method \nError: {err.Message}");
                throw new RpcException(new Status(StatusCode.Internal, "Internal Error"));
            }
        }

        public async Task<ResponsePaymentMethod?> GetOne(string methodId)
        {
            try
            {
                return await _context.PaymentMethods
                    .Where(method => method.Id == methodId)
                    .Select(method => new ResponsePaymentMethod
                    {
                        Id = method.Id,
                        Name = method.Name,
                        Enable = method.Enable,
                        CreateAt = method.CreateAt,
                        UpdateAt = method.UpdateAt
                    })
                    .FirstOrDefaultAsync();
            }
            catch (Exception err)
            {
                _logger.LogError($"Fail to get a payment method - {methodId} \nError: {err.Message}");
                throw new RpcException(new Status(StatusCode.Internal, "Internal Error"));
            }
        }

        public async Task<Response> Update(RequestUpdateMethod updateMethod)
        {
            try
            {
                var exist = await _context.PaymentMethods.FindAsync(updateMethod.Id);
                if (exist == null)
                    throw new Exception("Payment method does not exist");

                exist.Enable = updateMethod.Enable;
                exist.Name = updateMethod.Name;
                exist.UpdateAt = DateTime.Now;
                _context.PaymentMethods.Update(exist);
                await _context.SaveChangesAsync();
                return new Response { Message = $"_id: {exist.Id}", StatusCode = 200 };
            }
            catch (Exception err)
            {
                _logger.LogError($"Fail to update a payment method \nError: {err.Message}");
                throw new RpcException(new Status(StatusCode.Internal, "Internal Error"));
            }
        }
    }
}
