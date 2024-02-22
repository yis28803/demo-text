using Duanmaulan4.DataView.Authentication;
using Duanmaulan4.DataView.QuanlygiaovienModels;
using Duanmaulan4.DataView.QuanlyhocvienModels;
using Duanmaulan4.Helpers;
using Duanmaulan4.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Duanmaulan4.Services
{
    public class LecturerServices : ILecturerServices
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ApplicationDbContext context;

        public LecturerServices(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext context)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.context = context;
        }

        public async Task<List<LecturerViewModel>> GetLecturersAsync()
        {
            var lecturers = await context.GiaoVien
                .Include(gv => gv.User) // Include the User (AspNetUser) navigation property
                .Select(gv => new LecturerViewModel
                {
                    MaGiaoVien = gv.MaGiaoVien,
                    UserId = gv.UserId,
                    TenGiaoVien = gv.Ho + " " + gv.TenDemvaTen,
                    NgaySinh = gv.NgaySinh,
                    GioiTinh = gv.GioiTinh,
                    Email = gv.Email,
                    DienThoai = gv.DienThoai,
                    DiaChi = gv.DiaChi,
                    MaMonHoc = gv.MaMonHoc,
                    MonKiemNhiem = gv.MonKiemNhiem,
                    MatKhau = gv.MatKhau,
                    HinhAnh = gv.HinhAnh,
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

            /*var phanCong = new PHANCONG
            {
                MaGiaoVien = teacher.MaGiaoVien, 
            };

            context.PhanCong.Add(phanCong);
            await context.SaveChangesAsync();*/
        }




        /*
        public async Task<bool> UpdateLecturerDetailAsync(int MaGiaoVien, LecturerUpdateModel model)
        {
            try
            {
                var lecturer = await context.GiaoVien.FindAsync(MaGiaoVien);

                if (lecturer != null)
                {
                    // Cập nhật thông tin học sinh với dữ liệu từ model
                    lecturer.TenGiaoVien = model.TenGiaoVien;
                    lecturer.Diachi = model.Diachi;
                    lecturer.Dienthoai = model.Dienthoai;

                    // Lưu thay đổi vào cơ sở dữ liệu
                    await context.SaveChangesAsync();

                    // Cập nhật thông tin User
                    var user = await userManager.FindByIdAsync(lecturer.UserId);
                    if (user != null)
                    {
                        user.FirstName = model.TenGiaoVien;
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
        public async Task<bool> DeleteLecturerAsync(int MaGiaoVien)
        {
            try
            {
                var lecturer = await context.GiaoVien.FindAsync(MaGiaoVien);

                if (lecturer != null)
                {
                    
                    // Xóa thông tin học sinh
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
        public async Task<List<PhanCongViewModel>> GetLecturerScheduleAsync(int maGiaoVien)
        {
            var lecturerSchedule = await context.PhanCong
                .Include(pc => pc.NamHoc)
                .Include(pc => pc.Lop)
                .Include(pc => pc.MonHoc)
                .Where(pc => pc.MaGiaoVien == maGiaoVien)
                .Select(pc => new PhanCongViewModel
                {
                    TenNamHoc = pc.NamHoc != null ? pc.NamHoc.TenNamHoc : null,
                    TenLop = pc.Lop != null ? pc.Lop.TenLop : null,
                    TenMonHoc = pc.MonHoc != null ? pc.MonHoc.TenMonHoc : null,
                    ThoiGian = pc.ThoiGian,
                    Ngay = pc.Ngay
                    // Các trường khác nếu cần
                })
                .ToListAsync();

            return lecturerSchedule;
        }

        public async Task<List<LecturerScheduleViewModel>> GetLecturerSchedulesAsync()
        {
            var schedules = await context.PhanCong
                .Include(p => p.GiaoVien)
                .Include(p => p.MonHoc)
                .Include(p => p.Lop)
                .Include(p => p.NamHoc)
                .Select(p => new LecturerScheduleViewModel
                {
                    TenNamHoc = p.NamHoc != null ? p.NamHoc.TenNamHoc : null,
                    TenLop = p.Lop != null ? p.Lop.TenLop : null,
                    TenMonHoc = p.MonHoc != null ? p.MonHoc.TenMonHoc : null,
                    TenGiaoVien = p.GiaoVien != null ? p.GiaoVien.TenGiaoVien : null,
                    ThoiGian = p.ThoiGian,
                    Ngay = p.Ngay,
                    // Các trường khác nếu cần
                })
                .ToListAsync();

            return schedules;
        }
        public async Task<bool> AddLecturerScheduleAsync(int maGiaoVien, LecturerScheduleModel model)
        {
            try
            {
                // Kiểm tra xem giáo viên có tồn tại hay không
                var lecturer = await context.GiaoVien.FindAsync(maGiaoVien);
                if (lecturer == null)
                {
                    return false; // Giáo viên không tồn tại
                }

                // Kiểm tra xem lịch giảng dạy đã tồn tại hay chưa
                var existingSchedule = await context.PhanCong
                    .Where(pc => pc.MaGiaoVien == maGiaoVien && pc.ThoiGian == model.ThoiGian && pc.Ngay == model.Ngay)
                    .FirstOrDefaultAsync();

                if (existingSchedule != null)
                {
                    return false; // Lịch giảng dạy đã tồn tại
                }

                // Tạo mới lịch giảng dạy
                var newSchedule = new PHANCONG
                {
                    MaGiaoVien = maGiaoVien,
                    MaNamHoc = model.MaNamHoc,
                    MaLop = model.MaLop,
                    MaMonHoc = model.MaMonHoc,
                    ThoiGian = model.ThoiGian,
                    Ngay = model.Ngay
                    // Các trường khác nếu cần
                };

                // Thêm vào cơ sở dữ liệu
                context.PhanCong.Add(newSchedule);
                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                // Xử lý lỗi nếu có
                return false;
            }
        }



        public async Task<bool> AddLecturersScheduleAsync(LecturersScheduleModel model)
        {
            try
            {
                if (await context.PhanCong
                    .AnyAsync(pc => pc.MaGiaoVien == model.MaGiaoVien && pc.ThoiGian == model.ThoiGian && pc.Ngay == model.Ngay))
                    return false;

                var newSchedule = new PHANCONG
                {
                    MaGiaoVien = model.MaGiaoVien,
                    MaNamHoc = model.MaNamHoc,
                    MaLop = model.MaLop,
                    MaMonHoc = model.MaMonHoc,
                    ThoiGian = model.ThoiGian,
                    Ngay = model.Ngay
                };

                context.PhanCong.Add(newSchedule);
                await context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateLecturerScheduleAsync(int maPhanCong, LecturersScheduleModel model)
        {
            try
            {
                // Kiểm tra xem phân công có tồn tại hay không
                var existingSchedule = await context.PhanCong.FindAsync(maPhanCong);

                if (existingSchedule == null)
                {
                    return false; // Lịch giảng dạy không tồn tại
                }

                // Kiểm tra xem lịch giảng dạy đã tồn tại hay chưa
                var isExistingSchedule = await context.PhanCong
                    .AnyAsync(pc => pc.ThoiGian == model.ThoiGian && pc.Ngay == model.Ngay && pc.STT != maPhanCong);

                if (isExistingSchedule)
                {
                    return false; // Lịch giảng dạy đã tồn tại
                }

                // Cập nhật thông tin lịch giảng dạy
                existingSchedule.MaNamHoc = model.MaNamHoc;
                existingSchedule.MaLop = model.MaLop;
                existingSchedule.MaMonHoc = model.MaMonHoc;
                existingSchedule.MaGiaoVien = model.MaGiaoVien;
                existingSchedule.ThoiGian = model.ThoiGian;
                existingSchedule.Ngay = model.Ngay;

                // Lưu thay đổi vào cơ sở dữ liệu
                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                // Xử lý lỗi nếu có
                return false;
            }
        }

        public async Task<bool> DeleteLecturerScheduleAsync(int maPhanCong)
        {
            try
            {
                var schedule = await context.PhanCong.FindAsync(maPhanCong);

                if (schedule != null)
                {
                    context.PhanCong.Remove(schedule);
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


*/

    }
}
