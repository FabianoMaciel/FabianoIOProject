﻿namespace FabianoIO.ManagementPayments.Application.Query
{
    public interface IPaymentQuery
    {
        Task<bool> PaymentExists(Guid studentId, Guid courseId);
    }
}
