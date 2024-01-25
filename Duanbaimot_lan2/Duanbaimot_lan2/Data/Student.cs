namespace Duanbaimot_lan2.Data
{
    public class Student
    {
        public int StudentID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        // Other properties...
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
        public ICollection<Grade> Grades { get; set; }
    }
}
