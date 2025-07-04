using Domain.Requests;
using Domain.Responses;
using GrpcServiceUser.Interface;
using Microsoft.EntityFrameworkCore;

namespace GrpcServiceUser.Data
{
    public class AddressRepository : IAddressRepository
    {
        private AppDbContext _context;

        public AddressRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentException(nameof(_context));
        }
        public async Task<Response> CreateAddress(RequestCreateAddress address)
        {
            try
            {
                var addressData = new Domain.Entities.ReceiveAddress
                {
                    UserId = address.UserId,
                    Address = address.Address,
                    PhoneReceive = address.PhoneReceive,
                    Receiver = address.Receiver,
                    Note = address.Note,
                    CreateAt = DateTime.Now
                };
                _context.ReceiveAddresses.Add(addressData);
                await _context.SaveChangesAsync();
                return new Response { Message = addressData.Id, StatusCode = 201 };
            }
            catch (Exception err)
            {
                return new Response { Message = err.Message, StatusCode = 500 };
            }
        }

        public async Task<Response> DeleteAddress(string addressId)
        {
            try
            {
                var address = await _context.ReceiveAddresses.FindAsync(addressId);
                if (address == null)
                    return new Response { Message = "Address does not exist.", StatusCode = 404 };
                _context.ReceiveAddresses.Remove(address);
                await _context.SaveChangesAsync();
                return new Response { StatusCode = 204 };
            }
            catch (Exception err)
            {
                return new Response { Message = err.Message, StatusCode = 500 };
            }
        }

        public async Task<IEnumerable<ResponseReceiveAddress>> GetAllOfUser(string userId)
        {
            try
            {
                return await _context.ReceiveAddresses
                    .Where(x => x.UserId == userId)
                    .Select(a => new ResponseReceiveAddress
                    {
                        Id = a.Id,
                        UserId = a.UserId,
                        Address = a.Address,
                        PhoneReceive = a.PhoneReceive,
                        Receiver = a.Receiver,
                        Note = a.Note,
                        CreateAt = a.CreateAt,
                        UpdateAt = a.UpdateAt
                    })
                    .ToListAsync();
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

        public async Task<ResponseReceiveAddress?> GetOne(string addressId)
        {
            try
            {
                return await _context.ReceiveAddresses
                    .Where(a => a.Id == addressId)
                    .Select(a => new ResponseReceiveAddress
                    {
                        Id = a.Id,
                        UserId = a.UserId,
                        Address = a.Address,
                        PhoneReceive = a.PhoneReceive,
                        Receiver = a.Receiver,
                        Note = a.Note,
                        CreateAt = a.CreateAt,
                        UpdateAt = a.UpdateAt
                    })
                    .FirstOrDefaultAsync();
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

        public async Task<Response> UpdateAddress(RequestUpdateAddress address)
        {
            try
            {
                var addressUpdate = await _context.ReceiveAddresses.FindAsync(address.Id);
                if (addressUpdate == null)
                    return new Response { Message = "Address does not exist.", StatusCode = 404 };
                addressUpdate.Address = address.Address;
                addressUpdate.PhoneReceive = address.PhoneReceive;
                addressUpdate.Receiver = address.Receiver;
                addressUpdate.Note = address.Note;
                addressUpdate.UpdateAt = DateTime.Now;

                _context.ReceiveAddresses.Update(addressUpdate);
                await _context.SaveChangesAsync();
                return new Response { Message = address.Id, StatusCode = 200 };
            }
            catch (Exception err)
            {
                return new Response { Message = err.Message, StatusCode = 500 };
            }
        }
    }
}
