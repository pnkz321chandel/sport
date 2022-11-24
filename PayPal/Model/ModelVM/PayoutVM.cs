using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.PayPal.Model.ModelVM
{
    public class PayoutVM
    {
        public string Token { get; set; }
        public double Amount { get; set; }
        public string ReciverPayPalId { get; set; }
    }
}
