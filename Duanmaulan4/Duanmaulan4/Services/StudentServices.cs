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

        public async Task<List<StudentViewModel>> GetStudentsAsync()
        {
            var students = await context.HocSinh
                .Include(s => s.User) // Include the User (AspNetUser) navigation property
                .Select(s => new StudentViewModel
                {
                    MaHocSinh = s.MaHocSinh,
                    UserId = s.UserId,
                    Gioitinh = s.GioiTinh,
                    Ho = s.Ho,
                    TenDemvaTen = s.TenDemvaTen,
                    Diachi = s.DiaChi,   
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
                    await AddStudentInfoAsync(user.Id, model.Email, model.GioiTinh, MaPhanCong, model.NgaySinh, model.MaHocSinh, model.Ho, model.TenDemvaTen);
                }
            }

            return result;
        }

        private async Task AddStudentInfoAsync(string userId, string email, bool gioiTinh, int MaPhanCong, DateTime ngaySinh, string maHocSinh, string Ho, string TenDemvaTen)
        {
            var student = new HOCSINH
            {
                UserId = userId,
                GioiTinh = gioiTinh,
                Email = email,
                MaPhanCong = MaPhanCong,
                NgaySinh = ngaySinh,
                MaHocSinh = maHocSinh,
                Ho = Ho,
                TenDemvaTen = TenDemvaTen
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
                    .Join(context.PhanCong,
                          pl => pl.MaPhanCong,
                          pc => pc.MaPhanCong,
                          (pl, pc) => new { PhanLop = pl, PhanCong = pc })
                    .Join(context.MonHoc,
                          pc => pc.PhanCong.MaMonHoc,
                          mh => mh.MaMonHoc,
                          (pc, mh) => new TKBViewModel
                          {
                              MaPhanCong = pc.PhanCong.MaPhanCong,
                              ThoiGianBatDau = pc.PhanCong.ThoiGianBatDau,
                              ThoiGianKetThuc = pc.PhanCong.ThoiGianKetThuc,
                              NgayHoc = pc.PhanCong.NgayHoc,
                              TenMonHoc = mh.TenMonHoc,
                              TenGiaoVien = context.GiaoVien
                                              .Where(gv => gv.MaGiaoVien == pc.PhanCong.MaGiaoVien)
                                              .Select(gv => gv.Ho + " " + gv.TenDemvaTen)
                                              .FirstOrDefault(),
                              TenLop = context.Lop
                                            .Where(l => l.MaLop == pc.PhanCong.MaLop)
                                            .Select(l => l.TenLop)
                                            .FirstOrDefault(),
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
                var studentDetails = await context.HocSinh
                    .Where(hs => hs.MaHocSinh == maHocSinh)
                    .Select(hs => new StudentDetailViewModel
                    {
                        MaHocSinh = hs.MaHocSinh,
                        Ho = hs.Ho,
                        TenDemvaTen = hs.TenDemvaTen,
                        NgaySinh = hs.NgaySinh,
                        DiaChi = hs.DiaChi,
                        // Thêm các thuộc tính khác của HocSinh cần lấy
                        PhanLop = context.PhanLop
                            .Where(pl => pl.MaHocSinh == maHocSinh)
                            .Join(context.PhanCong,
                                  pl => pl.MaPhanCong,
                                  pc => pc.MaPhanCong,
                                  (pl, pc) => new { PhanLop = pl, PhanCong = pc })
                            .Join(context.MonHoc,
                                  pc => pc.PhanCong.MaMonHoc,
                                  mh => mh.MaMonHoc,
                                  (pc, mh) => new TKBViewModel
                                  {
                                      MaPhanCong = pc.PhanCong.MaPhanCong,
                                      ThoiGianBatDau = pc.PhanCong.ThoiGianBatDau,
                                      ThoiGianKetThuc = pc.PhanCong.ThoiGianKetThuc,
                                      NgayHoc = pc.PhanCong.NgayHoc,
                                      TenMonHoc = mh.TenMonHoc,
                                      TenGiaoVien = context.GiaoVien
                                                      .Where(gv => gv.MaGiaoVien == pc.PhanCong.MaGiaoVien)
                                                      .Select(gv => gv.Ho + " " + gv.TenDemvaTen)
                                                      .FirstOrDefault(),
                                      TenLop = context.Lop
                                                    .Where(l => l.MaLop == pc.PhanCong.MaLop)
                                                    .Select(l => l.TenLop)
                                                    .FirstOrDefault(),
                                      // Các thuộc tính khác cần thiết
                                  })
                            .ToList()
                    })
                    .FirstOrDefaultAsync();

                return studentDetails;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw; // Xử lý lỗi tùy theo yêu cầu của bạn
            }
        }

        public async Task<bool> UpdateStudentDetailsAsync(string maHocSinh, StudentUpdateModel updatedStudentDetails)
        {
            try
            {
                var existingStudent = await context.HocSinh.FirstOrDefaultAsync(hs => hs.MaHocSinh == maHocSinh);

                if (existingStudent != null)
                {
                    // Cập nhật thông tin học sinh
                    existingStudent.Ho = updatedStudentDetails.Ho;
                    existingStudent.TenDemvaTen = updatedStudentDetails.TenDemvaTen;
                    existingStudent.DiaChi = updatedStudentDetails.DiaChi;
                    // Cập nhật các thuộc tính khác cần thiết

                    context.Entry(existingStudent).State = EntityState.Modified;
                    await context.SaveChangesAsync();

                    return true;
                }
                else
                {
                    return false; // Học sinh không tồn tại
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw; // Xử lý lỗi tùy theo yêu cầu của bạn
            }
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

        /*public async Task<bool> CollectTuitionFeeAsync(string maHocSinh, int maPhanCong, int maLoaiHocPhi, decimal mucThuPhi, decimal giamGia, string ghiChu)
        {
            try
            {
                var phanCong = await context.PhanCong
                    .Include(pc => pc.PhanLop)
                    .ThenInclude(pl => pl.HocPhi) // Đảm bảo thông tin liên quan được nạp
                    .FirstOrDefaultAsync(pc => pc.MaPhanCong == maPhanCong);

                if (phanCong != null)
                {
                    var hocSinh = phanCong.PhanLop.FirstOrDefault(pl => pl.MaHocSinh == maHocSinh);

                    if (hocSinh != null)
                    {
                        // Cập nhật trạng thái học phí trong bảng PhanLop
                        hocSinh.HocPhi.TrangThai = true; // Đánh dấu là đã thu học phí

                        // Thêm dữ liệu học phí vào bảng THUHOCPHI
                        var thuHocPhi = new THUHOCPHI
                        {
                            MaHocSinh = maHocSinh,
                            MaPhanCong = maPhanCong,
                            MaLoaiHocPhi = maLoaiHocPhi,
                            MucThuPhi = mucThuPhi,
                            GiamGia = giamGia,
                            GhiChu = ghiChu,
                            NgayThuPhi = DateTime.Now // Đặt ngày thu học phí là ngày hiện tại
                        };

                        context.ThuHocPhi.Add(thuHocPhi);

                        await context.SaveChangesAsync();

                        return true;
                    }
                    else
                    {
                        return false; // Học sinh không thuộc lớp học này
                    }
                }
                else
                {
                    return false; // Phân công không tồn tại
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw; // Xử lý lỗi tùy theo yêu cầu của bạn
            }
        }
*/









        /*
        
        public async Task<StudentDetailViewModel> GetStudentDetailAsync(int MaHocSinh)
        {
            var studentDetail = await context.HocSinh
               .Where(s => s.MaHocSinh == MaHocSinh)
               .Select(s => new StudentDetailViewModel
               {
                   MaHocSinh = s.MaHocSinh,
                   HoTen = s.HoTen,
                   GioiTinh = s.GioiTinh,
                   DiaChi = s.DiaChi,
                   Email = s.Email,
               })
               .FirstOrDefaultAsync();

            return studentDetail;
        }



        public async Task<bool> UpdateStudentDetailAsync(int MaHocSinh, StudentUpdateModel model)
        {
            try
            {
                var student = await context.HocSinh.FindAsync(MaHocSinh);

                if (student != null)
                {
                    // Cập nhật thông tin học sinh với dữ liệu từ model
                    student.HoTen = model.HoTen;
                    student.GioiTinh = model.GioiTinh;
                    student.DiaChi = model.DiaChi;
                    student.Email = model.Email;

                    // Lưu thay đổi vào cơ sở dữ liệu
                    await context.SaveChangesAsync();

                    // Cập nhật thông tin User
                    var user = await userManager.FindByIdAsync(student.UserId);
                    if (user != null)
                    {
                        user.FirstName = model.HoTen; // Sử dụng trường FirstName để lưu thông tin họ tên
                        user.Email = model.Email;

                        await userManager.UpdateAsync(user);
                    }

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
        public async Task<bool> DeleteStudentAsync(int MaHocSinh)
        {
            try
            {
                var student = await context.HocSinh.FindAsync(MaHocSinh);

                if (student != null)
                {
                    // Xóa các liên kết từ bảng PhanLop trước
                    var enrollments = context.PhanLop.Where(e => e.MaHocSinh == MaHocSinh);
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

        public async Task<bool> CollectFeeAsync(int studentId, int classId, string feeType, decimal amount)
        {
            try
            {
                // Xác định MaLoaiHocPhi dựa trên loại học phí (feeType)
                var feeTypeId = await context.LoaiHocPhi
                    .Where(l => l.TenLoaiHocPhi == feeType)
                    .Select(l => l.MaLoaiHocPhi)
                    .FirstOrDefaultAsync();

                if (feeTypeId == 0)
                {
                    // Nếu không tìm thấy loại học phí, bạn có thể thực hiện xử lý nếu cần
                    return false;
                }

                // Thêm học phí vào bảng LoaiHocPhi
                var feeRecord = new THUHOCPHI
                {
                    MaHocSinh = studentId,
                    MaLop = classId,
                    MaLoaiHocPhi = feeTypeId,
                    MucThuPhi = amount,
                    GhiChu = "Thanh toán học phí",
                    NgayThuPhi = DateTime.Now // Có thể cần điều chỉnh ngày thu phí tùy theo logic của bạn
                };

                // Cập nhật trạng thái học phí của học sinh trong bảng PhanLop
                var enrollment = await context.PhanLop
                    .Where(e => e.MaHocSinh == studentId && e.MaLop == classId)
                    .FirstOrDefaultAsync();

                if (enrollment != null)
                {
                    enrollment.TinhTrangHocPhi = true;
                    await context.SaveChangesAsync();
                }
                else
                {
                    return false;
                }

                context.ThuHocPhi.Add(feeRecord);
                await context.SaveChangesAsync();

                

                return true;
            }
            catch (Exception)
            {
                // Xử lý lỗi nếu có
                return false;
            }
        
    }
        */
    }
}
