using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC.Utility.VNPay
{
    public class VnPaymentRequestModel
    {
        public string Name { get; set; }
        public int OrderId { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
