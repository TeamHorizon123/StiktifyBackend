using StiktifyShop.Application.DTOs.Requests;
using StiktifyShop.Application.DTOs.Responses;
using StiktifyShop.Domain.Entity;

namespace StiktifyShop.Application.Mapper
{
    public class MapperOrderTracking
    {
        public OrderTracking MapCreate(CreateOrderTracking orderTracking)
        {
            return new OrderTracking
            {
                CourierInfo = orderTracking.CourierInfo,
                Location = orderTracking.Location,
                Message = orderTracking.Message,
                OrderId = orderTracking.OrderId!,
                Status = orderTracking.Status,
                TimeTracking = orderTracking.TimeTracking,
            };
        }
        public ResponseOrderTracking MapResponse(OrderTracking orderTracking)
        {
            return new ResponseOrderTracking
            {
                Id = orderTracking.Id,
                CourierInfo = orderTracking.CourierInfo,
                Location = orderTracking.Location,
                Message = orderTracking.Message,
                OrderId = orderTracking.OrderId,
                Status = orderTracking.Status,
                TimeTracking = orderTracking.TimeTracking,
                CreateAt = orderTracking.CreatedAt,
                UpdateAt = orderTracking.UpdatedAt
            };
        }
    }
}
