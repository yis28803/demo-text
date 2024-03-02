using Duanmaulan4.DataView.Authentication;
using Duanmaulan4.DataView.QuanlygiaovienModels;
using Duanmaulan4.DataView.QuanlyhocvienModels;
using Microsoft.AspNetCore.Identity;

namespace Duanmaulan4.Services
{
    public interface ILecturerServices
    {
        Task<List<LecturerViewModel>> GetLecturersAsync();
        Task<List<LecturerViewModel>> GetLecturersAsync(string searchTerm);
        Task<IdentityResult> SignUpLecturerAsync(SignUpModelGiaoVien model, int maMonHoc);
        Task<bool> UpdateLecturerDetailAsync(string MaGiaoVien, LecturerUpdateModel model);
        Task<bool> DeleteLecturerAsync(string MaGiaoVien);
        Task<List<PhanCongViewModel>> GetLecturerScheduleAsync(string maGiaoVien);
        Task<List<PhanCongViewModel2>> GetAllLecturersScheduleAsync();
        Task<bool> AddTeachingScheduleAsync(TeachingScheduleDTO scheduleDTO);
        Task<bool> AddTeachingScheduleDetailAsync(int maPhanCong, TeachingScheduleDetailDTO scheduleDetailDTO);
        Task<bool> UpdateTeachingScheduleDetailAsync(int maLichHoc, TeachingScheduleDetailDTO updatedScheduleDetailDTO);

        Task<bool> UpdateTeachingScheduleAsync(int maPhanCong, TeachingScheduleUpdateDTO scheduleUpdateDTO);
        Task<bool> DeleteTeachingScheduleDetailAsync(int maLichHoc);
        Task<bool> DeleteTeachingScheduleAsync(int maPhanCong);
    }
}
