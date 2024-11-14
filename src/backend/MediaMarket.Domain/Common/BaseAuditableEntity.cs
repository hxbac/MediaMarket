namespace MediaMarket.Domain.Common
{
    public abstract class BaseAuditableEntity<T> : BaseEntity<T>
    {
        public DateTimeOffset CreatedOn { get; set; } = DateTimeOffset.Now;
        public Guid? CreatedBy { get; set; }
        public DateTimeOffset? LastModified { get; set; }
        public Guid? LastModifiedBy { get; set; }
    }
}
