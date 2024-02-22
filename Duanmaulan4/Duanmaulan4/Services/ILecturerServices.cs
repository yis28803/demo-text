using Duanmaulan4.DataView.Authentication;
using Duanmaulan4.DataView.QuanlygiaovienModels;
using Duanmaulan4.DataView.QuanlyhocvienModels;
using Microsoft.AspNetCore.Identity;

namespace Duanmaulan4.Services
{
    public interface ILecturerServices
    {
        Task<List<LecturerViewModel>> GetLecturersAsync();
        Task<IdentityResult> SignUpLecturerAsync(SignUpModelGiaoVien model, int maMonHoc);
       /* Task<bool> UpdateLecturerDetailAsync(int MaGiaoVien, LecturerUpdateModel model);
        Task<bool> DeleteLecturerAsync(int MaGiaoVien);
        Task<List<PhanCongViewModel>> GetLecturerScheduleAsync(int maGiaoVien);
        Task<List<LecturerScheduleViewModel>> GetLecturerSchedulesAsync();
        Task<bool> AddLecturerScheduleAsync(int maGiaoVien, LecturerScheduleModel model);
        Task<bool> AddLecturersScheduleAsync(LecturersScheduleModel model);
        Task<bool> UpdateLecturerScheduleAsync(int maPhanCong, LecturersScheduleModel model);
        Task<bool> DeleteLecturerScheduleAsync(int maPhanCong);*/





        // Thêm các phương thức khác nếu cần
    }
}
