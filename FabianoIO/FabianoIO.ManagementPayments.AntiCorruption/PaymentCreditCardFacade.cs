using Microsoft.Extensions.Options;
using FabianoIO.ManagementPayments.Business;

namespace FabianoIO.ManagementPayments.AntiCorruption;

public class PaymentCreditCardFacade(IPayPalGateway payPalGateway,
    IOptions<PaymentSettings> options) : IPaymentCreditCardFacade
{
    private readonly PaymentSettings _settings = options.Value;
    public BusinessTransaction MakePayment(Order order, Payment payment)
    {
        var apiKey = _settings.ApiKey;
        var encriptionKey = _settings.EncriptionKey;

        var serviceKey = payPalGateway.GetPayPalServiceKey(apiKey, encriptionKey);
        var cardHashKey = payPalGateway.GetCardHashKey(serviceKey, payment.CardNumber);

        var transaction = payPalGateway.CommitTransaction(cardHashKey, order.CourseId.ToString(), payment.Value);

        transaction.PaymentId = payment.Id;

        return transaction;
    }
}