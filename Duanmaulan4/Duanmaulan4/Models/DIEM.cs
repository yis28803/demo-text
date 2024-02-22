using System.ComponentModel.DataAnnotations;

namespace Duanmaulan4.Models
{
    public class DIEM
    {
        [Key]
        public int STT { get; set; }
        public string? MaHocSinh { get; set; }
        public int MaMonHoc { get; set; }
        public int MaHocKy { get; set; }
        public int MaNienKhoa { get; set; }
        public int MaLop { get; set; }
        public int MaLoai { get; set; }
        public float Diem { get; set; }
        
        // Navigation properties
        public HOCKY? HocKy { get; set; }
        public HOCSINH? HocSinh { get; set; }
        public MONHOC? MonHoc { get; set; }
        public NIENKHOA? NienKhoa { get; set; }
        public LOAIDIEM? LoaiDiem { get; set; }
        public LOP? Lop { get; set; }
    }
}
