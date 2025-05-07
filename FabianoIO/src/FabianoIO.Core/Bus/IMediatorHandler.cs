using FabianoIO.Core.Messages;

namespace FabianoIO.Core.Bus
{
    public interface IMediatorHandler
    {
        Task PublicEvent<T>(T ev) where T : Event;
    }
}
