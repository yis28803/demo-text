namespace Duanbaimot_lan2.Data
{
    public class Instructor
    {
        public int InstructorID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        // Other properties...
        public ICollection<TeachingSchedule> TeachingSchedules { get; set; }
    }
}
