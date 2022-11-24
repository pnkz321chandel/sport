using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.PayPal.Model.ModelVM
{
    public class CaptureResponseModel
    {
        public string id { get; set; }
        public string status { get; set; }
    }
}
