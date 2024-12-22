
namespace EITracker.Models
{
    public class HolidayListModel
    {
        public Guid HolidayId { get; set; }
        public string HolidayName { get; set; }
        public Enums.WeekDays WeekDay { get; set; }
        public DateTime HolidayDate { get; set; }
    }
}
