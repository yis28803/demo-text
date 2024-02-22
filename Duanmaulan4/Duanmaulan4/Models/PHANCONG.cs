using System.ComponentModel.DataAnnotations;

namespace Duanmaulan4.Models
{
    public class PHANCONG
    {
        [Key]
        public int MaPhanCong { get; set; }
        public int MaLop { get; set; }
        public int MaMonHoc { get; set; }
        public string? MaGiaoVien { get; set; }
        public string? PhongHoc { get; set; }
        public string? GioHoc { get; set; }

        public string? NgayHoc { get; set; }

        public string? ThoiGianBatDau { get; set; }

        public string? ThoiGianKetThuc { get; set; }


        // Navigation properties nếu có
        public LOP? Lop { get; set; }
        public MONHOC? MonHoc { get; set; }
        public GIAOVIEN? GiaoVien { get; set; }
    }
}
