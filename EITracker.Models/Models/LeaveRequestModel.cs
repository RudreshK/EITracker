
namespace EITracker.Models
{
    public class LeaveRequestModel
    {
        public Guid LeaveId { get; set; }
        public Guid EmployeeId { get; set; }
        public decimal NoOfDays { get; set; }
        public Enums.LeaveType LeaveType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Enums.LeaveStatus Status { get; set; }
        public string? Subject { get; set; }
        public string? Message { get; set; }
        public string? Attachments { get; set; }
    }
}
