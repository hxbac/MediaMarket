namespace MediaMarket.Application.Contracts.Common
{
    public interface IEventPublisher
    {
        Task Initialize();
        Task PublishAsync<T>(T @event, string queueKey);
    }
}
