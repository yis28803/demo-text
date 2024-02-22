using System.ComponentModel.DataAnnotations;

namespace Duanmaulan4.Models
{
    public class MONHOC
    {
        [Key]
        public int MaMonHoc { get; set; }
        public string? TenMonHoc { get; set; }
        public int MaKhoaKhoi { get; set; }
        public int MaToBoMon { get; set; }

        // Navigation properties nếu có

        public KHOAKHOI? KhoaKhoi { get; set; }
        public TOBOMON? ToBoMon { get; set; }
    }
}
