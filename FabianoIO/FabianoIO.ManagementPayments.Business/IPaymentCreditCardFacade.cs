﻿namespace FabianoIO.ManagementPayments.Business;

public interface IPaymentCreditCardFacade
{
    BusinessTransaction MakePayment(Payment payment);
}