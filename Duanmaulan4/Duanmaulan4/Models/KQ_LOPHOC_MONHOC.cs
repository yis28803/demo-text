using System.ComponentModel.DataAnnotations;

namespace Duanmaulan4.Models
{
    public class KQ_LOPHOC_MONHOC
    {
        [Key]
        public int MaLop { get; set; }
        public int MaNienKhoa { get; set; }
        public int MaMonHoc { get; set; }
        public int MaHocKy { get; set; }
        public int SoLuongDat { get; set; }
        public float TiLe { get; set; }
        // Navigation properties nếu có
        public LOP? Lop { get; set; }
        public NIENKHOA? NienKhoa { get; set; }
        public MONHOC? MonHoc { get; set; }
        public HOCKY? HocKy { get; set; }
    }
}
