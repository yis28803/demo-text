using Duanmaulan4.DataView.Authentication;
using Duanmaulan4.DataView.QuanlyhocvienModels;
using Microsoft.AspNetCore.Identity;

namespace Duanmaulan4.Services
{
    public interface IStudentServices
    {
        Task<List<StudentViewModel>> GetStudentsAsync();
        Task<IdentityResult> SignUpStudentAsync(SignUpModel model, int maLop);

        Task<List<ClassViewModel>> GetAllClassesAsync();

        Task<List<ClassViewModel>> GetClassesEnrolledByStudentAsync(string MaHocSinh);
        Task<bool> RegisterClassAsync(string maHocSinh, int maPhanCong);
        Task<bool> UnregisterClassAsync(string maHocSinh, int maPhanCong);
        Task<List<TKBViewModel>> GetStudentScheduleAsync(string maHocSinh);
        Task<StudentDetailViewModel> GetStudentDetailsAsync(string maHocSinh);
        Task<bool> UpdateStudentDetailsAsync(string maHocSinh, StudentUpdateModel updatedStudentDetails);
        Task<bool> DeleteStudentAsync(string maHocSinh);
        /*
       
      
        
       
        
        
        Task<bool> CollectFeeAsync(int studentId, int classId, string feeType, decimal amount);
        */
    }
}
