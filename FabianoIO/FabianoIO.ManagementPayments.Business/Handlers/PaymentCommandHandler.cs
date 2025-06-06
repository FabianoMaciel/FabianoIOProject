using MediatR;
using FabianoIO.Core.Messages.IntegrationCommands;

namespace FabianoIO.ManagementPayments.Business.Handlers;

public class PaymentCommandHandler(IPaymentService paymentService) : IRequestHandler<MakePaymentCourseCommand, bool>                                                                                                
{
    public async Task<bool> Handle(MakePaymentCourseCommand request, CancellationToken cancellationToken)
    {
        return await paymentService.MakePaymentCourse(request.PaymentCourse);
    }
}