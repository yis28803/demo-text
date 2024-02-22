using Duanmaulan4.DataView.Authentication;
using Duanmaulan4.DataView.QuanlyhocvienModels;
using Duanmaulan4.Models;
using Duanmaulan4.Services;
using Microsoft.AspNetCore.Mvc;

namespace Duanmaulan4.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentServices _studentServices;
        private readonly IAccountServices accountRepository;
        private readonly ILogger<AccountsController> logger;

        public StudentsController(IAccountServices accountRepository,
            ILogger<AccountsController> logger,
            IStudentServices studentServices)
        {
            this.accountRepository = accountRepository;
            this.logger = logger;
            _studentServices = studentServices;
        }

        [HttpGet]
        public async Task<ActionResult<List<StudentViewModel>>> GetStudents()
        {
            var students = await _studentServices.GetStudentsAsync();
            return Ok(students);
        }

        [HttpPost("signuphocvien")]
        public async Task<IActionResult> SignUpHocVien([FromBody] SignUpModel model, int MaPhanCong)
        {
            try
            {
                var result = await _studentServices.SignUpStudentAsync(model, MaPhanCong);

                if (result.Succeeded)
                {
                    return Ok(new { Message = "Đăng ký học sinh thành công" });
                }
                else
                {
                    return BadRequest(new { Error = "Đăng ký học sinh thất tại", Errors = result.Errors });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(500, new { Error = "Có lỗi xảy ra" });
            }
        }

        
        [HttpGet("getallclasses")]
        public async Task<ActionResult<List<ClassViewModel>>> GetAllClasses()
        {
            var classes = await _studentServices.GetAllClassesAsync();
            return Ok(classes);
        }


        [HttpGet("enrolled-classes/{MaHocSinh}")]
        public async Task<IActionResult> GetEnrolledClasses(string MaHocSinh)
        {
            var enrolledClasses = await _studentServices.GetClassesEnrolledByStudentAsync(MaHocSinh);
            return Ok(enrolledClasses);
        }

        [HttpPost("registerclass")]
        public async Task<IActionResult> RegisterClass([FromBody] EnrollStudentInClassModel model)
        {
            try
            {
                var result = await _studentServices.RegisterClassAsync(model.MaHocSinh, model.MaPhanCong);

                if (result)
                {
                    return Ok(new { Message = "Đăng ký lớp học thành công" });
                }
                else
                {
                    return BadRequest(new { Error = "Học sinh đã đăng ký lớp này" });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(500, new { Error = "Có lỗi xảy ra" });
            }
        }

        [HttpPost("unregisterclass")]
        public async Task<IActionResult> UnregisterClass([FromBody] UnenrollStudentInClassModel model)
        {
            try
            {
                var result = await _studentServices.UnregisterClassAsync(model.MaHocSinh, model.MaPhanCong);

                if (result)
                {
                    return Ok(new { Message = "Hủy đăng ký lớp học thành công" });
                }
                else
                {
                    return BadRequest(new { Error = "Học sinh chưa đăng ký lớp này" });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(500, new { Error = "Có lỗi xảy ra" });
            }
        }

        [HttpGet("studentschedule/{maHocSinh}")]
        public async Task<ActionResult<List<TKBViewModel>>> GetStudentSchedule(string maHocSinh)
        {
            try
            {
                var studentSchedule = await _studentServices.GetStudentScheduleAsync(maHocSinh);
                return Ok(studentSchedule);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(500, new { Error = "Có lỗi xảy ra" });
            }
        }

        [HttpGet("studentdetails/{maHocSinh}")]
        public async Task<ActionResult<StudentDetailViewModel>> GetStudentDetails(string maHocSinh)
        {
            try
            {
                var studentDetails = await _studentServices.GetStudentDetailsAsync(maHocSinh);
                return Ok(studentDetails);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(500, new { Error = "Có lỗi xảy ra" });
            }
        }


        [HttpPut("updatestudentdetails/{maHocSinh}")]
        public async Task<ActionResult> UpdateStudentDetails(string maHocSinh, [FromBody] StudentUpdateModel updatedStudentDetails)
        {
            try
            {
                var result = await _studentServices.UpdateStudentDetailsAsync(maHocSinh, updatedStudentDetails);

                if (result)
                {
                    return Ok(new { Message = "Cập nhật thông tin học sinh thành công" });
                }
                else
                {
                    return BadRequest(new { Error = "Học sinh không tồn tại" });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(500, new { Error = "Có lỗi xảy ra" });
            }
        }
        

        [HttpDelete("deletestudent/{MaHocSinh}")]
        public async Task<IActionResult> DeleteStudent(string MaHocSinh)
        {
            try
            {
                var result = await _studentServices.DeleteStudentAsync(MaHocSinh);

                if (result)
                {
                    return Ok(new { Message = "Xóa thông tin học sinh thành công" });
                }
                else
                {
                    return BadRequest(new { Error = "Xóa thông tin học sinh thất bại. Học sinh không tồn tại." });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(500, new { Error = "Có lỗi xảy ra" });
            }
        }





        /*

        



       

       

        [HttpPut("updatestudentdetail/{MaHocSinh}")]
        public async Task<IActionResult> UpdateStudentDetail(int MaHocSinh, [FromBody] StudentUpdateModel model)
        {
            try
            {
                var result = await _studentServices.UpdateStudentDetailAsync(MaHocSinh, model);

                if (result)
                {
                    return Ok(new { Message = "Cập nhật thông tin học sinh thành công" });
                }
                else
                {
                    return BadRequest(new { Error = "Cập nhật thông tin học sinh thất bại. Học sinh không tồn tại." });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(500, new { Error = "Có lỗi xảy ra" });
            }
        }



       

        [HttpPost("collectfee")]
        public async Task<IActionResult> CollectFee([FromBody] FeeRequestModel model)
        {
            try
            {
                var result = await _studentServices.CollectFeeAsync(model.StudentId, model.ClassId, model.FeeType, model.Amount);

                if (result)
                {
                    return Ok(new { Message = "Thu học phí thành công" });
                }
                else
                {
                    return BadRequest(new { Error = "Thu học phí thất bại" });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(500, new { Error = "Có lỗi xảy ra" });
            }
        }*/





    }
}
