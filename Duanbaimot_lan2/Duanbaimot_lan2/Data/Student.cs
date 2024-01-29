namespace Duanbaimot_lan2.Data
{
    public class Student
    {
        public string? UserId { get; set; }  // Khóa ngoại liên kết với bảng AspNetUsers
        public int StudentId { get; set; }
        public string? StudentCode { get; set; }
        // Thêm các trường thông tin khác nếu cần
        public ApplicationUser? User { get; set; }
    }
}
