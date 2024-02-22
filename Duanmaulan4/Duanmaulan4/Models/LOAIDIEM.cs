using System.ComponentModel.DataAnnotations;

namespace Duanmaulan4.Models
{
    public class LOAIDIEM
    {
        [Key]
        public int MaLoai { get; set; }
        public string? TenLoai { get; set; }
        public int HeSo { get; set; }

    }
}
