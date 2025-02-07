using MediaMarket.Application.Bases;
using MediaMarket.Application.Contracts.Common;
using MediaMarket.Infrastructure.Services;

namespace MediaMarket.Infrastructure.Event
{
    public class EventManager(
        IEventPublisher eventPublisher,
        GenericHandler genericHandler
    )
    {
        private readonly Dictionary<Type, List<Type>> _listeners = new();
        private readonly IEventPublisher _eventPublisher = eventPublisher;
        private readonly GenericHandler _genericHandler = genericHandler;

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

        public async Task TriggerAsync<TEvent>(TEvent? @event) where TEvent : EventBase
        {
            var eventType = typeof(TEvent);

            if (_listeners.ContainsKey(eventType))
            {
                foreach (var listener in _listeners[eventType])
                {
                    if (Array.Exists(listener.GetInterfaces(), i => i == typeof(IShouldQueue)))
                    {
                        await _eventPublisher.PublishAsync(@event, "default");
                    }
                    else
                    {
                        var method = _genericHandler.GetType().GetMethod("InvokeAsync")!.MakeGenericMethod(listener);
                        await (method.Invoke(_genericHandler, [@event]) as Task);
                    }
                }
            }
        }
    }
}
