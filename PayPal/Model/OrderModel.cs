using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.PayPal.Model
{
    public class OrderModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("status")]
        public string status { get; set; }

        //[JsonProperty("payment_source")]
        //public PaymentSource payment_source { get; set; }

        [JsonProperty("links")]
        public List<Link> Links { get; set; }
    }

    public class Link
    {
        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("rel")]
        public string Rel { get; set; }

        [JsonProperty("method")]
        public string Method { get; set; }
    }
}
