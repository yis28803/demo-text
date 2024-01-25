namespace Duanbaimot_lan2.Data
{
    public class TeachingSchedule
    {
        public int ScheduleID { get; set; }
        public int InstructorID { get; set; }
        public int ClassID { get; set; }
        public string DayOfWeek { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        // Other properties...
        public Instructor Instructor { get; set; }
        public Class Class { get; set; }
    }
}
