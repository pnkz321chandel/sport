using Core.PayPal.Model;
using Core.PayPal.Model.DemoProject.Stripe.Model.Paypal;
using Core.PayPal.Model.ModelVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.PayPal.Interface
{
    public interface IPaymentService
    {
        Task<string> GetPaymentToken(CancellationToken cancellationToken);
        Task<string> CreateOrder(PaymentModel paymentModel ,CancellationToken cancellationToken);
        Task<RootCapture> CaptureOrder(CaptureVM captureModel, CancellationToken cancellationToken);

        string CreatePayout(PayoutVM payoutModel, CancellationToken cancellationToken);
        Task<ConfirmPayoutModel> ConfirmPayout(string token , string payoutId);
    }
}
