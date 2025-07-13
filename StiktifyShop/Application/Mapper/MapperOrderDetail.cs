using StiktifyShop.Application.DTOs.Requests;
using StiktifyShop.Application.DTOs.Responses;
using StiktifyShop.Domain.Entity;

namespace StiktifyShop.Application.Mapper
{
    public class MapperOrderDetail
    {
        public OrderDetail MapCreate(CreateOrderDetail orderDetail)
        {
            return new OrderDetail
            {
                Id = orderDetail.Id,
                DateOfDelivery = orderDetail.DateOfDelivery,
                DateOfPurchase = orderDetail.DateOfPurchase,
                DateOfShipping = orderDetail.DateOfShipping,
                PurchaseMethod = orderDetail.PurchaseMethod,
            };
        }

        public ResponseOrderDetail MapResponse(OrderDetail orderDetail)
        {
            return new ResponseOrderDetail
            {
                Id = orderDetail.Id,
                DateOfDelivery = orderDetail.DateOfDelivery,
                DateOfPurchase = orderDetail.DateOfPurchase,
                DateOfShipping = orderDetail.DateOfShipping,
                PurchaseMethod = orderDetail.PurchaseMethod,
            };
        }
    }
}
