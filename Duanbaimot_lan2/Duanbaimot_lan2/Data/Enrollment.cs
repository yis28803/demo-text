namespace Duanbaimot_lan2.Data
{
    public class Enrollment
    {
        public int EnrollmentID { get; set; }
        public int StudentID { get; set; }
        public int ClassID { get; set; }
        public DateTime EnrollmentDate { get; set; }
        // Other properties...
        public Student Student { get; set; }
        public Class Class { get; set; }
    }
}
