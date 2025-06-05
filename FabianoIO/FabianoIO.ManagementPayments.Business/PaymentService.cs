using FabianoIO.Core.DomainObjects.DTOs;
using FabianoIO.Core.Messages.Notifications;
using MediatR;

namespace FabianoIO.ManagementPayments.Business;

public class PaymentService(IPaymentCreditCardFacade paymentCreditCardFacade,
                              IPaymentRepository paymentRepository,
                              IMediator mediator) : IPaymentService
{
    public async Task<bool> MakePaymentCourse(PaymentCourse paymentCourse)
    {
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

        var transaction = paymentCreditCardFacade.MakePayment(payment);

        if (transaction.StatusTransaction == StatusTransaction.Accept)
        {
            //TO DO Fabiano eu preciso gerar esse evento?
            // payment.AddEvent(new PaymentCourseMadeEvent(payment.CourseId, payment.StudentId));

            paymentRepository.Add(payment);
            paymentRepository.AddTransaction(transaction);

            await paymentRepository.UnitOfWork.Commit();
            return true;
        }

        await mediator.Publish(new DomainNotification("Payment", "The transaction was declined"));
        return false;
    }
}