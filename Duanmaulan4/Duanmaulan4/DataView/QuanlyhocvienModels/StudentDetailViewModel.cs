﻿namespace Duanmaulan4.DataView.QuanlyhocvienModels
{
    public class StudentDetailViewModel
    {
        public string? MaHocSinh { get; set; }

        public string? Ho { get; set; }
        public string? TenDemvaTen { get; set; }
        public DateTime NgaySinh { get; set; }
        public string? DiaChi { get; set; }
        public string? Email { get; set; }

        // Thêm các thuộc tính khác của HocSinh cần lấy
        public List<TKBViewModel2>? PhanLop { get; set; }
    }

}
