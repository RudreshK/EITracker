
using System.ComponentModel.DataAnnotations;

namespace EITracker.Enums
{
    public enum LeaveType
    {
        None = 0,
        [Display(Name = "Casual Leave")]
        CasualLeave = 1,
        [Display(Name = "Privilage Leave")]
        PrivilageLeave = 2,
        [Display(Name = "Sick Leave")]
        SickLeave = 3,
		[Display(Name = "Marriage Leave")]
		MarriageLeave = 4,
	}
}
