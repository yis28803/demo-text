using Duanmaulan4.Models;


namespace Duanmaulan4.DataView.QuanlyhocvienModels
{
    public class ClassViewModel
    {
        public int MaPhanCong { get; set; }
        public string? TenLop { get; set; }
        public string? TenKhoaKhoi { get; set; }
        public string? TenNienKhoa { get; set; }
        public string? TenMonHoc { get; set; }
        public int SoLuongHocSinh {  get; set; }
        public string? TenGiaoVien { get; internal set; }
        public string? MaHocSinh { get; set; }
        public PHANCONG? PhanCong { get; internal set; }
    }

}
