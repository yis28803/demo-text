using Duanmaulan4.Controllers;
using Duanmaulan4.DataView.Authentication;
using Duanmaulan4.DataView.QuanlyhocvienModels;
using Duanmaulan4.Helpers;
using Duanmaulan4.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Duanmaulan4.Services
{
    public class StudentServices : IStudentServices
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ApplicationDbContext context;
        private readonly ILogger<StudentsController> logger;
        public StudentServices(UserManager<ApplicationUser> userManager,
             RoleManager<IdentityRole> roleManager,
            ApplicationDbContext context
            , ILogger<StudentsController> logger)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.context = context;
            this.logger = logger;
        }

        //hoanthanh
        public async Task<List<StudentViewModel>> GetStudentsAsync()
        {
            var students = await context.HocSinh
                .Include(s => s.User) // Include the User (AspNetUser) navigation property
                .Select(s => new StudentViewModel
                {
                    UserId = s.UserId,
                    MaHocSinh = s.MaHocSinh,
                    HinhAnh = s.HinhAnh,
                    Ho = s.Ho,
                    TenDemvaTen = s.TenDemvaTen,
                    Gioitinh = s.GioiTinh,
                    HoTenPhuHuynh = s.HoTenPhuHuynh,
                    Diachi = s.DiaChi,
                    NgaySinh = s.NgaySinh,
                    DienThoai = s.DienThoai,
                    Email = s.Email,      
                    // Add other fields as needed
                })
                .ToListAsync();

            return students;
        }
        public async Task<List<StudentViewModel>> GetStudentsAsync(string searchKeyword)
        {
            var students = await context.HocSinh
                .Include(s => s.User) // Include the User (AspNetUser) navigation property
                .Where(s =>
                    s.MaHocSinh.Contains(searchKeyword) ||
                    s.Email.Contains(searchKeyword) ||
                    s.DienThoai.Contains(searchKeyword)
                )
                .Select(s => new StudentViewModel
                {
                    UserId = s.UserId,
                    MaHocSinh = s.MaHocSinh,
                    HinhAnh = s.HinhAnh,
                    Ho = s.Ho,
                    TenDemvaTen = s.TenDemvaTen,
                    Gioitinh = s.GioiTinh,
                    HoTenPhuHuynh = s.HoTenPhuHuynh,
                    Diachi = s.DiaChi,
                    NgaySinh = s.NgaySinh,
                    DienThoai = s.DienThoai,
                    Email = s.Email,
                    // Add other fields as needed
                })
                .ToListAsync();

            return students;
        }


        public async Task<IdentityResult> SignUpStudentAsync(SignUpModel model, int MaPhanCong)
        {
            var user = new ApplicationUser
            {
                Ho = model.Ho,
                TenDemvaTen = model.TenDemvaTen,
                Email = model.Email,
                DienThoai = model.DienThoai,
                UserName = model.Email
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // Kiểm tra role Student đã có
                if (!await roleManager.RoleExistsAsync(AppRole.Student))
                {
                    await roleManager.CreateAsync(new IdentityRole(AppRole.Student));
                }

                await userManager.AddToRoleAsync(user, AppRole.Student);

                // Kiểm tra xem vai trò có phải là Student hay không
                var userRoles = await userManager.GetRolesAsync(user);
                if (userRoles.Contains(AppRole.Student))
                {
                    // Thêm thông tin người dùng vào bảng Student
                    await AddStudentInfoAsync(user.Id, model.Email, model.GioiTinh, MaPhanCong, model.NgaySinh, model.MaHocSinh, model.Ho,
                        model.TenDemvaTen, model.DienThoai, model.Diachi, model.HoTenPhuHuynh, model.HinhAnh);
                }
            }

            return result;
        }

        private async Task AddStudentInfoAsync(string userId, string email, bool gioiTinh, int MaPhanCong, DateTime ngaySinh,
                                       string maHocSinh, string ho, string tenDemvaTen, string dienThoai,
                                       string diaChi, string hoTenPhuHuynh, string HinhAnh)
        {
            var student = new HOCSINH
            {
                UserId = userId,
                GioiTinh = gioiTinh,
                Email = email,
                MaPhanCong = MaPhanCong,
                NgaySinh = ngaySinh,
                MaHocSinh = maHocSinh,
                Ho = ho,
                TenDemvaTen = tenDemvaTen,
                DienThoai = dienThoai,
                DiaChi = diaChi,
                HoTenPhuHuynh = hoTenPhuHuynh,
                HinhAnh = HinhAnh
            };

            context.HocSinh.Add(student);
            await context.SaveChangesAsync();

            // Thêm thông tin vào bảng PHANLOP
            var phanLop = new PHANLOP
            {
                MaHocSinh = student.MaHocSinh,
                TinhTrangHocPhi = false,
                MaPhanCong = MaPhanCong
            };  

            context.PhanLop.Add(phanLop);
            await context.SaveChangesAsync();
        }

        public async Task<List<ClassViewModel>> GetAllClassesAsync()
        {
            var classes = await context.Lop
                .Join(context.PhanCong,
                      l => l.MaLop,
                      pc => pc.MaLop,
                      (l, pc) => new { Lop = l, PhanCong = pc })
                .Join(context.MonHoc,
                      pc => pc.PhanCong.MaMonHoc,
                      mh => mh.MaMonHoc,
                      (pc, mh) => new { pc.Lop, pc.PhanCong, MonHoc = mh })
                .Join(context.GiaoVien,
                      pc => pc.PhanCong.MaGiaoVien,
                      gv => gv.MaGiaoVien,
                      (pc, gv) => new ClassViewModel
                      {
                          MaPhanCong = pc.PhanCong.MaPhanCong,
                          TenLop = pc.Lop.TenLop,
                          TenKhoaKhoi = pc.Lop != null && pc.Lop.KhoaKhoi != null ? pc.Lop.KhoaKhoi.TenKhoaKhoi : null,
                          TenNienKhoa = pc.Lop != null && pc.Lop.NienKhoa != null ? pc.Lop.NienKhoa.TenNienKhoa : null,
                          SoLuongHocSinh = pc.Lop != null ? pc.Lop.SoLuongHocSinh : 0,
                          TenMonHoc = pc.MonHoc.TenMonHoc,
                          TenGiaoVien = gv.Ho + " " +  gv.TenDemvaTen,
                          // Add other fields as needed
                      })
                .ToListAsync();

            return classes;
        }


        
        public async Task<List<ClassViewModel>> GetClassesEnrolledByStudentAsync(string MaHocSinh)
        {
            var classes = await context.Lop
               .Join(context.PhanCong,
                     l => l.MaLop,
                     pc => pc.MaLop,
                     (l, pc) => new { Lop = l, PhanCong = pc })
               .Join(context.MonHoc,
                     pc => pc.PhanCong.MaMonHoc,
                     mh => mh.MaMonHoc,
                     (pc, mh) => new { pc.Lop, pc.PhanCong, MonHoc = mh })
               .Join(context.GiaoVien,
                     pc => pc.PhanCong.MaGiaoVien,
                     gv => gv.MaGiaoVien,
                     (pc, gv) => new ClassViewModel
                     {
                         MaPhanCong = pc.PhanCong.MaPhanCong,
                         TenLop = pc.Lop.TenLop,
                         TenKhoaKhoi = pc.Lop != null && pc.Lop.KhoaKhoi != null ? pc.Lop.KhoaKhoi.TenKhoaKhoi : null,
                         TenNienKhoa = pc.Lop != null && pc.Lop.NienKhoa != null ? pc.Lop.NienKhoa.TenNienKhoa : null,
                         SoLuongHocSinh = pc.Lop != null ? pc.Lop.SoLuongHocSinh : 0,
                         TenMonHoc = pc.MonHoc.TenMonHoc,
                         TenGiaoVien = $"{gv.Ho} {gv.TenDemvaTen}",
                         // Add other fields as needed
                     })
            .ToListAsync();

            var filteredClasses = classes
                .Where(pc => context.PhanLop
                    .Any(pl => pl.MaHocSinh == MaHocSinh && pl.MaPhanCong == pc.MaPhanCong))
                .ToList();

            return filteredClasses;

        }
        public async Task<List<ClassViewModel>> GetClassesEnrolledByStudentAsync(string MaHocSinh, string tenLopSearch)
        {
            var classes = await context.Lop
               .Join(context.PhanCong,
                     l => l.MaLop,
                     pc => pc.MaLop,
                     (l, pc) => new { Lop = l, PhanCong = pc })
               .Join(context.MonHoc,
                     pc => pc.PhanCong.MaMonHoc,
                     mh => mh.MaMonHoc,
                     (pc, mh) => new { pc.Lop, pc.PhanCong, MonHoc = mh })
               .Join(context.GiaoVien,
                     pc => pc.PhanCong.MaGiaoVien,
                     gv => gv.MaGiaoVien,
                     (pc, gv) => new ClassViewModel
                     {
                         MaPhanCong = pc.PhanCong.MaPhanCong,
                         TenLop = pc.Lop.TenLop,
                         TenKhoaKhoi = pc.Lop != null && pc.Lop.KhoaKhoi != null ? pc.Lop.KhoaKhoi.TenKhoaKhoi : null,
                         TenNienKhoa = pc.Lop != null && pc.Lop.NienKhoa != null ? pc.Lop.NienKhoa.TenNienKhoa : null,
                         SoLuongHocSinh = pc.Lop != null ? pc.Lop.SoLuongHocSinh : 0,
                         TenMonHoc = pc.MonHoc.TenMonHoc,
                         TenGiaoVien = $"{gv.Ho} {gv.TenDemvaTen}",
                         // Add other fields as needed
                     })
            .ToListAsync();

            var filteredClasses = classes
                .Where(pc => context.PhanLop
                    .Any(pl => pl.MaHocSinh == MaHocSinh && pl.MaPhanCong == pc.MaPhanCong) &&
                                (string.IsNullOrEmpty(tenLopSearch) || pc.TenLop.Equals(tenLopSearch, StringComparison.OrdinalIgnoreCase)))
                .ToList();


            return filteredClasses;
        }


        public async Task<bool> RegisterClassAsync(string maHocSinh, int maPhanCong)
        {
            try
            {
                // Kiểm tra xem học sinh đã đăng ký lớp này chưa
                var existingRegistration = await context.PhanLop
                    .FirstOrDefaultAsync(pl => pl.MaHocSinh == maHocSinh && pl.MaPhanCong == maPhanCong);

                if (existingRegistration != null)
                {
                    // Học sinh đã đăng ký lớp này, không thực hiện đăng ký lại
                    return false;
                }

                // Tạo mới bản ghi đăng ký lớp
                var registration = new PHANLOP
                {
                    MaHocSinh = maHocSinh,
                    MaPhanCong = maPhanCong
                    // Các thuộc tính khác của bảng PhanLop nếu có
                };

                // Thêm vào DbContext và lưu thay đổi
                context.PhanLop.Add(registration);
                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw; // Xử lý lỗi tùy theo yêu cầu của bạn
            }
        }


        public async Task<bool> UnregisterClassAsync(string maHocSinh, int maPhanCong)
        {
            try
            {
                // Kiểm tra xem học sinh đã đăng ký lớp này chưa
                var existingRegistration = await context.PhanLop
                    .FirstOrDefaultAsync(pl => pl.MaHocSinh == maHocSinh && pl.MaPhanCong == maPhanCong);

                if (existingRegistration == null)
                {
                    // Học sinh chưa đăng ký lớp này, không thực hiện hủy đăng ký
                    return false;
                }

                // Xóa bản ghi đăng ký lớp
                context.PhanLop.Remove(existingRegistration);
                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw; // Xử lý lỗi tùy theo yêu cầu của bạn
            }
        }
        public async Task<List<TKBViewModel>> GetStudentScheduleAsync(string maHocSinh)
        {
            try
            {
                var studentSchedule = await context.PhanLop
                    .Where(pl => pl.MaHocSinh == maHocSinh)
                    .Join(context.LichHoc,
                          pl => pl.MaPhanCong,
                          lh => lh.MaPhanCong,
                          (pl, lh) => new { PhanLop = pl, LichHoc = lh })
                    .Join(context.PhanCong,
                          lh => lh.PhanLop.MaPhanCong,
                          pc => pc.MaPhanCong,
                          (lh, pc) => new TKBViewModel
                          {
                              MaPhanCong = pc.MaPhanCong,
                              NgayBatDau = pc.NgayBatDau,
                              NgayKetThuc = pc.NgayKetThuc,
                              ThoiGianHoc = lh.LichHoc.ThoiGianHoc,
                              PhongHoc = lh.LichHoc.PhongHoc,
                              Thu = lh.LichHoc.Thu,
                              TenMonHoc = pc.MonHoc.TenMonHoc,
                              TenGiaoVien = pc.GiaoVien.Ho + " " + pc.GiaoVien.TenDemvaTen,
                              TenLop = pc.Lop.TenLop,
                              // Các thuộc tính khác cần thiết
                          })
                    .ToListAsync();

                return studentSchedule;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw; // Xử lý lỗi tùy theo yêu cầu của bạn
            }
        }
        public async Task<StudentDetailViewModel> GetStudentDetailsAsync(string maHocSinh)
        {
            try
            {
                // Lấy thông tin cơ bản của sinh viên
                var studentInfo = await context.HocSinh
                    .Where(s => s.MaHocSinh == maHocSinh)
                    .Select(s => new StudentDetailViewModel
                    {
                        MaHocSinh = s.MaHocSinh,
                        Ho = s.Ho,
                        TenDemvaTen = s.TenDemvaTen,
                        NgaySinh = s.NgaySinh,
                        DiaChi = s.DiaChi,
                        Email = s.Email,
                    })
                    .FirstOrDefaultAsync();

                if (studentInfo == null)
                {
                    // Sinh viên không tồn tại
                    return null;
                }

                // Lấy lịch học của sinh viên
                var studentClasses = await context.PhanLop
                    .Where(pl => pl.MaHocSinh == maHocSinh)
                    .Join(context.PhanCong,
                          pl => pl.MaPhanCong,
                          pc => pc.MaPhanCong,
                          (pl, pc) => new TKBViewModel2
                          {
                              MaPhanCong = pc.MaPhanCong,
                              TenMonHoc = pc.MonHoc.TenMonHoc,
                              TenNienKhoa = pc.Lop.NienKhoa.TenNienKhoa,
                              MaLop = pc.Lop.MaLop,
                              TenLop = pc.Lop.TenLop,
                              HocPhi = pc.Lop.HocPhi,
                              TinhTrangHocPhi = pl.TinhTrangHocPhi,
                              // Thêm các thuộc tính khác nếu cần
                          })
                    .ToListAsync();


                // Gán lịch học vào thông tin sinh viên
                studentInfo.PhanLop = studentClasses;

                return studentInfo;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw; // Xử lý lỗi tùy theo yêu cầu của bạn
            }
        }

        public async Task<IdentityResult> UpdateStudentAsync(string maHocSinh, string ho, string tenDemvaTen,
                DateTime ngaySinh, bool gioiTinh, string email, string dienThoai, string diaChi, string hoTenPhuHuynh, string hinhAnh, int? maPhanCong)
        {
            var student = await context.HocSinh.FirstOrDefaultAsync(s => s.MaHocSinh == maHocSinh);

            if (student == null)
            {
                // Học sinh không tồn tại
                return IdentityResult.Failed(new IdentityError { Description = "Student not found." });
            }

            // Cập nhật thông tin học sinh
            student.Ho = ho;
            student.TenDemvaTen = tenDemvaTen;
            student.NgaySinh = ngaySinh;
            student.GioiTinh = gioiTinh;
            student.Email = email;
            student.DienThoai = dienThoai;
            student.DiaChi = diaChi;
            student.HoTenPhuHuynh = hoTenPhuHuynh;
            student.HinhAnh = hinhAnh;

            // Cập nhật thông tin lớp học (nếu có)
            if (maPhanCong.HasValue)
            {
                var studentClass = await GetStudentClassAsync(maHocSinh);

                if (studentClass != null)
                {
                    // Hủy đăng ký lớp cũ
                    context.PhanLop.Remove(studentClass);
                }

                // Đăng ký lớp mới
                var newStudentClass = new PHANLOP
                {
                    MaHocSinh = maHocSinh,
                    TinhTrangHocPhi = false,
                    MaPhanCong = maPhanCong.Value,
                    // Thêm các thuộc tính khác nếu cần
            };

                context.PhanLop.Add(newStudentClass);
            }

            // Lưu thay đổi vào DbContext
            await context.SaveChangesAsync();

            return IdentityResult.Success;
        }
        public async Task<PHANLOP> GetStudentClassAsync(string maHocSinh)
        {
            return await context.PhanLop.FirstOrDefaultAsync(pl => pl.MaHocSinh == maHocSinh);
        }
        public async Task<bool> DeleteStudentAsync(string maHocSinh)
        {
            try
            {
                var student = await context.HocSinh.FindAsync(maHocSinh);

                if (student != null)
                {
                    // Xóa các liên kết từ bảng PhanLop trước
                    var enrollments = context.PhanLop.Where(e => e.MaHocSinh == maHocSinh);
                    context.PhanLop.RemoveRange(enrollments);

                    // Xóa thông tin học sinh
                    context.HocSinh.Remove(student);

                    // Xóa thông tin User
                    var userId = student.UserId;
                    var user = await userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        var result = await userManager.DeleteAsync(user);
                        if (!result.Succeeded)
                        {
                            // Xử lý lỗi khi xóa User
                            return false;
                        }
                    }

                    await context.SaveChangesAsync();
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                // Xử lý lỗi nếu có
                return false;
            }
        }
        public async Task<List<StudentViewModel>> GetNewStudentsAsync()
        {
            var newStudents = await context.HocSinh
                .Join(context.PhanLop,
                    hs => hs.MaHocSinh,
                    pl => pl.MaHocSinh,
                    (hs, pl) => new { HocSinh = hs, PhanLop = pl })
                .Where(joinResult => !joinResult.PhanLop.TinhTrangHocPhi)
                .Join(context.PhanCong,
                    result => result.PhanLop.MaPhanCong,
                    pc => pc.MaPhanCong,
                    (result, pc) => new { result.HocSinh, result.PhanLop, MaLop = pc.MaLop })
                .Select(joinResult => new StudentViewModel
                {
                    UserId = joinResult.HocSinh.UserId,
                    MaHocSinh = joinResult.HocSinh.MaHocSinh,
                    HinhAnh = joinResult.HocSinh.HinhAnh,
                    Ho = joinResult.HocSinh.Ho,
                    TenDemvaTen = joinResult.HocSinh.TenDemvaTen,
                    Gioitinh = joinResult.HocSinh.GioiTinh,
                    HoTenPhuHuynh = joinResult.HocSinh.HoTenPhuHuynh,
                    Diachi = joinResult.HocSinh.DiaChi,
                    NgaySinh = joinResult.HocSinh.NgaySinh,
                    DienThoai = joinResult.HocSinh.DienThoai,
                    Email = joinResult.HocSinh.Email,
                    MaLop = joinResult.MaLop,
                    // Add other fields as needed
                })
                .ToListAsync();

            // Filter out duplicates based on MaHocSinh and MaLop
            var distinctStudents = newStudents
                .GroupBy(s => new { s.MaHocSinh, s.MaLop })
                .Select(group => group.First())
                .ToList();

            return distinctStudents;
        }
        public async Task<bool> ThuHocPhiAsync(string maHocSinh, int maLop, int maLoaiHocPhi, decimal mucThuPhi, decimal giamGia, string ghiChu)
        {
            try
            {
                // Step 1: Get all MaPhanCong for the specified MaLop
                var maPhanCongList = await context.PhanCong
                    .Where(pc => pc.MaLop == maLop)
                    .Select(pc => pc.MaPhanCong)
                    .ToListAsync();

                // Check if MaPhanCongList is empty
                if (maPhanCongList == null || !maPhanCongList.Any())
                {
                    // Handle the case where maPhanCongList is not found or empty
                    return false;
                }

                // Step 2: Check if Hoc Phi has already been collected for the given MaHocSinh and any MaPhanCong
                var daThuHocPhi = await context.ThuHocPhi
                    .AnyAsync(thp => thp.MaHocSinh == maHocSinh && thp.MaLop == maLop);

                // Step 3: If not, proceed to collect Hoc Phi
                if (!daThuHocPhi)
                {
                    // Get the current date and time
                    DateTime ngayThuPhi = DateTime.Now;

                    // Choose one MaPhanCong from the list (for example, the first one)
                    var maPhanCong = maPhanCongList.First();

                    // Create a new THUHOCPHI object
                    var thuHocPhi = new THUHOCPHI
                    {
                        MaHocSinh = maHocSinh,
                        MaLoaiHocPhi = maLoaiHocPhi,
                        MaLop = maLop,
                        MucThuPhi = mucThuPhi,
                        GiamGia = giamGia,
                        GhiChu = ghiChu,
                        NgayThuPhi = ngayThuPhi
                    };

                    // Update TinhTrangHocPhi in the PhanLop table
                    var enrollments = await context.PhanLop
                        .Where(e => e.MaHocSinh == maHocSinh && maPhanCongList.Contains(e.MaPhanCong))
                        .ToListAsync();

                    foreach (var enrollment in enrollments)
                    {
                        enrollment.TinhTrangHocPhi = true;
                    }


                    // Add the new THUHOCPHI to the database and save changes
                    context.ThuHocPhi.Add(thuHocPhi);
                    await context.SaveChangesAsync();

                    // Return true after processing
                    return true;
                }

                // Step 4: If Hoc Phi has already been collected, return false
                return false;

            }
            catch (Exception ex)
            {
                // Log the error
                logger.LogError($"Error in ThuHocPhiAsync: {ex.Message}");
                return false;
            }
        }
    }
}
