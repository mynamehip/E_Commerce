using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC.Utility.VNPay
{
    public interface IVNPayService
    {
        public string CreatePayment(HttpContext context, VnPaymentRequestModel model);
        public VnPaymentResponseModel PaymentExecute(IQueryCollection collections);
    }
}
