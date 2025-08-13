using StiktifyShop.Application.DTOs.Requests;
using StiktifyShop.Application.DTOs.Responses;
using StiktifyShop.Application.Interfaces;
using StiktifyShop.Application.Mapper;
using StiktifyShop.Domain.Entity;

namespace StiktifyShop.Infrastructure.Repository
{
    public class UserAddressRepo : IUserAddressRepo
    {
        private AppDbContext _context;
        public UserAddressRepo(AppDbContext context)
        {
            _context = context ?? throw new ArgumentException(nameof(context));
        }
        public async Task<Response> Create(CreateUserAddress userAddress)
        {
            try
            {
                var newAddress = MapperSingleton<MapperUserAddress>.Instance.MapCreate(userAddress);
                _context.UserAddresses.Add(newAddress);
                await _context.SaveChangesAsync();
                return new Response
                {
                    StatusCode = 201,
                    Message = "User address created successfully",
                    Data = new { Id = newAddress.Id }
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
                var existing = await _context.UserAddresses.FindAsync(id);
                if (existing == null)
                {
                    return new Response
                    {
                        StatusCode = 404,
                        Message = "User address not found"
                    };
                }
                _context.UserAddresses.Remove(existing);
                await _context.SaveChangesAsync();
                return new Response
                {
                    StatusCode = 204,
                    Message = "User address deleted successfully"
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

        public IQueryable<ResponseUserAddress> GetAll()
        {
            try
            {
                var list = _context.UserAddresses
                    .Select(ua => MapperSingleton<MapperUserAddress>.Instance.MapResponse(ua))
                    .ToList();
                return list.AsQueryable();
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

        public async Task<Response> Update(UpdateUserAddress userAddress)
        {
            try
            {
                var existing = await _context.UserAddresses.FindAsync(userAddress.Id);
                if (existing == null)
                {
                    return new Response
                    {
                        StatusCode = 404,
                        Message = "User address not found"
                    };
                }
                existing.Address = userAddress.Address;
                existing.Receiver = userAddress.Receiver;
                existing.PhoneReceive = userAddress.PhoneReceive;
                existing.Note = userAddress.Note;
                existing.UpdatedAt = DateTime.Now;

                _context.UserAddresses.Update(existing);
                await _context.SaveChangesAsync();
                return new Response
                {
                    StatusCode = 200,
                    Message = "User address updated successfully",
                    Data = new { value = MapperSingleton<MapperUserAddress>.Instance.MapResponse(existing) }
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
