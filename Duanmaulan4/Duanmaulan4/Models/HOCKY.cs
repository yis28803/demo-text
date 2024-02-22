using System.ComponentModel.DataAnnotations;

namespace Duanmaulan4.Models
{
    public class HOCKY
    {
        [Key]
        public int MaHocKy { get; set; }
        public string? TenHocKy {  set; get; }
        public int HeSo {  get; set; }
    }
}
