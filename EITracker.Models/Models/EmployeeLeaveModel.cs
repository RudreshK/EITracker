

namespace EITracker.Models
{
    public class EmployeeLeaveModel
    {
        public Guid EmployeeId { get; set; }
        public Guid LeaveId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string LeaveType { get; set; }
        public string Status { get; set; }
        public string Comments { get; set; }
    }
}
