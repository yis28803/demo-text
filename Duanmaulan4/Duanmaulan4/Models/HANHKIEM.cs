using System.ComponentModel.DataAnnotations;

namespace Duanmaulan4.Models
{
    public class HANHKIEM
    {
        [Key]
        public int MaHanhKiem { get; set; }
        public string? TenHanhKiem { get; set; }
        
    }
}
