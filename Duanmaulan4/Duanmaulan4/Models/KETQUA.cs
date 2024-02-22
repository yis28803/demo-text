using System.ComponentModel.DataAnnotations;

namespace Duanmaulan4.Models
{
    public class KETQUA
    {
        [Key]
        public int MaKetQua { get; set; }
        public string? TenKetQua { get; set; }

    }
}
