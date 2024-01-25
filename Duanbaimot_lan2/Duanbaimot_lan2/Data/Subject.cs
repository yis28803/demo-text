namespace Duanbaimot_lan2.Data
{
    public class Subject
    {
        public int SubjectID { get; set; }
        public string SubjectName { get; set; }
        // Other properties...
        public ICollection<Grade> Grades { get; set; }
        public ICollection<DepartmentsSubjects> DepartmentsSubjects { get; set; }  // Thêm dòng này
    }
}
