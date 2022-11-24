using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.PayPal.Model.ModelVM
{
    public class PayoutResponse
    {      
        public string PayoutBatchId { get; set; }
        public string BatchStatus { get; set; }
        public List<payoutItem> itemList { get; set; }
    
    }
    public class payoutItem
    {
        public string payout_item_id { get; set; }
        public string transaction_id { get; set; }
        public string activity_id { get; set; }
        public string transaction_status { get; set; }        
        public string payout_batch_id { get; set; }
       
    }

  

}
