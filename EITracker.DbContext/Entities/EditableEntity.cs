
namespace EITracker.DbContext.Entities
{
    public abstract class EditableEntity : EditableEntityCreatedUser
    {
        public Guid ModifiedById { get; set; }
        public DateTime ModifiedTime { get; set; }
        public byte[] ConcurrencyStamp { get; set; }
    }
}
