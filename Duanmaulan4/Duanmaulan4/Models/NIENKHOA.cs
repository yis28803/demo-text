using System.ComponentModel.DataAnnotations;

namespace Duanmaulan4.Models
{
    public class NIENKHOA
    {
        [Key]
        public string? MaNienKhoa { get; set; }
        public string? MaNienKhoaPhu { get; set; }
        public string? TenNienKhoa { get; set; }
    }
}
