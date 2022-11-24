using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.PayPal.Model
{
    namespace DemoProject.Stripe.Model.Paypal
    {
        public class Capture
        {
            public string Id { get; set; }
            public string Status { get; set; }
            public Amount Amount { get; set; }
            public bool FinalCapture { get; set; }
            public string disbursement_mode { get; set; }
            public SellerProtection SellerProtection { get; set; }
            public SellerReceivableBreakdown seller_receivable_breakdown { get; set; }
            public List<LinkModel> links { get; set; }
            public DateTime create_time { get; set; }
            public DateTime update_time { get; set; }
        }
        public class Address
        {
            public string country_code { get; set; }
            public string address_line_1 { get; set; }
            public string address_line_2 { get; set; }
            public string admin_area_2 { get; set; }
            public string admin_area_1 { get; set; }
            public string postal_code { get; set; }
        }

        public class Amount
        {
            public string CurrencyCode { get; set; }
            public string Value { get; set; }
        }


        public class GrossAmount
        {
            public string currency_code { get; set; }
            public string value { get; set; }
        }

        public class LinkModel
        {
            public string Href { get; set; }
            public string Rel { get; set; }
            public string Method { get; set; }
        }

        public class Name
        {
            public string GivenName { get; set; }
            public string Surname { get; set; }
            public string Fullame { get; set; }
        }

        public class NetAmount
        {
            public string currency_code { get; set; }
            public string value { get; set; }
        }

        public class Payer
        {
            public Name name { get; set; }
            public string email_address { get; set; }
            public string payer_id { get; set; }
            public Address address { get; set; }
        }

        public class Payments
        {
            public List<Capture> captures { get; set; }
        }

        public class PaymentSource
        {
            public Paypal Paypal { get; set; }
        }

        public class Paypal
        {
            public string EmailAddress { get; set; }
            public string AccountId { get; set; }
            public Name Name { get; set; }
            public Address Address { get; set; }
        }

        public class PaypalFee
        {
            public string currency_code { get; set; }
            public string value { get; set; }
        }

        public class PurchaseUnit
        {
            public string reference_id { get; set; }
            public Shipping shipping { get; set; }
            public Payments payments { get; set; }
        }

        public class RootCapture
        {
            public string id { get; set; }
            public string status { get; set; }
            public PaymentSource PaymentSource { get; set; }
            public List<PurchaseUnit> purchase_units { get; set; }
            public Payer payer { get; set; }
            public List<Link> links { get; set; }
        }

        public class SellerProtection
        {
            public string status { get; set; }
            public List<string> dispute_categories { get; set; }
        }

        public class SellerReceivableBreakdown
        {
            public GrossAmount gross_amount { get; set; }
            public PaypalFee paypal_fee { get; set; }
            public NetAmount net_amount { get; set; }
        }

        public class Shipping
        {
            public Name name { get; set; }
            public Address address { get; set; }
        }


    }

}
