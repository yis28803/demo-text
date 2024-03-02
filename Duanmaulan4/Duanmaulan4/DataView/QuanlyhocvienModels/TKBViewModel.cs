namespace Duanmaulan4.DataView.QuanlyhocvienModels
{
    public class TKBViewModel
    {
        public int MaPhanCong { get; set; }
        public string? TenMonHoc { get; set; }
        public string? TenGiaoVien { get; set; }
        public string? TenLop { get; set; }
        public string? NgayBatDau { get; set; }
        public string? NgayKetThuc { get; set; }
        public string? ThoiGianHoc { get; set; }
        public string? PhongHoc { get; set; }
        public string? Thu { get; set; }

        // Các thuộc tính khác nếu cần
    }
    public class TKBViewModel2
    {
        public int MaPhanCong { get; set; }
        public string? TenMonHoc { get; set; }
        public string? TenNienKhoa { get; set; }
        public int MaLop { get; set; }
        public string? TenLop { get; set; }
        public int HocPhi { get; set; }
        public bool? TinhTrangHocPhi { get; set; }
        
        // Các thuộc tính khác nếu cần
    }
}
