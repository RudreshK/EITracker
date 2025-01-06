

namespace EITracker.DbContext.Entities
{
    public class LeaveRequestDetail : EditableEntity
    {
        public Guid LeaveId { get; set; }
        public Guid LeaveRequestDetailId { get; set; }
        public Guid AssignedTo { get; set; }
        public string CC { get; set; }
        public int DisplayOrder { get; set; }
        public string? Comments { get; set; }
        public byte Status { get; set; }
        public virtual LeaveRequest LeaveRequest { get; internal set; }
    }
}
