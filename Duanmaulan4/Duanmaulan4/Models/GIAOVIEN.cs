using System.ComponentModel.DataAnnotations;

namespace Duanmaulan4.Models
{
    public class GIAOVIEN
    {
        [Key]
        public string? MaGiaoVien { get; set; }
        public string? UserId { get; set; }
        public string? MaSoThue { get; set; }
        public string? Ho { get; set; }
        public string? TenDemvaTen { get; set; }
        public DateTime NgaySinh { get; set; }
        public bool GioiTinh { get; set; }
        public string? Email { get; set; }
        public string? DienThoai { get; set; }
        public string? DiaChi { get; set; }
        public int MaMonHoc { get; set; }
        public string? MonKiemNhiem { get; set; }
        public string? MatKhau { get; set; }
        public string? HinhAnh { get; set; }

        // Thêm các thuộc tính mới theo yêu cầu

        public ApplicationUser? User { get; set; }
        public MONHOC? MonHoc { get; set; }

    }
}
