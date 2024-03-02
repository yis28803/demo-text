using Duanmaulan4.DataView.Authentication;
using Duanmaulan4.DataView.QuanlyhocvienModels;
using Duanmaulan4.Models;
using Microsoft.AspNetCore.Identity;

namespace Duanmaulan4.Services
{
    public interface IStudentServices
    {
        Task<List<StudentViewModel>> GetStudentsAsync();
        Task<IdentityResult> SignUpStudentAsync(SignUpModel model, int maLop);
        Task<List<StudentViewModel>> GetStudentsAsync(string searchKeyword);
        Task<List<ClassViewModel>> GetAllClassesAsync();

        Task<List<ClassViewModel>> GetClassesEnrolledByStudentAsync(string MaHocSinh);
        Task<List<ClassViewModel>> GetClassesEnrolledByStudentAsync(string MaHocSinh, string tenLopSearch);
        Task<bool> RegisterClassAsync(string maHocSinh, int maPhanCong);
        Task<bool> UnregisterClassAsync(string maHocSinh, int maPhanCong);
        Task<List<TKBViewModel>> GetStudentScheduleAsync(string maHocSinh);
        Task<StudentDetailViewModel> GetStudentDetailsAsync(string maHocSinh);
        Task<IdentityResult> UpdateStudentAsync(string maHocSinh, string ho, string tenDemvaTen,
             DateTime ngaySinh, bool gioiTinh, string email, string dienThoai, string diaChi, string hoTenPhuHuynh, string hinhAnh, int? maPhanCong);
        Task<PHANLOP> GetStudentClassAsync(string maHocSinh);
        Task<bool> DeleteStudentAsync(string maHocSinh);
        Task<List<StudentViewModel>> GetNewStudentsAsync();
        Task<bool> ThuHocPhiAsync(string maHocSinh, int maLop, int maLoaiHocPhi, decimal mucThuPhi, decimal giamGia, string ghiChu);
        

    }
}
