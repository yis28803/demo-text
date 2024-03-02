using System.ComponentModel.DataAnnotations;

namespace Duanmaulan4.Models
{
    public class LOP
    {
        [Key]
        public int MaLop { get; set; }
        public string? MaLopPhu { get; set; }
        public string? TenLop { get; set; }
        public int MaKhoaKhoi { get; set; }
        public string? MaNienKhoa { get; set; }
        public int SoLuongHocSinh { get; set; }
        public bool TrangThai { get; set; }
        public int HocPhi { get; set; }
        public string? Mota { get; set; }
        public string? Hinhanh { get; set; }

        // Navigation properties nếu có
        /*Khối lớp*/
        public KHOAKHOI? KhoaKhoi { get; set; }
        /*Năm Học*/
        public NIENKHOA? NienKhoa { get; set; }
    }
}
