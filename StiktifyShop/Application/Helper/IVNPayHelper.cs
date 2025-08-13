using StiktifyShop.Application.DTOs.Requests;
using StiktifyShop.Application.DTOs.Responses;

namespace StiktifyShop.Application.Helper
{
    public interface IVNPayHelper
    {
        string CreatePaymentUrl(VNPayRequest model, HttpContext context);
        VNPayResponse PaymentExecute(IQueryCollection collections);

    }
}
