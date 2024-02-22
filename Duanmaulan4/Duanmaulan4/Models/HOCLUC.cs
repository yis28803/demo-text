using System.ComponentModel.DataAnnotations;

namespace Duanmaulan4.Models
{
    public class HOCLUC
    {
        [Key]
        public int MaHocLuc { get; set; }
        public string? TenHocLuc { get; set;}
        public int DiemCanDuoi { get; set;}
        public int DiemCanTren {  get; set;}
        public int DiemKhongChe {  get; set;}
    }
}
