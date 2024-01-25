namespace Duanbaimot_lan2.Data
{
    public class DepartmentsSubjects
    {
        public int DepartmentSubjectID { get; set; }
        public int DepartmentID { get; set; }
        public int SubjectID { get; set; }  // Thêm thuộc tính SubjectID
        // Other properties...
        public Department Department { get; set; }
        public Subject Subject { get; set; }  // Thêm thuộc tính Subject
    }
}
