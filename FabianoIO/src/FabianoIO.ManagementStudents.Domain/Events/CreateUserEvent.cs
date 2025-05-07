using FabianoIO.Core.DomainObjects;

namespace FabianoIO.ManagementStudents.Domain.Events
{
    public class CreateUserEvent : DomainEvent
    {
        public CreateUserEvent(Guid aggregateId) : base(aggregateId)
        {

        }
    }
}
