using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.PayPal.Model
{
  

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class BatchHeader
    {
        public string payout_batch_id { get; set; }
        public string batch_status { get; set; }
        public SenderBatchHeader sender_batch_header { get; set; }
    }

    public class LinkVM
    {
        public string Href { get; set; }
        public string Rel { get; set; }
        public string Method { get; set; }
        public string EncType { get; set; }
    }

    public class PayoutModel
    {
        public BatchHeader batch_header { get; set; }
        public List<LinkVM> links { get; set; }
    }

    public class SenderBatchHeader
    {
        public string SenderBatchId { get; set; }
        public string EmailSubject { get; set; }
        public string EmailMessage { get; set; }
    }

}
