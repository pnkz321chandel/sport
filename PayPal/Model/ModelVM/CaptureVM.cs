using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.PayPal.Model.ModelVM
{
    public class CaptureVM
    {
        public string Token { get; set; }
        public string PayerId { get; set; }
    }
}
