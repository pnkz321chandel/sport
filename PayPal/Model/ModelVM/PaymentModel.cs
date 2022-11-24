using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.PayPal.Model
{
    public class PaymentModel
    {
        public long Amount { get; set; }
        public string Currency { get; set; }
        //public string Description { get; set; }
        //public string PaymentTokken { get; set; }
    }
}
