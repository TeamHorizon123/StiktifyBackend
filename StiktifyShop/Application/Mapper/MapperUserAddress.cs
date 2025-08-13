using StiktifyShop.Application.DTOs.Requests;
using StiktifyShop.Application.DTOs.Responses;
using StiktifyShop.Domain.Entity;

namespace StiktifyShop.Application.Mapper
{
    public class MapperUserAddress
    {
        public UserAddress MapCreate(CreateUserAddress createAddress)
        {
            return new UserAddress
            {
                UserId = createAddress.UserId,
                Address = createAddress.Address,
                Note = createAddress.Note,
                PhoneReceive = createAddress.PhoneReceive,
                Receiver = createAddress.Receiver,
            };
        }

        public ResponseUserAddress MapResponse(UserAddress userAddress)
        {
            return new ResponseUserAddress
            {
                Id = userAddress.Id,
                UserId = userAddress.UserId,
                Address = userAddress.Address,
                Note = userAddress.Note,
                PhoneReceive = userAddress.PhoneReceive,
                Receiver = userAddress.Receiver,
                CreateAt = userAddress.CreatedAt,
                UpdateAt = userAddress.UpdatedAt
            };
        }
    }
}
