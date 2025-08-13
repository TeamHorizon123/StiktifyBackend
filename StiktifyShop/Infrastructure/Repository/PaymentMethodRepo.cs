using Microsoft.EntityFrameworkCore;
using StiktifyShop.Application.DTOs.Requests;
using StiktifyShop.Application.DTOs.Responses;
using StiktifyShop.Application.Interfaces;
using StiktifyShop.Application.Mapper;

namespace StiktifyShop.Infrastructure.Repository
{
    public class PaymentMethodRepo : IPaymentMethodRepo
    {
        private AppDbContext _context;
        public PaymentMethodRepo(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Response> Create(CreatePaymentMethod paymentMethod)
        {
            try
            {
                var existingMethod = await _context.PaymentMethods
                    .FirstOrDefaultAsync(method => method.Name == paymentMethod.Name);
                if (existingMethod != null)
                    return new Response
                    {
                        StatusCode = 400,
                        Message = "Payment method already exists."
                    };
                var newMethod = MapperSingleton<MapperPaymentMethod>.Instance.MapCreate(paymentMethod);
                _context.PaymentMethods.Add(newMethod);
                await _context.SaveChangesAsync();
                return new Response
                {
                    StatusCode = 201,
                    Message = "Payment method created successfully.",
                    Data = new { Id = newMethod.Id }
                };
            }
            catch (Exception err)
            {
                return new Response
                {
                    StatusCode = 500,
                    Message = err.Message
                };
            }
        }

        public async Task<Response> Delete(string id)
        {
            try
            {
                var existingMethod = await _context.PaymentMethods
                    .FirstOrDefaultAsync(method => method.Id == id);
                if (existingMethod == null)
                    return new Response
                    {
                        StatusCode = 404,
                        Message = "Payment method not found."
                    };
                _context.PaymentMethods.Remove(existingMethod);
                await _context.SaveChangesAsync();
                return new Response
                {
                    StatusCode = 204,
                    Message = "Payment method deleted successfully."
                };
            }
            catch (Exception err)
            {
                return new Response
                {
                    StatusCode = 500,
                    Message = err.Message
                };
            }
        }

        public async Task<ResponsePaymentMethod?> Get(string id)
        {
            try
            {
                return await _context.PaymentMethods
                    .Where(method => method.Id == id)
                    .Select(method => MapperSingleton<MapperPaymentMethod>.Instance.MapResponse(method))
                    .FirstOrDefaultAsync();
            }
            catch (Exception err)
            {
                return null;
                throw new Exception(err.Message);
            }
        }

        public IQueryable<ResponsePaymentMethod> GetAll()
        {
            try
            {
                var list = _context.PaymentMethods.Select(method
                    => MapperSingleton<MapperPaymentMethod>.Instance.MapResponse(method)).AsQueryable();
                return list;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

        public async Task<Response> Update(UpdatePaymentMethod paymentMethod)
        {
            try
            {
                var existingMethod = await _context.PaymentMethods
                    .FirstOrDefaultAsync(method => method.Id == paymentMethod.Id);
                if (existingMethod == null)
                {
                    return new Response
                    {
                        StatusCode = 404,
                        Message = "Payment method not found."
                    };
                }
                existingMethod.Name = paymentMethod.Name;
                existingMethod.Enable = paymentMethod.Enable;
                existingMethod.UpdatedAt = DateTime.Now;
                _context.PaymentMethods.Update(existingMethod);
                await _context.SaveChangesAsync();
                return new Response
                {
                    StatusCode = 200,
                    Message = "Payment method updated successfully.",
                    Data = new
                    {
                        value = MapperSingleton<MapperPaymentMethod>.Instance.MapResponse(existingMethod)
                    }   
                };
            }
            catch (Exception err)
            {
                return new Response
                {
                    StatusCode = 500,
                    Message = err.Message
                };
            }
        }
    }
}
