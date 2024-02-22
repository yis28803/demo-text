using Duanmaulan4.DataView.Authentication;
using Duanmaulan4.DataView.QuanlygiaovienModels;
using Duanmaulan4.Services;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost("signuplecturer")]
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
/*
        [HttpPut("{MaGiaoVien}")]
        public async Task<IActionResult> UpdateLecturerDetail(int MaGiaoVien, [FromBody] LecturerUpdateModel model)
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
        public async Task<IActionResult> DeleteLecturer(int MaGiaoVien)
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
        public async Task<IActionResult> GetLecturerSchedule(int maGiaoVien)
        {
            var lecturerSchedule = await _lecturerServices.GetLecturerScheduleAsync(maGiaoVien);
            return Ok(lecturerSchedule);
        }

        [HttpGet("lecturers/schedules")]
        public async Task<ActionResult<List<PhanCongViewModel>>> GetLecturerSchedules()
        {
            var schedules = await _lecturerServices.GetLecturerSchedulesAsync();
            return Ok(schedules);
        }

        [HttpPost("add-schedule/{maGiaoVien}")]
        public async Task<IActionResult> AddLecturerSchedule(int maGiaoVien, [FromBody] LecturerScheduleModel model)
        {
            var result = await _lecturerServices.AddLecturerScheduleAsync(maGiaoVien, model);

            if (result)
            {
                return Ok(new { Message = "Lịch giảng dạy đã được thêm." });
            }
            else
            {
                return BadRequest(new { Message = "Không thể thêm lịch giảng dạy." });
            }
        }

        [HttpPost("add-lecturers-schedule")]
        public async Task<IActionResult> AddLecturersSchedule([FromBody] LecturersScheduleModel model)
        {
            try
            {
                var result = await _lecturerServices.AddLecturersScheduleAsync(model);

                if (result)
                {
                    return Ok(new { Message = "Lịch giảng dạy đã được thêm." });
                }
                else
                {
                    return BadRequest(new { Message = "Không thể thêm lịch giảng dạy." });
                }
            }
            catch (Exception)
            {
                // Xử lý lỗi nếu có
                return StatusCode(500, new { Error = "Có lỗi xảy ra" });
            }
        }

        [HttpPut("update-lecturer-schedule/{maPhanCong}")]
        public async Task<IActionResult> UpdateLecturerSchedule(int maPhanCong, [FromBody] LecturersScheduleModel model)
        {
            try
            {
                var result = await _lecturerServices.UpdateLecturerScheduleAsync(maPhanCong, model);

                if (result)
                {
                    return Ok(new { Message = "Cập nhật lịch giảng dạy thành công." });
                }
                else
                {
                    return BadRequest(new { Message = "Không thể cập nhật lịch giảng dạy." });
                }
            }
            catch (Exception)
            {
                // Xử lý lỗi nếu có
                return StatusCode(500, new { Error = "Có lỗi xảy ra" });
            }
        }


        [HttpDelete("delete-lecturer-schedule/{maPhanCong}")]
        public async Task<IActionResult> DeleteLecturerSchedule(int maPhanCong)
        {
            try
            {
                var result = await _lecturerServices.DeleteLecturerScheduleAsync(maPhanCong);

                if (result)
                {
                    return Ok(new { Message = "Xoá lịch giảng dạy thành công." });
                }
                else
                {
                    return BadRequest(new { Message = "Không thể xoá lịch giảng dạy." });
                }
            }
            catch (Exception)
            {
                // Xử lý lỗi nếu có
                return StatusCode(500, new { Error = "Có lỗi xảy ra" });
            }
        }
*/

    }
}
