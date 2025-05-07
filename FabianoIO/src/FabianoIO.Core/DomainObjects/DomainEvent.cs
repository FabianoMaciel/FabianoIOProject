using FabianoIO.Core.Messages;

namespace FabianoIO.Core.DomainObjects
{
    public class DomainEvent : Event
    {
        public DomainEvent(Guid aggregateId) 
        {
            AggregatedId = aggregateId;
        }
    }
}
