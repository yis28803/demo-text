namespace Duanbaimot_lan2.Data
{
    public class Class
    {
        public int ClassID { get; set; }
        public string ClassName { get; set; }
        public string Schedule { get; set; }
        // Other properties...
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public ICollection<Grade> Grades { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
        public ICollection<TeachingSchedule> TeachingSchedules { get; set; }
    }
}
