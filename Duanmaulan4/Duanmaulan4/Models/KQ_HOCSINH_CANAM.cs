using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Duanmaulan4.Models
{
    public class KQ_HOCSINH_CANAM
    {
        [Key]
        public string? MaHocSinh { get; set; }
        public int MaLop { get; set; }

        public int MaNienKhoa { get; set; }

        public int MaHocLuc { get; set; }
        public int MaHanhKiem { get; set; }
        public int MaKetQua { get; set; }
        public float DiemTBHK1 { get; set; }
        public float DiemTBHK2 { get; set; }
        public float DiemTBCN { get; set; }

        // 
        public HOCSINH? HocSinh { get; set; }
        public LOP? Lop { get; set; }
        public NIENKHOA? NienKhoa { get; set; }
        public HOCLUC? HocLuc { get; set; }
        public HANHKIEM? HanhKiem { get; set; }
        public KETQUA? KetQua { get; set; }
    }
}
