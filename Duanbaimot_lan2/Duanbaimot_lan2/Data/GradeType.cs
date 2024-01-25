namespace Duanbaimot_lan2.Data
{
    public class GradeType
    {
        public int GradeTypeID { get; set; }
        public string TypeName { get; set; }
        // Other properties...
        public ICollection<Grade> Grades { get; set; }
    }
}
