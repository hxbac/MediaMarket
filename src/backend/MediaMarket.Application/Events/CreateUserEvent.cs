using MediaMarket.Application.Bases;

namespace MediaMarket.Application.Events
{
    public class CreateUserEvent : EventBase
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
    }
}
