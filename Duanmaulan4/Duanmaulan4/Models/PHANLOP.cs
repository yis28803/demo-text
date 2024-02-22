using System.ComponentModel.DataAnnotations;

namespace Duanmaulan4.Models
{
    public class PHANLOP
    {
        [Key]
        public int PhanLopId { get; set; }
        public int MaPhanCong { get; set; }
        public string? MaHocSinh { get; set; }
        public bool TinhTrangHocPhi { get; set; } 

        // Navigation properties nếu có
        public PHANCONG? PhanCong { get; set; }
        public HOCSINH? HocSinh { get; set; }
    }
}
