using System.ComponentModel.DataAnnotations;

namespace Duanmaulan4.Models
{
    public class TOBOMON
    {
        [Key]
        public int MaToBoMon { get; set; }
        public string? TenToBoMon { get; set; }
    }
}
