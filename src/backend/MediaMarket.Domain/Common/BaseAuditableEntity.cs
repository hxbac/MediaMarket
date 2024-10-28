namespace MediaMarket.Domain.Common
{
    public abstract class BaseAuditableEntity<T> : BaseEntity<T>
    {
        public DateTimeOffset CreatedOn { get; set; } = DateTimeOffset.Now;
        public string? CreatedBy { get; set; }
        public DateTimeOffset? LastModified { get; set; }
        public string? LastModifiedBy { get; set; }
    }
}
