using FabianoIO.Core.DomainObjects;

namespace FabianoIO.ManagementPayments.Business;

public class BusinessTransaction : Entity
{
    public Guid RegistrationId { get; set; }
    public Guid PaymentId { get; set; }
    public double Total { get; set; }
    public StatusTransaction StatusTransaction { get; set; }
    public Payment Payment { get; set; }
}