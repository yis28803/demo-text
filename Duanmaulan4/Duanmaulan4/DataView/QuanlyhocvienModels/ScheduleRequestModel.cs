namespace Duanmaulan4.DataView.QuanlyhocvienModels
{
    public class ScheduleRequestModel
    {
        public int StudentId { get; set; }
        public int ClassId { get; set; }
        public DateTime NgayHoc { get; set; }
        public int ThangHoc { get; set; }
        public int ThuHoc { get; set; }
        // Thêm các trường thông tin khác nếu cần
    }
}
