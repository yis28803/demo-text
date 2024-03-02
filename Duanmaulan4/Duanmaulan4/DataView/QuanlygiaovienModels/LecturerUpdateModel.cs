namespace Duanmaulan4.DataView.QuanlygiaovienModels
{
    public class LecturerUpdateModel
    {
        // Các thuộc tính cần cập nhật
        public string? MaSoThue { get; set; }
        public string? MaGiaoVien {  get; set; }
        public string? Ho { get; set; }
        public string? TenDemvaTen { get; set; }
        public DateTime NgaySinh { get; set; }
        public bool GioiTinh { get; set; }
        public string? Email { get; set; }
        public string? DienThoai { get; set; }
        public string? DiaChi { get; set; }
        public int MaMonHoc { get; set; }
        public string? MonKiemNhiem { get; set; }
        public string? HinhAnh { get; set; }

        // Thuộc tính mới
        public string? DiaChiMoi { get; set; }
    }
}
