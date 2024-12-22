

namespace EITracker.DbContext.Entities
{
    public class HolidayList : EditableEntity
    {
        public Guid HolidayId { get; set; }  
        public string HolidayName { get; set; }
        public byte WeekDay { get; set; }
        public DateTime HolidayDate { get; set; }
    }
}
