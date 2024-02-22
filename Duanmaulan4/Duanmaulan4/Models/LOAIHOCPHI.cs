using System.ComponentModel.DataAnnotations;

namespace Duanmaulan4.Models
{
    public class LOAIHOCPHI
    {
        [Key]
        public int MaLoaiHocPhi { get; set; }
        public string? TenLoaiHocPhi { get; set; }
    }
}
