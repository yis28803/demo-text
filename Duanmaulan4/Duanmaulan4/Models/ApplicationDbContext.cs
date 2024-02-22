using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Duanmaulan4.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSet cho từng lớp model
        public DbSet<DIEM> Diem { get; set; }
        public DbSet<GIAOVIEN> GiaoVien { get; set; }
        public DbSet<HANHKIEM> HanhKiem { get; set; }
        public DbSet<HOCKY> HocKy { get; set; }
        public DbSet<HOCLUC> HocLuc { get; set; }
        public DbSet<HOCSINH> HocSinh { get; set; }
        public DbSet<KETQUA> KetQua { get; set; }
        public DbSet<KHOAKHOI> KhoaKhoi { get; set; }
        public DbSet<KQ_HOCSINH_CANAM> KqHocSinhCaNam { get; set; }
        public DbSet<KQ_HOCSINH_MONHOC> KqHocSinhMonHoc { get; set; }
        public DbSet<KQ_LOPHOC_HOCKY> KqLopHocHocky { get; set; }
        public DbSet<KQ_LOPHOC_MONHOC> KqLopHocMonHoc { get; set; }
        public DbSet<LOAIDIEM> LoaiDiem { get; set; }
        public DbSet<LOAIHOCPHI> LoaiHocPhi { get; set; }
        public DbSet<LOP> Lop { get; set; }
        public DbSet<MONHOC> MonHoc { get; set; }
        public DbSet<NIENKHOA> NienKhoa { get; set; }
        public DbSet<PHANCONG> PhanCong { get; set; }
        public DbSet<PHANLOP> PhanLop { get; set; }
        public DbSet<THUHOCPHI> ThuHocPhi { get; set; }
        public DbSet<TOBOMON> ToBoMon { get; set; }

        // Override phương thức OnModelCreating để cấu hình mối quan hệ
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Mối quan hệ giữa AspNetUsers và hocsinh
            modelBuilder.Entity<HOCSINH>()
                .HasOne(s => s.User)
                .WithOne()
                .HasForeignKey<HOCSINH>(s => s.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<HOCSINH>()
               .HasOne(s => s.PhanCong)
               .WithMany()
               .HasForeignKey(s => s.MaPhanCong)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<GIAOVIEN>()
               .HasOne(s => s.User)
               .WithOne()
               .HasForeignKey<GIAOVIEN>(s => s.UserId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<GIAOVIEN>()
              .HasOne(s => s.MonHoc)
              .WithOne()
              .HasForeignKey<GIAOVIEN>(s => s.MaMonHoc)
              .OnDelete(DeleteBehavior.Restrict);

            // Cấu hình mối quan hệ 1-nhiều và khóa ngoại
            modelBuilder.Entity<DIEM>()
                .HasOne(e => e.HocKy)
                .WithMany()
                .HasForeignKey(e => e.MaHocKy)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<DIEM>()
                .HasOne(e => e.HocSinh)
                .WithMany()
                .HasForeignKey(e => e.MaHocSinh)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<DIEM>()
                .HasOne(e => e.MonHoc)
                .WithMany()
                .HasForeignKey(e => e.MaMonHoc)
                .OnDelete(DeleteBehavior.Restrict); 
            modelBuilder.Entity<DIEM>()
                .HasOne(e => e.NienKhoa)
                .WithMany()
                .HasForeignKey(e => e.MaNienKhoa)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<DIEM>()
                .HasOne(e => e.LoaiDiem)
                .WithMany()
                .HasForeignKey(e => e.MaLoai)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<DIEM>()
                .HasOne(e => e.Lop)
                .WithMany()
                .HasForeignKey(e => e.MaLop)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<KQ_HOCSINH_CANAM>()
                .HasOne(e => e.HocSinh)
                .WithMany()
                .HasForeignKey(e => e.MaHocSinh)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<KQ_HOCSINH_CANAM>()
                .HasOne(e => e.Lop)
                .WithMany()
                .HasForeignKey(e => e.MaLop)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<KQ_HOCSINH_CANAM>()
                .HasOne(e => e.NienKhoa)
                .WithMany()
                .HasForeignKey(e => e.MaNienKhoa)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<KQ_HOCSINH_CANAM>()
                .HasOne(e => e.HocLuc)
                .WithMany()
                .HasForeignKey(e => e.MaHocLuc)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<KQ_HOCSINH_CANAM>()
                .HasOne(e => e.HanhKiem)
                .WithMany()
                .HasForeignKey(e => e.MaHanhKiem)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<KQ_HOCSINH_CANAM>()
                .HasOne(e => e.KetQua)
                .WithMany()
                .HasForeignKey(e => e.MaKetQua)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<KQ_HOCSINH_MONHOC>()
                .HasOne(e => e.HocSinh)
                .WithMany()
                .HasForeignKey(e => e.MaHocSinh)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<KQ_HOCSINH_MONHOC>()
                .HasOne(e => e.Lop)
                .WithMany()
                .HasForeignKey(e => e.MaLop)
    .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<KQ_HOCSINH_MONHOC>()
                .HasOne(e => e.NienKhoa)
                .WithMany()
                .HasForeignKey(e => e.MaNienKhoa)
    .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<KQ_HOCSINH_MONHOC>()
                .HasOne(e => e.MonHoc)
                .WithMany()
                .HasForeignKey(e => e.MaMonHoc)
    .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<KQ_HOCSINH_MONHOC>()
                .HasOne(e => e.HocKy)
                .WithMany()
                .HasForeignKey(e => e.MaHocKy)
    .OnDelete(DeleteBehavior.Restrict);

            
            modelBuilder.Entity<KQ_LOPHOC_HOCKY>()
                .HasOne(e => e.Lop)
                .WithMany()
                .HasForeignKey(e => e.MaLop)
    .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<KQ_LOPHOC_HOCKY>()
                .HasOne(e => e.NienKhoa)
                .WithMany()
                .HasForeignKey(e => e.MaNienKhoa)
    .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<KQ_LOPHOC_HOCKY>()
                .HasOne(e => e.HocKy)
                .WithMany()
                .HasForeignKey(e => e.MaHocKy)
    .OnDelete(DeleteBehavior.Restrict);
           
            modelBuilder.Entity<KQ_LOPHOC_MONHOC>()
                .HasOne(e => e.Lop)
                .WithMany()
                .HasForeignKey(e => e.MaLop)
    .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<KQ_LOPHOC_MONHOC>()
                .HasOne(e => e.NienKhoa)
                .WithMany()
                .HasForeignKey(e => e.MaNienKhoa)
    .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<KQ_LOPHOC_MONHOC>()
                .HasOne(e => e.MonHoc)
                .WithMany()
                .HasForeignKey(e => e.MaMonHoc)
    .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<KQ_LOPHOC_MONHOC>()
                .HasOne(e => e.HocKy)
                .WithMany()
                .HasForeignKey(e => e.MaHocKy)
    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<LOP>()
                .HasOne(e => e.KhoaKhoi)
                .WithMany()
                .HasForeignKey(e => e.MaKhoaKhoi)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<LOP>()
                .HasOne(e => e.NienKhoa)
                .WithMany()
                .HasForeignKey(e => e.MaNienKhoa)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MONHOC>()
                .HasOne(e => e.KhoaKhoi)
                .WithMany()
                .HasForeignKey(e => e.MaKhoaKhoi)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<MONHOC>()
                .HasOne(e => e.ToBoMon)
                .WithMany()
                .HasForeignKey(e => e.MaToBoMon)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PHANCONG>()
                .HasOne(e => e.Lop)
                .WithMany()
                .HasForeignKey(e => e.MaLop)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<PHANCONG>()
                .HasOne(e => e.MonHoc)
                .WithMany()
                .HasForeignKey(e => e.MaMonHoc)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<PHANCONG>()
                .HasOne(e => e.GiaoVien)
                .WithMany()
                .HasForeignKey(e => e.MaGiaoVien)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PHANLOP>()
                .HasOne(e => e.PhanCong)
                .WithMany()
                .HasForeignKey(e => e.MaPhanCong)
    .OnDelete(DeleteBehavior.Restrict);
            /*modelBuilder.Entity<PHANLOP>()
                .HasOne(e => e.NienKhoa)
                .WithMany()
                .HasForeignKey(e => e.MaNienKhoa)
    .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<PHANLOP>()
                .HasOne(e => e.KhoaKhoi)
                .WithMany()
                .HasForeignKey(e => e.MaKhoaKhoi)
    .OnDelete(DeleteBehavior.Restrict);*/
            modelBuilder.Entity<PHANLOP>()
                .HasOne(e => e.HocSinh)
                .WithMany()
                .HasForeignKey(e => e.MaHocSinh)
    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<THUHOCPHI>()
                .HasOne(e => e.Lop)
                .WithMany()
                .HasForeignKey(e => e.MaLop)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<THUHOCPHI>()
                .HasOne(e => e.LoaiHocPhi)
                .WithMany()
                .HasForeignKey(e => e.MaLoaiHocPhi)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<THUHOCPHI>()
                .HasOne(e => e.HocSinh)
                .WithMany()
                .HasForeignKey(e => e.MaHocSinh)
                .OnDelete(DeleteBehavior.Restrict);
            // Thêm cấu hình cho các mối quan hệ khác
            modelBuilder.Entity<KQ_HOCSINH_CANAM>()
                .HasKey(kq => new { kq.MaHocSinh, kq.MaLop, kq.MaNienKhoa});

            modelBuilder.Entity<KQ_HOCSINH_MONHOC>()
               .HasKey(kq => new { kq.MaHocSinh, kq.MaLop, kq.MaNienKhoa, kq.MaMonHoc, kq.MaHocKy });
            modelBuilder.Entity<KQ_LOPHOC_HOCKY>()
               .HasKey(kq => new { kq.MaLop, kq.MaNienKhoa, kq.MaHocKy });
            modelBuilder.Entity<KQ_LOPHOC_MONHOC>()
               .HasKey(kq => new { kq.MaMonHoc, kq.MaLop, kq.MaNienKhoa, kq.MaHocKy });

            modelBuilder.Entity<PHANLOP>()
               .HasKey(kq => new { kq.MaHocSinh, kq.MaPhanCong });

            base.OnModelCreating(modelBuilder);
        }
    }
}
