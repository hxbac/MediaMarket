using MediaMarket.Application.Bases;
using MediaMarket.Application.Contracts.Common;

namespace MediaMarket.Infrastructure.Event
{
    public class EventManager
    {
        private readonly Dictionary<Type, List<Type>> _listeners = new();
        private readonly IEventPublisher _eventPublisher;

        public EventManager(IEventPublisher eventPublisher)
        {
            _eventPublisher = eventPublisher;
        }

        public void AddListener<TEvent>(Type listener) where TEvent : EventBase
        {
            var eventType = typeof(TEvent);

            if (!_listeners.ContainsKey(eventType))
            {
                _listeners[eventType] = new List<Type>();
            }

            _listeners[eventType].Add(listener);
        }

        public void RemoveListener<TEvent>(Type listener) where TEvent : EventBase
        {
            var eventType = typeof(TEvent);

            if (_listeners.ContainsKey(eventType))
            {
                _listeners[eventType].Remove(listener);
            }
        }

        public async Task TriggerAsync<TEvent>(TEvent @event) where TEvent : EventBase
        {
            var eventType = typeof(TEvent);

            if (_listeners.ContainsKey(eventType))
            {
                foreach (var listener in _listeners[eventType])
                {
                    //await listener.HandleAsync(@event);
                }
            }
        }
    }
}
