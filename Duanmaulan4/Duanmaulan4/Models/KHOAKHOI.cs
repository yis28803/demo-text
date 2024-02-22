using System.ComponentModel.DataAnnotations;

namespace Duanmaulan4.Models
{
    public class KHOAKHOI
    {
        [Key]
        public int MaKhoaKhoi { get; set; }
        public string? TenKhoaKhoi { get; set; }
    }
}
