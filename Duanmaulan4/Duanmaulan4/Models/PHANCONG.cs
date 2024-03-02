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
        public string? NgayBatDau { get; set; }
        public string? NgayKetThuc { get; set; }
        public bool ChotDiem { get; set; }
        public bool ChotLuong { get; set; } = false;
        public List<LICHHOC>? LichHoc { get; set; }

        // Navigation properties nếu có
        public LOP? Lop { get; set; }
        public MONHOC? MonHoc { get; set; }
        public GIAOVIEN? GiaoVien { get; set; }
    }
}
