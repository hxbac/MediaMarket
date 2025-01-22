using MediaMarket.Application.Bases;

namespace MediaMarket.Application.Contracts.Common
{
    public interface ITaskConsumer
    {
        Task HandleAsync(EventBase @event);
    }
}
