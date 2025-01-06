namespace EITracker.DbContext.Entities
{
    public class LeaveRequest : EditableEntity
    {
        public LeaveRequest()
        {
            this.LeaveRequestDetails = new HashSet<LeaveRequestDetail>();
        }
        public Guid LeaveId { get; set; }
        public Guid EmployeeId { get; set; }
        public decimal NoOfDays { get; set; }
        public byte LeaveType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public byte Status { get; set; }
        public string? Subject { get; set; }
        public string? Message { get; set; }
        public string? Attachments { get; set; }
        public virtual ICollection<LeaveRequestDetail> LeaveRequestDetails { get; set; }
    }
}
