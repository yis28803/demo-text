using System.ComponentModel.DataAnnotations;

namespace Duanmaulan4.Models
{
    public class NIENKHOA
    {
        [Key]
        public int MaNienKhoa { get; set; }
        public string? TenNienKhoa { get; set; }
    }
}
