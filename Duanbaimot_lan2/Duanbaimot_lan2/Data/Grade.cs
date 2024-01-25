namespace Duanbaimot_lan2.Data
{
    public class Grade
    {
        public int GradeID { get; set; }
        public int StudentID { get; set; }
        public int ClassID { get; set; }
        public int SubjectID { get; set; }
        public int GradeTypeID { get; set; }
        public decimal GradeValue { get; set; }
        // Other properties...
        public Student Student { get; set; }
        public Class Class { get; set; }
        public Subject Subject { get; set; }
        public GradeType GradeType { get; set; }
    }
}
