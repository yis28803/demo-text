using Duanmaulan4.Controllers;
using Duanmaulan4.DataView.Authentication;
using Duanmaulan4.DataView.QuanlygiaovienModels;
using Duanmaulan4.DataView.QuanlyhocvienModels;
using Duanmaulan4.Helpers;
using Duanmaulan4.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Duanmaulan4.Services
{
    public class LecturerServices : ILecturerServices
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ApplicationDbContext context;
        private readonly ILogger<LecturersController> logger;

        public LecturerServices(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext context,
            ILogger<LecturersController> logger)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.context = context;
            this.logger = logger;
        }

        public async Task<List<LecturerViewModel>> GetLecturersAsync()
        {
            var lecturers = await context.GiaoVien
                .Include(gv => gv.User) // Include the User (AspNetUser) navigation property
                .Select(gv => new LecturerViewModel
                {
                    UserId = gv.UserId,
                    MaGiaoVien = gv.MaGiaoVien,
                    HinhAnh = gv.HinhAnh,
                    TenGiaoVien = gv.Ho + " " + gv.TenDemvaTen,
                    NgaySinh = gv.NgaySinh,
                    GioiTinh = gv.GioiTinh,
                    Email = gv.Email,
                    DienThoai = gv.DienThoai,
                    DiaChi = gv.DiaChi,
                    MaMonHoc = gv.MaMonHoc,
                    MonKiemNhiem = gv.MonKiemNhiem,
                    MatKhau = gv.MatKhau,
                    
                    // Thêm các trường khác nếu cần
                })
                .ToListAsync();

            return lecturers;
        }
        public async Task<List<LecturerViewModel>> GetLecturersAsync(string searchTerm)
        {
            var lecturers = await context.GiaoVien
                .Include(gv => gv.User) // Include the User (AspNetUser) navigation property
                .Where(gv =>
                    gv.MaGiaoVien.Contains(searchTerm) || // Tìm kiếm theo MaGiaoVien
                    (gv.Ho + " " + gv.TenDemvaTen).Contains(searchTerm) || // Tìm kiếm theo TenDemvaTen
                    gv.Email.Contains(searchTerm) || // Tìm kiếm theo Email
                    gv.DienThoai.Contains(searchTerm)) // Tìm kiếm theo DienThoai
                .Select(gv => new LecturerViewModel
                {
                    UserId = gv.UserId,
                    MaGiaoVien = gv.MaGiaoVien,
                    HinhAnh = gv.HinhAnh,
                    TenGiaoVien = gv.Ho + " " + gv.TenDemvaTen,
                    NgaySinh = gv.NgaySinh,
                    GioiTinh = gv.GioiTinh,
                    Email = gv.Email,
                    DienThoai = gv.DienThoai,
                    DiaChi = gv.DiaChi,
                    MaMonHoc = gv.MaMonHoc,
                    MonKiemNhiem = gv.MonKiemNhiem,
                    MatKhau = gv.MatKhau,
                    // Thêm các trường khác nếu cần
                })
                .ToListAsync();

            return lecturers;
        }

        public async Task<IdentityResult> SignUpLecturerAsync(SignUpModelGiaoVien model, int maMonHoc)
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
                if (!await roleManager.RoleExistsAsync(AppRole.Lecturer))
                {
                    await roleManager.CreateAsync(new IdentityRole(AppRole.Lecturer));
                }

                await userManager.AddToRoleAsync(user, AppRole.Lecturer);

                // Kiểm tra xem vai trò có phải là Student hay không
                var userRoles = await userManager.GetRolesAsync(user);
                if (userRoles.Contains(AppRole.Lecturer))
                {
                    // Thêm thông tin người dùng vào bảng Student
                    await AddLecturerInfoAsync(user.Id, model, maMonHoc);
                }
            }

            return result;
        }
        private async Task AddLecturerInfoAsync(string userId, SignUpModelGiaoVien model, int maMonHoc)
        {
            var teacher = new GIAOVIEN
            {
                MaGiaoVien = model.MaGiaoVien,
                UserId = userId,
                MaSoThue = model.MaSoThue,
                Ho = model.Ho,
                TenDemvaTen = model.TenDemvaTen,
                NgaySinh = model.NgaySinh,
                GioiTinh = model.GioiTinh,
                Email = model.Email,
                DienThoai = model.DienThoai,
                DiaChi = model.DiaChi,
                MaMonHoc = maMonHoc,
                MonKiemNhiem = model.MonKiemNhiem,
                MatKhau = model.Password, // Lưu mật khẩu cho giáo viên (lưu ý: cần xem xét cách lưu trữ mật khẩu an toàn)
                HinhAnh = model.HinhAnh
            };

            context.GiaoVien.Add(teacher);
            await context.SaveChangesAsync();

        }

        public async Task<bool> UpdateLecturerDetailAsync(string maGiaoVien, LecturerUpdateModel model)
        {
            try
            {
                // Lấy giáo viên từ mã giáo viên
                var lecturer = await context.GiaoVien
                    .Where(gv => gv.MaGiaoVien == maGiaoVien)
                    .FirstOrDefaultAsync();

                if (lecturer != null)
                {
                    // Cập nhật thông tin giáo viên
                    lecturer.MaSoThue = model.MaSoThue;
                    lecturer.Ho = model.Ho;
                    lecturer.TenDemvaTen = model.TenDemvaTen;
                    lecturer.NgaySinh = model.NgaySinh;
                    lecturer.GioiTinh = model.GioiTinh;
                    lecturer.Email = model.Email;
                    lecturer.DienThoai = model.DienThoai;
                    lecturer.DiaChi = model.DiaChi;
                    lecturer.MaMonHoc = model.MaMonHoc;
                    lecturer.MonKiemNhiem = model.MonKiemNhiem;
                    lecturer.HinhAnh = model.HinhAnh;

                    
                    // Lưu thay đổi vào cơ sở dữ liệu
                    await context.SaveChangesAsync();

                    return true; // Cập nhật thành công
                }

                return false; // Không tìm thấy giáo viên
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return false; // Xử lý lỗi nếu có
            }
        }

        public async Task<bool> DeleteLecturerAsync(string MaGiaoVien)
        {
            try
            {
                var lecturer = await context.GiaoVien.FindAsync(MaGiaoVien);

                if (lecturer != null)
                {
                    // Kiểm tra xem có PhanCong liên kết không
                    var phanCongRecords = context.PhanCong.Where(pc => pc.MaGiaoVien == MaGiaoVien);

                    // Xóa các bản ghi PhanCong trước
                    context.PhanCong.RemoveRange(phanCongRecords);

                    // Xóa thông tin giáo viên
                    context.GiaoVien.Remove(lecturer);

                    // Xóa thông tin User
                    var userId = lecturer.UserId;
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


        public async Task<List<PhanCongViewModel>> GetLecturerScheduleAsync(string maGiaoVien)
        {
            try
            {
                var teachingSchedule = await context.PhanCong
                    .Where(pc => pc.MaGiaoVien == maGiaoVien)
                    .Join(context.LichHoc,
                          pc => pc.MaPhanCong,
                          lh => lh.MaPhanCong,
                          (pc, lh) => new PhanCongViewModel
                          {
                              MaLichHoc = lh.MaLichHoc,
                              MaMonHoc = pc.MonHoc.MaMonHoc,
                              TenMonHoc = pc.MonHoc.TenMonHoc,
                              TenLop = pc.Lop.TenLop,
                              NgayBatDau = pc.NgayBatDau,
                              NgayKetThuc = pc.NgayKetThuc,
                              ThoiGianHoc = lh.ThoiGianHoc,
                              PhongHoc = lh.PhongHoc,
                              Thu = lh.Thu,
                              // Các thuộc tính khác cần thiết
                          })
                    .ToListAsync();

                return teachingSchedule;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw; // Xử lý lỗi tùy theo yêu cầu của bạn
            }
        }
        public async Task<List<PhanCongViewModel2>> GetAllLecturersScheduleAsync()
        {
            try
            {
                var teachingSchedule = await context.PhanCong
            .Join(context.LichHoc,
                  pc => pc.MaPhanCong,
                  lh => lh.MaPhanCong,
                  (pc, lh) => new PhanCongViewModel2
                  {
                      MaLichHoc = lh.MaLichHoc,
                      MaMonHoc = pc.MonHoc.MaMonHoc,
                      TenMonHoc = pc.MonHoc.TenMonHoc,
                      TenLop = pc.Lop.TenLop,
                      MaGiaoVien = pc.GiaoVien.MaGiaoVien,
                      TenGiaoVien = pc.GiaoVien.Ho + " " + pc.GiaoVien.TenDemvaTen,
                      NgayBatDau = pc.NgayBatDau,
                      NgayKetThuc = pc.NgayKetThuc,
                      ThoiGianHoc = lh.ThoiGianHoc,
                      PhongHoc = lh.PhongHoc,
                      Thu = lh.Thu,
                      // Các thuộc tính khác cần thiết
                  })
            .ToListAsync();

                return teachingSchedule;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw; // Xử lý lỗi tùy theo yêu cầu của bạn
            }


        }
        
        public async Task<bool> AddTeachingScheduleAsync(TeachingScheduleDTO scheduleDTO)
        {
            try
            {
                // Tạo mới bản ghi PhanCong
                var phanCong = new PHANCONG
                {
                    MaLop = scheduleDTO.MaLop,
                    MaMonHoc = scheduleDTO.MaMonHoc,
                    MaGiaoVien = scheduleDTO.MaGiaoVien,
                    NgayBatDau = scheduleDTO.NgayBatDau,
                    NgayKetThuc = scheduleDTO.NgayKetThuc,
                    ChotDiem = false,
                    ChotLuong = false
                };

                context.PhanCong.Add(phanCong);
                await context.SaveChangesAsync();

                // Lấy MaPhanCong của bản ghi PhanCong vừa thêm
                var maPhanCong = phanCong.MaPhanCong;

                // Tạo mới bản ghi LichHoc
                var lichHoc = new LICHHOC
                {
                    MaPhanCong = maPhanCong,
                    ThoiGianHoc = scheduleDTO.ThoiGianHoc,
                    PhongHoc = scheduleDTO.PhongHoc,
                    Thu = scheduleDTO.Thu
                };

                context.LichHoc.Add(lichHoc);
                await context.SaveChangesAsync();

                var existingClass = await context.Lop.FirstOrDefaultAsync(l => l.MaLop == scheduleDTO.MaLop);

                if (existingClass != null)
                {
                    existingClass.TrangThai = true;
                    await context.SaveChangesAsync();
                }

                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return false;
            }
        }
        public async Task<bool> AddTeachingScheduleDetailAsync(int maPhanCong, TeachingScheduleDetailDTO scheduleDetailDTO)
        {
            try
            {
                // Kiểm tra xem maPhanCong đã tồn tại chưa
                var existingPhanCong = await context.PhanCong.FirstOrDefaultAsync(pc => pc.MaPhanCong == maPhanCong);

                if (existingPhanCong == null)
                {
                    // Trả về false nếu maPhanCong không tồn tại
                    return false;
                }

                // Tạo mới bản ghi LichHoc
                var lichHoc = new LICHHOC
                {
                    MaPhanCong = maPhanCong,
                    ThoiGianHoc = scheduleDetailDTO.ThoiGianHoc,
                    PhongHoc = scheduleDetailDTO.PhongHoc,
                    Thu = scheduleDetailDTO.Thu
                };

                context.LichHoc.Add(lichHoc);
                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return false;
            }
        }
        public async Task<bool> UpdateTeachingScheduleDetailAsync(int maLichHoc, TeachingScheduleDetailDTO updatedScheduleDetailDTO)
        {
            try
            {
                // Kiểm tra xem maLichHoc đã tồn tại chưa
                var existingLichHoc = await context.LichHoc.FirstOrDefaultAsync(lh => lh.MaLichHoc == maLichHoc);

                if (existingLichHoc == null)
                {
                    // Trả về false nếu maLichHoc không tồn tại
                    return false;
                }

                // Cập nhật thông tin lịch học
                existingLichHoc.ThoiGianHoc = updatedScheduleDetailDTO.ThoiGianHoc;
                existingLichHoc.PhongHoc = updatedScheduleDetailDTO.PhongHoc;
                existingLichHoc.Thu = updatedScheduleDetailDTO.Thu;

                // Lưu thay đổi vào cơ sở dữ liệu
                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return false;
            }
        }



        public async Task<bool> UpdateTeachingScheduleAsync(int maPhanCong, TeachingScheduleUpdateDTO scheduleUpdateDTO)
        {
            try
            {
                var phanCong = await context.PhanCong.FindAsync(maPhanCong);

                if (phanCong != null)
                {
                    // Cập nhật thông tin lịch giảng dạy
                    phanCong.MaLop = scheduleUpdateDTO.MaLop;
                    phanCong.MaMonHoc = scheduleUpdateDTO.MaMonHoc;
                    phanCong.MaGiaoVien = scheduleUpdateDTO.MaGiaoVien;
                    phanCong.NgayBatDau = scheduleUpdateDTO.NgayBatDau;
                    phanCong.NgayKetThuc = scheduleUpdateDTO.NgayKetThuc;

                    // Lưu thay đổi vào cơ sở dữ liệu
                    await context.SaveChangesAsync();

                    // Cập nhật thông tin lịch học
                    var lichHoc = await context.LichHoc.FindAsync(phanCong.MaPhanCong);

                    if (lichHoc != null)
                    {
                        lichHoc.PhongHoc = scheduleUpdateDTO.PhongHoc;
                        lichHoc.Thu = scheduleUpdateDTO.Thu;
                        lichHoc.ThoiGianHoc = scheduleUpdateDTO.ThoiGianHoc;

                        // Lưu thay đổi vào cơ sở dữ liệu
                        await context.SaveChangesAsync();
                    }

                    return true; // Cập nhật thành công
                }

                return false; // Không tìm thấy lịch giảng dạy
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return false; // Xử lý lỗi nếu có
            }
        }

        public async Task<bool> DeleteTeachingScheduleDetailAsync(int maLichHoc)
        {
            try
            {
                // Kiểm tra xem maLichHoc đã tồn tại chưa
                var existingLichHoc = await context.LichHoc.FirstOrDefaultAsync(lh => lh.MaLichHoc == maLichHoc);

                if (existingLichHoc == null)
                {
                    // Trả về false nếu maLichHoc không tồn tại
                    return false;
                }

                // Xóa lịch học
                context.LichHoc.Remove(existingLichHoc);

                // Lưu thay đổi vào cơ sở dữ liệu
                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return false;
            }
        }



        public async Task<bool> DeleteTeachingScheduleAsync(int maPhanCong)
        {
            try
            {
                // Kiểm tra xem lịch giảng dạy có tồn tại hay không
                var scheduleToDelete = await context.PhanCong.FindAsync(maPhanCong);

                if (scheduleToDelete != null)
                {
                    // Xóa lịch giảng dạy
                    context.PhanCong.Remove(scheduleToDelete);
                    await context.SaveChangesAsync();

                    return true;
                }

                return false; // Lịch giảng dạy không tồn tại
            }
            catch (Exception)
            {
                // Xử lý lỗi nếu có
                return false;
            }
        }
    }
}
