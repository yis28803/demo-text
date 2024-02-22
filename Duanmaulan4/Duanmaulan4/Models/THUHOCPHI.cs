using System.ComponentModel.DataAnnotations;

namespace Duanmaulan4.Models
{
    public class THUHOCPHI
    {
        [Key]
        public int MaHocPhi { get; set; }
        public string? MaHocSinh { get; set; }
        public int MaLop { get; set; }
        public int MaLoaiHocPhi { get; set; }
        public decimal MucThuPhi { get; set; }
        public decimal GiamGia { get; set; }
        public string? GhiChu { get; set; }
        public DateTime NgayThuPhi { get; set; }

        // Navigation properties nếu có
        public HOCSINH? HocSinh { get; set; }
        public LOP? Lop { get; set; }
        public LOAIHOCPHI? LoaiHocPhi { get; set; }
    }
}
