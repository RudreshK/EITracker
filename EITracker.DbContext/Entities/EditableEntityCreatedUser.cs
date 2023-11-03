
namespace EITracker.DbContext.Entities
{
    public abstract class EditableEntityCreatedUser
    {
        public Guid CreatedById { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
