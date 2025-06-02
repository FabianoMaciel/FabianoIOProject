using MediatR;
using System.ComponentModel.DataAnnotations.Schema;

namespace FabianoIO.Core.Messages
{
    [NotMapped]
    public abstract class Event : Message, INotification
    {
        public DateTime Timestamp { get; private set; }

        protected Event()
        {
            Timestamp = DateTime.Now;
        }
    }
}
