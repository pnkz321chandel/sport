using Core.PayPal.Interface;
using Core.PayPal.Model;
using Core.PayPal.Model.DemoProject.Stripe.Model.Paypal;
using Core.PayPal.Model.ModelVM;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.PayPal.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly string clientId;
        private readonly string secretKey;

        public PaymentService(IConfiguration Config)
        {
            clientId = Config.GetValue<string>("PayPal:ClientId");
            secretKey = Config.GetValue<string>("PayPal:SecretKey");
        }
        public async Task<string> GetPaymentToken(CancellationToken cancellationToken)
        {
            try
            {
                //var clientId = "Af6naXGv9sF1JP5Q5OgvairDE8ntoPWSchuBKqr5-mBQk6HaKqvKNiWb5lOePbhO0krz_App9Zkh8mMs";
                //var SecretKey = "EHNOMAnji7DzVzsQ-rx--oBpP1FMyhrfM6pSG8Cy5I8UTuNXslEZ8ZDCB2_IZM4NxhH4wVBN4L4U4OX5";

                var au = clientId + ":" + secretKey;
                var plainTextBytes2 = System.Text.Encoding.UTF8.GetBytes(au);
                var auth = System.Convert.ToBase64String(plainTextBytes2);

                var client = new RestClient("https://api-m.sandbox.paypal.com/v1/oauth2/token");
                var request = new RestRequest();
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                request.AddHeader("Authorization", "Basic " + auth);
                request.AddParameter("grant_type", "client_credentials");
                RestResponse response = await client.PostAsync(request, CancellationToken.None);
                var AuthToken = string.Empty;
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    AuthModel responseModel = JsonConvert.DeserializeObject<AuthModel>(response.Content);
                    AuthToken = responseModel.AccessToken;
                }

                return AuthToken;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        public async Task<string> CreateOrder(PaymentModel paymentModel, CancellationToken cancellationToken)
        {
            try
            {
                var Currency = paymentModel.Currency ?? "USD";
                double Amount = paymentModel.Amount;
                var token = await GetPaymentToken(cancellationToken);
                var client = new RestClient("https://api-m.sandbox.paypal.com/v2/checkout/orders");
                var requestId = Guid.NewGuid();

                var request = new RestRequest();
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Authorization", "Bearer " + token);
                request.AddHeader("PayPal-Request-Id", requestId.ToString());
                var body = @"{" + "\n" +
                @"  ""intent"": ""CAPTURE""," + "\n" +
                @"  ""purchase_units"": [" + "\n" +
                @"    {" + "\n" +
                @"      ""amount"": {" + "\n" +
                @"        ""currency_code"": " + '"' + Currency + '"' + "," + "\n" +
                @"        ""value"": " + Amount + "" + "\n" +
                @"      }" + "\n" +
                @"    }" + "\n" +
                @"  ]," + "\n" +
                @"  ""payment_source"": {" + "\n" +
                @"    ""paypal"": {" + "\n" +
                @"      ""experience_context"": {" + "\n" +
                @"        ""return_url"": ""https://example.com/returnUrl""," + "\n" +
                @"        ""cancel_url"": ""https://example.com/cancelUrl""" + "\n" +
                @"      }" + "\n" +
                @"    }" + "\n" +
                @"  }" + "\n" +
                @"}";
                request.AddParameter("application/json", body, ParameterType.RequestBody);
                RestResponse response = await client.PostAsync(request, CancellationToken.None);
                OrderModel responseModel = JsonConvert.DeserializeObject<OrderModel>(response.Content);
                var order = responseModel.Links[1].Href;

                return order;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        //""return_url"": ""https://toptierlessons.com/transaction""," + "\n" +
        //        @"        ""cancel_url"": ""https://toptierlessons.com/transaction""" + "\n" +
        public async Task<RootCapture> CaptureOrder(CaptureVM captureModel, CancellationToken cancellationToken)
        {
        
            RootCapture Model = new RootCapture();
            try
            {
                var client = new RestClient("https://api-m.sandbox.paypal.com/v2/checkout/orders/" + captureModel.Token + "/capture");
                var authtoken = await GetPaymentToken(cancellationToken);
                var request = new RestRequest();
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Authorization", "Bearer " + authtoken);
                request.AddHeader("PayPal-Request-Id", Guid.NewGuid().ToString());
                var body = @"";
                request.AddParameter("application/json", body, ParameterType.RequestBody);
                RestResponse response = await client.PostAsync(request, CancellationToken.None);

                RootCapture responseModel = JsonConvert.DeserializeObject<RootCapture>(response.Content);
                Model.id = responseModel.id;
                Model.status = responseModel.status;
              
                //Model.PaymentSource.Paypal.Name = responseModel.PaymentSource.Paypal.Name;

                return responseModel;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public string CreatePayout(PayoutVM payoutModel, CancellationToken cancellationToken)
        {
            try
            {
                //var payoutToken = await GetPaymentToken(cancellationToken);

                var client = new RestClient("https://api-m.sandbox.paypal.com/v1/payments/payouts");    
                //var Amount =payoutModel.Amount;
                //var ReciverId =payoutModel.ReciverPayPalId;
                string batchId = "Payouts_"+DateTime.UtcNow;

                var request = new RestRequest();
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Authorization", "Bearer " + payoutModel.Token);
                var body = @"{" + "\n" +
                         @"  ""sender_batch_header"": {" + "\n" +
                         @"    ""sender_batch_id"": " + '"' + batchId + '"' + "," + "\n" +
                         @"    ""email_subject"": ""You have a payout!""," + "\n" +
                         @"    ""email_message"": ""You have received a payout! Thanks for using our service!""" + "\n" +
                         @"  }," + "\n" +
                         @"  ""items"": [" + "\n" +
                         @"    {" + "\n" +
                         @"      ""recipient_type"": ""PAYPAL_ID""," + "\n" +
                         @"      ""amount"": {" + "\n" +
                         @"        ""value"": "+ payoutModel.Amount + "," + "\n" +
                         @"        ""currency"": ""USD""" + "\n" +
                         @"      }," + "\n" +
                         @"      ""note"": ""Thanks for your support!""," + "\n" +
                         @"      ""sender_item_id"": ""5Q5KHT6UMDK8N""," + "\n" +
                         @"      ""receiver"": " + '"' + payoutModel.ReciverPayPalId + '"' + "\n" +
                         @"    }" + "\n" +
                         @"  ]" + "\n" +
                         @"}";
                request.AddParameter("application/json", body, ParameterType.RequestBody);
                //RestResponse response = await client.PostAsync(request, CancellationToken.None);

                RestResponse response = client.ExecutePost(request);

                PayoutModel responseModel = JsonConvert.DeserializeObject<PayoutModel>(response.Content);

                //PayoutResponse res = await ConfirmPayout(payoutToken, responseModel.batch_header.payout_batch_id);

                return responseModel.batch_header.payout_batch_id;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task<ConfirmPayoutModel> ConfirmPayout(string payoutToken,string payoutId)
        {

            try
            {
                PayoutResponse confirmPayoutModel = new PayoutResponse();
                //var payoutToken = await GetPaymentToken(cancellationToken);
                ConfirmPayoutModel responseModel = new ConfirmPayoutModel();
                using (var client = new RestClient("https://api-m.sandbox.paypal.com/v1/payments/payouts/" + payoutId))
                {
                    
                    var request = new RestRequest();
                    request.AddHeader("Content-Type", "application/json");
                    request.AddHeader("Authorization", "Bearer " + payoutToken);

                    //RestResponse response = await client.GetAsync(request, CancellationToken.None);
                    RestResponse response = await client.ExecuteGetAsync(request);
                    responseModel = JsonConvert.DeserializeObject<ConfirmPayoutModel>(response.Content);
                }

                //List<payoutItem> itemlist = new List<payoutItem>();
                //foreach (var item in responseModel.items)
                //{
                //    payoutItem itemData = new payoutItem();
                //    itemData.transaction_id = item.transaction_id;
                //    itemData.transaction_status = item.transaction_status;

                //    itemlist.Add(itemData);                    
                //}
                //confirmPayoutModel.PayoutBatchId = responseModel.batch_header.payout_batch_id;
                //confirmPayoutModel.BatchStatus = responseModel.batch_header.batch_status;
                //confirmPayoutModel.itemList = itemlist;

                return responseModel;
            }
            catch (Exception ex)
            {
                throw;
            }
            
        }
     
    }
}
