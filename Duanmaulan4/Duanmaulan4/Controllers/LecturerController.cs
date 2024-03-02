using Duanmaulan4.DataView.Authentication;
using Duanmaulan4.DataView.QuanlygiaovienModels;
using Duanmaulan4.Helpers;
using Duanmaulan4.Services;
using Duanmaulan4.Services.PhanQuyen;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Duanmaulan4.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LecturersController : ControllerBase
    {
        private readonly ILecturerServices _lecturerServices;
        private readonly ILogger<LecturersController> _logger;

        public LecturersController(ILecturerServices lecturerServices, ILogger<LecturersController> logger)
        {
            _lecturerServices = lecturerServices ?? throw new ArgumentNullException(nameof(lecturerServices));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [Authorize(Roles = PhanQuyenViewModel.Role_Admin + "," + PhanQuyenViewModel.Role_Accounting + "," + PhanQuyenViewModel.Role_Director + "," + PhanQuyenViewModel.Role_Registration)]
        [CustomAuthorization(PhanQuyenViewModel.Claim_Viewalllecturerlists)]
        public async Task<ActionResult<List<LecturerViewModel>>> GetLecturers()
        {
            try
            {
                var lecturers = await _lecturerServices.GetLecturersAsync();
                return Ok(lecturers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, new { Error = "Có lỗi xảy ra" });
            }
        }
        [HttpGet("search")]
        [Authorize(Roles = PhanQuyenViewModel.Role_Admin + "," + PhanQuyenViewModel.Role_Accounting + "," + PhanQuyenViewModel.Role_Director + "," + PhanQuyenViewModel.Role_Registration)]
        [CustomAuthorization(PhanQuyenViewModel.Claim_Viewalllecturerlists)]
        public async Task<ActionResult<List<LecturerViewModel>>> GetLecturersAsync(string searchTerm)
        {
            try
            {
                var lecturers = await _lecturerServices.GetLecturersAsync(searchTerm);
                return Ok(lecturers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, new { Error = "Có lỗi xảy ra" });
            }
        }


        [HttpPost("signuplecturer")]
        [Authorize(Roles = PhanQuyenViewModel.Role_Admin + "," + PhanQuyenViewModel.Role_Accounting + "," + PhanQuyenViewModel.Role_Director + "," + PhanQuyenViewModel.Role_Registration)]
        [CustomAuthorization(PhanQuyenViewModel.Claim_Adddeleteeditlecturers)]
        public async Task<IActionResult> SignUpLecturer([FromBody] SignUpModelGiaoVien model, int maMonHoc)
        {
            var result = await _lecturerServices.SignUpLecturerAsync(model, maMonHoc);

            if (result.Succeeded)
            {
                return Ok(new { Message = "Đăng ký giáo viên thành công" });
            }
            else
            {
                return BadRequest(new { Error = "Đăng ký giáo viên thất bại", Errors = result.Errors });
            }
        }

        [HttpPut("{MaGiaoVien}")]
        [Authorize(Roles = PhanQuyenViewModel.Role_Admin + "," + PhanQuyenViewModel.Role_Accounting + "," + PhanQuyenViewModel.Role_Director + "," + PhanQuyenViewModel.Role_Registration)]
        [CustomAuthorization(PhanQuyenViewModel.Claim_Adddeleteeditlecturers)]
        public async Task<IActionResult> UpdateLecturerDetail(string MaGiaoVien, [FromBody] LecturerUpdateModel model)
        {
            try
            {
                var result = await _lecturerServices.UpdateLecturerDetailAsync(MaGiaoVien, model);

                if (result)
                {
                    return Ok(new { Message = "Cập nhật thông tin giáo viên thành công" });
                }
                else
                {
                    return NotFound(new { Error = "Không tìm thấy giáo viên hoặc cập nhật thất bại" });
                }
            }
            catch (Exception)
            {
                // Xử lý lỗi nếu có
                return StatusCode(500, new { Error = "Có lỗi xảy ra" });
            }
        }

        [HttpDelete("{MaGiaoVien}")]
        [Authorize(Roles = PhanQuyenViewModel.Role_Admin + "," + PhanQuyenViewModel.Role_Accounting + "," + PhanQuyenViewModel.Role_Director + "," + PhanQuyenViewModel.Role_Registration)]
        [CustomAuthorization(PhanQuyenViewModel.Claim_Adddeleteeditlecturers)]
        public async Task<IActionResult> DeleteLecturer(string MaGiaoVien)
        {
            var result = await _lecturerServices.DeleteLecturerAsync(MaGiaoVien);

            if (result)
            {
                return Ok(new { Message = "Xóa giáo viên thành công" });
            }
            else
            {
                return BadRequest(new { Error = "Xóa giáo viên thất bại" });
            }
        }
        [HttpGet("lecturer-schedule/{maGiaoVien}")]
        [Authorize(Roles = PhanQuyenViewModel.Role_Admin + "," + PhanQuyenViewModel.Role_Lecturer + "," + PhanQuyenViewModel.Role_Accounting + "," + PhanQuyenViewModel.Role_Director + "," + PhanQuyenViewModel.Role_Registration)]
        [CustomAuthorization(PhanQuyenViewModel.Claim_Viewteachingschedule)]
        public async Task<IActionResult> GetLecturerSchedule(string maGiaoVien)
        {
            var lecturerSchedule = await _lecturerServices.GetLecturerScheduleAsync(maGiaoVien);
            return Ok(lecturerSchedule);
        }
        [HttpGet("lecturers/schedules")]
        [Authorize(Roles = PhanQuyenViewModel.Role_Admin + "," + PhanQuyenViewModel.Role_Lecturer + "," + PhanQuyenViewModel.Role_Accounting + "," + PhanQuyenViewModel.Role_Director + "," + PhanQuyenViewModel.Role_Registration)]
        [CustomAuthorization(PhanQuyenViewModel.Claim_Viewteachingschedule)]
        public async Task<ActionResult<List<PhanCongViewModel2>>> GetLecturerSchedules()
        {
            var schedules = await _lecturerServices.GetAllLecturersScheduleAsync();
            return Ok(schedules);
        }

        [HttpPost("add-teaching-schedule")]
        [Authorize(Roles = PhanQuyenViewModel.Role_Admin + "," + PhanQuyenViewModel.Role_Accounting + "," + PhanQuyenViewModel.Role_Director + "," + PhanQuyenViewModel.Role_Registration)]
        [CustomAuthorization(PhanQuyenViewModel.Claim_Adddeleteeditteachingschedule)]
        public async Task<IActionResult> AddTeachingSchedule([FromBody] TeachingScheduleDTO scheduleDTO)
        {
            var result = await _lecturerServices.AddTeachingScheduleAsync(scheduleDTO);

            if (result)
            {
                return Ok("Lịch giảng dạy đã được thêm.");
            }

            return BadRequest("Không thể thêm lịch giảng dạy.");
        }

        [HttpPost("add-teaching-schedule-detail/{maPhanCong}")]
        [Authorize(Roles = PhanQuyenViewModel.Role_Admin + "," + PhanQuyenViewModel.Role_Accounting + "," + PhanQuyenViewModel.Role_Director + "," + PhanQuyenViewModel.Role_Registration)]
        [CustomAuthorization(PhanQuyenViewModel.Claim_Adddeleteeditteachingschedule)]
        public async Task<IActionResult> AddTeachingScheduleDetail(int maPhanCong, [FromBody] TeachingScheduleDetailDTO scheduleDetailDTO)
        {
            var success = await _lecturerServices.AddTeachingScheduleDetailAsync(maPhanCong, scheduleDetailDTO);

            if (success)
            {
                return Ok(new { Message = "Lịch học đã được thêm mới thành công." });
            }
            else
            {
                return BadRequest(new { Message = "Không thể thêm mới lịch học. MaPhanCong không hợp lệ." });
            }
        }
        [HttpPut("updateTeachingScheduleDetail/{maLichHoc}")]
        [Authorize(Roles = PhanQuyenViewModel.Role_Admin + "," + PhanQuyenViewModel.Role_Accounting + "," + PhanQuyenViewModel.Role_Director + "," + PhanQuyenViewModel.Role_Registration)]
        [CustomAuthorization(PhanQuyenViewModel.Claim_Adddeleteeditteachingschedule)]
        public async Task<IActionResult> UpdateTeachingScheduleDetail(int maLichHoc, [FromBody] TeachingScheduleDetailDTO updatedScheduleDetailDTO)
        {
            var success = await _lecturerServices.UpdateTeachingScheduleDetailAsync(maLichHoc, updatedScheduleDetailDTO);

            if (success)
            {
                return Ok(new { Message = "Cập nhật lịch học thành công." });
            }
            else
            {
                return BadRequest(new { Message = "Không thể cập nhật lịch học. MaLichHoc không hợp lệ." });
            }
        }



        [HttpPut("update-teaching-schedule")]
        [Authorize(Roles = PhanQuyenViewModel.Role_Admin + "," + PhanQuyenViewModel.Role_Accounting + "," + PhanQuyenViewModel.Role_Director + "," + PhanQuyenViewModel.Role_Registration)]
        [CustomAuthorization(PhanQuyenViewModel.Claim_Adddeleteeditteachingschedule)]
        public async Task<IActionResult> UpdateTeachingScheduleAsync(int maPhanCong, [FromBody] TeachingScheduleUpdateDTO scheduleUpdateDTO)
        {
            var result = await _lecturerServices.UpdateTeachingScheduleAsync(maPhanCong, scheduleUpdateDTO);

            if (result)
            {
                return Ok("Lịch giảng dạy đã được cập nhật.");
            }

            return BadRequest("Không thể cập nhật lịch giảng dạy.");
        }
        [HttpDelete("deleteTeachingScheduleDetail/{maLichHoc}")]
        [Authorize(Roles = PhanQuyenViewModel.Role_Admin + "," + PhanQuyenViewModel.Role_Accounting + "," + PhanQuyenViewModel.Role_Director + "," + PhanQuyenViewModel.Role_Registration)]
        [CustomAuthorization(PhanQuyenViewModel.Claim_Adddeleteeditteachingschedule)]
        public async Task<IActionResult> DeleteTeachingScheduleDetail(int maLichHoc)
        {
            var success = await _lecturerServices.DeleteTeachingScheduleDetailAsync(maLichHoc);

            if (success)
            {
                return Ok(new { Message = "Xóa lịch học thành công." });
            }
            else
            {
                return BadRequest(new { Message = "Không thể xóa lịch học. MaLichHoc không hợp lệ." });
            }
        }

        [HttpDelete("delete-teaching-schedule")]
        [Authorize(Roles = PhanQuyenViewModel.Role_Admin + "," + PhanQuyenViewModel.Role_Accounting + "," + PhanQuyenViewModel.Role_Director + "," + PhanQuyenViewModel.Role_Registration)]
        [CustomAuthorization(PhanQuyenViewModel.Claim_Adddeleteeditteachingschedule)]
        public async Task<IActionResult> DeleteTeachingSchedule(int maPhanCong)
        {
            var result = await _lecturerServices.DeleteTeachingScheduleAsync(maPhanCong);

            if (result)
            {
                return Ok("Lịch giảng dạy đã được xóa.");
            }

            return BadRequest("Không thể xóa lịch giảng dạy.");
        }
    }
}
