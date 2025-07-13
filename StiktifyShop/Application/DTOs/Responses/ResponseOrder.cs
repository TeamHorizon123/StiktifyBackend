using StiktifyShop.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace StiktifyShop.Application.DTOs.Responses
{
    public class ResponseOrder : BaseResponse
    {
        public string? UserId { get; set; }
        public string? ShopId { get; set; }
        public ResponseShop? Shop { get; set; }
        public string? ProductId { get; set; }
        public ResponseProduct? Product { get; set; }
        public string? ProductItemId { get; set; }
        public ResponseProductItem? ProductItem { get; set; }
        public string? AddressId { get; set; }
        public ResponseUserAddress? Address { get; set; }
        public string? Status { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double ShippingFee { get; set; }
        public IEnumerable<ResponseOrderTracking>? OrderTrackings { get; set; }
    }
}
