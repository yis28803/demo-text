namespace Duanmaulan4.DataView.QuanlyhocvienModels
{
    public class FeeRequestModel
    {
        public int StudentId { get; set; }
        public int ClassId { get; set; }
        public string? FeeType { get; set; }
        public decimal Amount { get; set; }
        // Thêm các trường thông tin khác nếu cần
    }
}
