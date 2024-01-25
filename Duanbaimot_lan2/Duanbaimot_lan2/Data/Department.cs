namespace Duanbaimot_lan2.Data
{
    public class Department
    {
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        // Other properties...
        public ICollection<DepartmentsSubjects> DepartmentsSubjects { get; set; }
    }
}
