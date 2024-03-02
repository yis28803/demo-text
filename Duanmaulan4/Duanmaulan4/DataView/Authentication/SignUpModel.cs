using System.ComponentModel.DataAnnotations;

namespace Duanmaulan4.DataView.Authentication
{
    public class SignUpModel
    {
        [Required]
        public string Ho { get; set; } = null!;

        [Required]
        public string TenDemvaTen { get; set; } = null!;

        [Required, EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public string DienThoai { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        [Required]
        public string ConfirmPassword { get; set; } = null!;

        public bool GioiTinh { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime NgaySinh { get; set; }

        [Required]
        public string? MaHocSinh { get; set; } // Thêm trường MaHocSinh
        public string? HoTenPhuHuynh { get; set; }
        public string? Diachi { get; set; }
        public string? HinhAnh { get; set; }
    }


}
