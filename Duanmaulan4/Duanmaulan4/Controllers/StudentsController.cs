using Duanmaulan4.DataView.Authentication;
using Duanmaulan4.DataView.QuanlyhocvienModels;
using Duanmaulan4.DataView.QuanLyKhoaDaotaoModels;
using Duanmaulan4.Helpers;
using Duanmaulan4.Models;
using Duanmaulan4.Services;
using Duanmaulan4.Services.PhanQuyen;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Ocsp;

namespace Duanmaulan4.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentServices _studentServices; private readonly IAccountServices accountRepository; private readonly ILogger<AccountsController> logger;
        public StudentsController(IAccountServices accountRepository, ILogger<AccountsController> logger, IStudentServices studentServices)
        {
            this.accountRepository = accountRepository;
            this.logger = logger;
            _studentServices = studentServices;
        }

        [HttpGet]
        [Authorize(Roles = PhanQuyenViewModel.Role_Admin + "," + PhanQuyenViewModel.Role_Lecturer + "," + PhanQuyenViewModel.Role_Accounting + "," + PhanQuyenViewModel.Role_Director + "," + PhanQuyenViewModel.Role_Registration)]
        [CustomAuthorization(PhanQuyenViewModel.Claim_Viewallstudentlists)]
        public async Task<ActionResult<List<StudentViewModel>>> GetStudents()
        {
            var students = await _studentServices.GetStudentsAsync();
            return Ok(students);
        }

        [HttpGet("search-students")]
        [Authorize(Roles = PhanQuyenViewModel.Role_Admin + "," + PhanQuyenViewModel.Role_Lecturer + "," + PhanQuyenViewModel.Role_Accounting + "," + PhanQuyenViewModel.Role_Director + "," + PhanQuyenViewModel.Role_Registration)]
        [CustomAuthorization(PhanQuyenViewModel.Claim_Viewallstudentlists)]
        public async Task<ActionResult<List<StudentViewModel>>> SearchStudents(string searchKeyword)
        {
            try
            {
                var students = await _studentServices.GetStudentsAsync(searchKeyword);
                return Ok(students);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(500, new { Error = "Có lỗi xảy ra" });
            }
        }
        [HttpPost("signuphocvien")]
        [Authorize(Roles = PhanQuyenViewModel.Role_Admin + "," + PhanQuyenViewModel.Role_Accounting + "," + PhanQuyenViewModel.Role_Director + "," + PhanQuyenViewModel.Role_Registration)]
        [CustomAuthorization(PhanQuyenViewModel.Claim_Adddeleteeditstudents)]
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
        [Authorize(Roles = PhanQuyenViewModel.Role_Admin + "," + PhanQuyenViewModel.Role_Accounting + "," + PhanQuyenViewModel.Role_Director + "," + PhanQuyenViewModel.Role_Registration)]
        public async Task<ActionResult<List<ClassViewModel>>> GetAllClasses()
        {
            var classes = await _studentServices.GetAllClassesAsync();
            return Ok(classes);
        }
        [HttpGet("enrolled-classes/{MaHocSinh}")]
        /*[Authorize]*/
        [Authorize(Roles = PhanQuyenViewModel.Role_Admin + "," + PhanQuyenViewModel.Role_Lecturer + "," + PhanQuyenViewModel.Role_Student)]
        public async Task<IActionResult> GetEnrolledClasses(string MaHocSinh)
        {
            var enrolledClasses = await _studentServices.GetClassesEnrolledByStudentAsync(MaHocSinh);
            return Ok(enrolledClasses);
        }


        [HttpGet("enrolled-classessearch/{MaHocSinh}")]
        /*[Authorize]*/
        [Authorize(Roles = PhanQuyenViewModel.Role_Admin + "," + PhanQuyenViewModel.Role_Lecturer + "," + PhanQuyenViewModel.Role_Student)]
        public async Task<IActionResult> GetEnrolledClassessearch(string MaHocSinh, string tenLopSearch)
        {
            var enrolledClasses = await _studentServices.GetClassesEnrolledByStudentAsync(MaHocSinh, tenLopSearch);
            return Ok(enrolledClasses);
        }

        [HttpPost("registerclass")]
        [Authorize(Roles = PhanQuyenViewModel.Role_Student)]
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
        [Authorize(Roles = PhanQuyenViewModel.Role_Admin + "," + PhanQuyenViewModel.Role_Accounting + "," + PhanQuyenViewModel.Role_Director + "," + PhanQuyenViewModel.Role_Registration)]
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
        /*[Authorize]*/
        [Authorize(Roles = PhanQuyenViewModel.Role_Admin + "," + PhanQuyenViewModel.Role_Lecturer + "," + PhanQuyenViewModel.Role_Student)]
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
        /*[Authorize]*/
        [Authorize(Roles = PhanQuyenViewModel.Role_Admin + "," + PhanQuyenViewModel.Role_Lecturer + "," + PhanQuyenViewModel.Role_Student)]
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
        [Authorize(Roles = PhanQuyenViewModel.Role_Admin + "," + PhanQuyenViewModel.Role_Accounting + "," + PhanQuyenViewModel.Role_Director + "," + PhanQuyenViewModel.Role_Registration)]
        [CustomAuthorization(PhanQuyenViewModel.Claim_Adddeleteeditstudents)]
        public async Task<ActionResult> UpdateStudentDetails([FromBody] StudentUpdateModel model)
        {
            try
            {
                var result = await _studentServices.UpdateStudentAsync(model.MaHocSinh, model.Ho, model.TenDemvaTen,
                model.NgaySinh, model.GioiTinh, model.Email, model.DienThoai, model.DiaChi, model.HoTenPhuHuynh, model.HinhAnh, model.MaPhanCong);

                if (result.Succeeded)
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
        [Authorize(Roles = PhanQuyenViewModel.Role_Admin + "," + PhanQuyenViewModel.Role_Accounting + "," + PhanQuyenViewModel.Role_Director + "," + PhanQuyenViewModel.Role_Registration)]
        [CustomAuthorization(PhanQuyenViewModel.Claim_Adddeleteeditstudents)]
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

        [HttpGet("new-students")]
        [Authorize(Roles = PhanQuyenViewModel.Role_Admin + "," + PhanQuyenViewModel.Role_Accounting + "," + PhanQuyenViewModel.Role_Director + "," + PhanQuyenViewModel.Role_Registration)]
        public async Task<ActionResult<List<StudentViewModel>>> GetNewStudents()
        {
            try
            {
                var newStudents = await _studentServices.GetNewStudentsAsync();

                return Ok(newStudents);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(500, new { Error = "Có lỗi xảy ra" });
            }
        }
        [HttpPost("thu-hoc-phi")]
        [CustomAuthorization(PhanQuyenViewModel.Claim_Adddeleteediteducationmanagement)]
        public async Task<IActionResult> ThuHocPhi([FromBody] ThuHocPhiRequest request)
        {
            try
            {
                var result = await _studentServices.ThuHocPhiAsync(
                    request.MaHocSinh,
                    request.MaLop,
                    request.MaLoaiHocPhi,
                    request.MucThuPhi,
                    request.GiamGia,
                    request.GhiChu
                );

                if (result)
                {
                    return Ok(new { Message = "Thu học phí thành công." });
                }
                else
                {
                    return BadRequest(new { Message = "Thu học phí không thành công. Có thể đã thu hoặc có lỗi khác." });
                }
            }
            catch (Exception ex)
            {
                // Log lỗi hoặc xử lý tùy ý
                return StatusCode(500, new { Message = "Lỗi server khi thực hiện thu học phí." });
            }
        }

    }
}
