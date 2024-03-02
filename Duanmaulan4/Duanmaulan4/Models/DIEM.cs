using System.ComponentModel.DataAnnotations;

namespace Duanmaulan4.Models
{
    public class DIEM
    {
        [Key]
        public int MaDiem { get; set; }
        public string? MaHocSinh { get; set; }
        public int MaMonHoc { get; set; }
        public int MaPhanCong { get; set; }
        public int MaLoaiDiem { get; set; }
        public string? Diem { get; set; }
        
        // Navigation properties
        public HOCSINH? HocSinh { get; set; }
        public MONHOC? MonHoc { get; set; }
        public LOAIDIEM? LoaiDiem { get; set; }
        public PHANCONG? PhanCong { get; set; }


    }
}
