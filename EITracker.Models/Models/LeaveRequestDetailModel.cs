
using System.ComponentModel.DataAnnotations;

namespace EITracker.Models
{
    public class LeaveRequestDetailModel
    {
        public Guid LeaveId { get; set; }
        public Guid LeaveRequestDetailId { get; set; }
        public Guid AssignedTo { get; set; }
        public string CC { get; set; }
        public int DisplayOrder { get; set; }
        public string? Comments { get; set; }
        public Enums.LeaveStatus Status { get; set; }
     
    }
}
