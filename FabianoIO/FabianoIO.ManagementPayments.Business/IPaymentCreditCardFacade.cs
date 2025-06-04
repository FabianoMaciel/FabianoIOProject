namespace FabianoIO.ManagementPayments.Business;

public interface IPaymentCreditCardFacade
{
    BusinessTransaction MakePayment(Order pedido, Payment pagamento);
}