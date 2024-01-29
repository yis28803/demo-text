namespace Duanbaimot_lan2.Data
{
    public class Enrollment
    {
        public int EnrollmentId { get; set; }
        public int StudentId { get; set; }    // Khóa ngoại liên kết với bảng Students
        public int ClassId { get; set; }     // Khóa ngoại liên kết với bảng Courses
        public DateTime EnrollmentDate { get; set; }
        // Thêm các trường thông tin khác nếu cần
        public Class? Class { get; set; }
        public Student? Student { get; set; }  // Thêm thuộc tính Student
    }
}
