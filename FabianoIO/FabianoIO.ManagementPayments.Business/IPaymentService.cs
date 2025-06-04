using FabianoIO.Core.DomainObjects.DTOs;

namespace FabianoIO.ManagementPayments.Business;

public interface IPaymentService
{
    Task<bool> MakePaymentCourse(PaymentCourse paymentCourse);
}