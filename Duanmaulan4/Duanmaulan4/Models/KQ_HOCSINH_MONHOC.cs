using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Duanmaulan4.Models
{
    public class KQ_HOCSINH_MONHOC
    {
        public string? MaHocSinh { get; set; }
        public int MaLop { get; set; }
        public int MaNienKhoa { get; set; }
        public int MaMonHoc { get; set; }
        public int MaHocKy { get; set; }
        public float DiemMiengTB { get; set; }
        public float Diem15PhutTB { get; set; }
        public float Diem45PhutTB { get; set; }
        public float DiemThi { get; set; }
        public float DiemTBHK { get; set; }

        // Navigation properties nếu có
        public HOCSINH? HocSinh { get; set; }
        public LOP? Lop { get; set; }
        public NIENKHOA? NienKhoa { get; set; }
        public MONHOC? MonHoc { get; set; }
        public HOCKY? HocKy { get; set; }
    }
}
