using Microsoft.EntityFrameworkCore;
using StiktifyShop.Application.DTOs.Requests;
using StiktifyShop.Application.DTOs.Responses;
using StiktifyShop.Application.Interfaces;
using StiktifyShop.Application.Mapper;

namespace StiktifyShop.Infrastructure.Repository
{
    public class PaymentRepo : IPaymentRepo
    {
        private AppDbContext context;
        public PaymentRepo(AppDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Response> Create(CreatePayment createPayment)
        {
            try
            {
                var newPayment = MapperSingleton<MapperPayment>.Instance.MapCreate(createPayment);
                context.Payments.Add(newPayment);
                await context.SaveChangesAsync();
                return new Response
                {
                    StatusCode = 201,
                    Message = "Payment created successfully.",
                    Data = new { Id = newPayment.Id }
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

        public IQueryable<ResponsePayment> GetAll()
        {
            try
            {
                var list = context.Payments.ToList();
                return list
                    .Select(p => MapperSingleton<MapperPayment>.Instance.MapResponse(p))
                    .AsQueryable();
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

        public async Task<Response> Update(UpdatePayment updatePayment)
        {
            try
            {
                var existing = await context.Payments
                    .FirstOrDefaultAsync(p => p.Id == updatePayment.Id);
                if (existing == null)
                {
                    return new Response
                    {
                        StatusCode = 404,
                        Message = "Payment not found."
                    };
                }
                existing.Amount = updatePayment.Amount;
                existing.Status = updatePayment.Status;
                existing.PaidAt = updatePayment.PaidAt;
                existing.TxnRef = updatePayment.TxnRef;
                existing.UpdatedAt = DateTime.Now;

                context.Payments.Update(existing);
                await context.SaveChangesAsync();
                return new Response
                {
                    StatusCode = 200,
                    Message = "Payment updated successfully.",
                    Data = new { value = MapperSingleton<MapperPayment>.Instance.MapResponse(existing) }
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
