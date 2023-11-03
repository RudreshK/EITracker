namespace EITracker.DbContext.Entities
{
    public class EmployeeAttendance : EditableEntity
    {
        public Guid EmployeeId { get; set; }
        public Guid AttendanceId { get; set; }
        public DateOnly Date { get; set;}
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set;}
        public string Status { get; set; }

    }
}
