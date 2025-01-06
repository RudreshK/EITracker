using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace EITracker.Models
{
	public static class CommonMethods
	{
		public static string GetDisplayName(Enums.LeaveType leaveType)
		{
			// Use reflection to get the Display attribute value
			var fieldInfo = leaveType.GetType().GetField(leaveType.ToString());
			var displayAttribute = fieldInfo?.GetCustomAttribute<DisplayAttribute>();

			return displayAttribute?.Name ?? leaveType.ToString();
		}
	}
}
