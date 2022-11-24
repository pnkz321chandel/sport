using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.PayPal.Model
{
    public class ConfirmPayoutModel
    {
        public BatchHeaderModel batch_header { get; set; }
        public List<Item> items { get; set; }
        public List<Link> links { get; set; }
    }
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Amount
    {
        public string currency { get; set; }
        public string value { get; set; }
    }

    public class BatchHeaderModel
    {
        public string payout_batch_id { get; set; }
        public string batch_status { get; set; }
        public DateTime time_created { get; set; }
        public DateTime time_completed { get; set; }
        public DateTime time_closed { get; set; }
        public SenderBatchHeader sender_batch_header { get; set; }
        public string funding_source { get; set; }
        public Amount amount { get; set; }
        public Fees fees { get; set; }
    }

    public class Fees
    {
        public string currency { get; set; }
        public string value { get; set; }
    }

    public class Item
    {
        public string payout_item_id { get; set; }
        public string transaction_id { get; set; }
        public string activity_id { get; set; }
        public string transaction_status { get; set; }
        public PayoutItemFee payout_item_fee { get; set; }
        public string payout_batch_id { get; set; }
        public PayoutItem payout_item { get; set; }
        public DateTime time_processed { get; set; }
        public List<LinkViewModel> links { get; set; }
    }

    public class LinkViewModel
    {
        public string href { get; set; }
        public string rel { get; set; }
        public string method { get; set; }
        public string encType { get; set; }
    }

    public class PayoutItem
    {
        public string recipient_type { get; set; }
        public Amount amount { get; set; }
        public string note { get; set; }
        public string receiver { get; set; }
        public string sender_item_id { get; set; }
        public string recipient_wallet { get; set; }
    }

    public class PayoutItemFee
    {
        public string currency { get; set; }
        public string value { get; set; }
    }

   

    public class SenderBatchHeaderModel
    {
        public string sender_batch_id { get; set; }
        public string email_subject { get; set; }
        public string email_message { get; set; }
    }


}
