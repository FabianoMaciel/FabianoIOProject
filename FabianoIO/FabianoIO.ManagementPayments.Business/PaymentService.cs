using MediatR;
using FabianoIO.Core.Messages.IntegrationEvents;
using FabianoIO.Core.Messages.Notifications;
using FabianoIO.Core.DomainObjects.DTOs;

namespace FabianoIO.ManagementPayments.Business;

public class PaymentService(IPaymentCreditCardFacade paymentCreditCardFacade,
                              IPaymentRepository paymentRepository,
                              IMediator mediator) : IPaymentService
{
    public async Task<bool> MakePaymentCourse(PaymentCourse paymentCourse)
    {
        var order = new Order
        {
            CourseId = paymentCourse.CourseId,
            StudentId = paymentCourse.StudentId,
            Total = paymentCourse.Total,
        };

        var payment = new Payment
        {
            Value = paymentCourse.Total,
            CardName = paymentCourse.CardName,
            CardNumber = paymentCourse.CardNumber,
            CardExpirationDate = paymentCourse.CardExpirationDate,
            CardCVV = paymentCourse.CardCVV,
            StudentId = paymentCourse.StudentId,
            CourseId = paymentCourse.CourseId
        };

        var transaction = paymentCreditCardFacade.MakePayment(order, payment);

        if (transaction.StatusTransaction == StatusTransaction.Accept)
        {
            payment.AddEvent(new PaymentCourseMadeEvent(payment.CourseId, payment.StudentId));

            paymentRepository.Add(payment);
            paymentRepository.AddTransaction(transaction);

            await paymentRepository.UnitOfWork.Commit();
            return true;
        }

        await mediator.Publish(new DomainNotification("Payment", "The transaction was declined"));
        return false;
    }
}