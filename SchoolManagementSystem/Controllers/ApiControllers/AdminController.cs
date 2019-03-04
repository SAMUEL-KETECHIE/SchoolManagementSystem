using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SchoolManagementSystem.Data.Helpers;
using SchoolManagementSystem.Data.Models;
using SchoolManagementSystem.Data.Utilities;
using SchoolManagementSystem.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Net.Http.Headers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SchoolManagementSystem.Controllers.ApiControllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class AdminController : Controller
    {
        private readonly DataHelper _dataHelper;
        private readonly ILogger<AdminController> _logger;
        private readonly IConfiguration _configuration;
        public AdminController(DataHelper dataHelper, ILogger<AdminController> logger, IConfiguration configuration)
        {
            _dataHelper = dataHelper;
            _logger = logger;
            _configuration = configuration;
        }

        #region Fetching Data Region
        // GET: api/<controller>
        [HttpGet]
        public async Task<ResponseModel> GetAllStudents()
        {
            try
            {
                var result = await _dataHelper.GetAllStudents();
                if (result != null)
                {
                    var response = new ResponseModel(result.ToArray(), result.Count());
                    return response;
                }
                return null;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"An error occurred executing {nameof(GetAllStudents)},-{e.Message}");
                return new ResponseModel(e.Message.ToString());
                throw e;
            }
        }

        [HttpGet]
        public async Task<ResponseModel> GetAllClasses()
        {
            try
            {
                var result = await _dataHelper.GetAllClasses();
                if (result != null)
                {
                    var response = new ResponseModel(result.ToArray(), result.Count());
                    return response;
                }
                return null;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"An error occurred executing {nameof(GetAllClasses)},-{e.Message}");
                return new ResponseModel(e.Message.ToString());
                throw e;
            }
        }

        [HttpGet]
        public async Task<ResponseModel> GetAllTeachers()
        {
            try
            {
                var result = await _dataHelper.GetAllTeachers();
                if (result != null)
                {
                    var response = new ResponseModel(result.ToArray(), result.Count());
                    return response;
                }
                return null;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"An error occurred executing {nameof(GetAllTeachers)},-{e.Message}");
                return new ResponseModel(e.Message.ToString());
                throw e;
            }
        }

        [HttpGet]
        public async Task<ResponseModel> GetAllSubjects()
        {
            try
            {
                var result = await _dataHelper.GetAllSubjects();
                if (result != null)
                {
                    var response = new ResponseModel(result.ToArray(), result.Count());
                    return response;
                }
                return null;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"An error occurred executing {nameof(GetAllSubjects)},-{e.Message}");
                return new ResponseModel(e.Message.ToString());
                throw e;
            }
        }

        #endregion Fetch



        #region Posting to server

        // POST api/<controller>
        [HttpPost]
        public async Task<ResponseModel> AddNewStudent([FromBody]StudentRequestModel model)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var age = CalculateAge(model.DateOfBirth);
                    var studNo = await GenerateStudentNo();
                    var imageUrl = model.Image;
                    var res = await _dataHelper.AddNewStudent(new PostStudentModel
                    {
                        StudentName = model.StudentName,
                        StudentNo = studNo,
                        StudentAddress = model.StudentAddress,
                        DateOfBirth = model.DateOfBirth,
                        Age = age,
                        Gender = model.Gender,
                        ParentName = model.ParentName,
                        DateEnrolled = model.DateEnrolled,
                        Image = imageUrl,
                        ClassId = model.ClassId
                    });
                    _ = await _dataHelper.AddNewUser(new PostUserModel
                    {
                        Username = model.StudentName,
                        Password = studNo,
                        RoleId = _configuration.GetValue<int>("App:StudentRole")
                    });
                    return new ResponseModel(res);
                }
                return new ResponseModel("An error occurred, please check the inputs");

            }
            catch (Exception e)
            {
                _logger.LogError(e, $"An error occurred executing {nameof(AddNewStudent)} on Admin Controller.");
                return new ResponseModel(e.Message);
                throw e;
            }
        }

        [HttpPost]
        public async Task<ResponseModel> AddNewSubject([FromBody]SubjectRequestModel model)
        {
            try
            {

                if (ModelState.IsValid)
                {

                    var res = await _dataHelper.AddNewSubject(new PostSubjectModel { SubjectName = model.SubjectName });

                    return new ResponseModel(res);
                }
                return new ResponseModel("An error occurred, please check the inputs");

            }
            catch (Exception e)
            {
                _logger.LogError(e, $"An error occurred executing {nameof(AddNewSubject)} on Admin Controller.");
                return new ResponseModel(e.Message);
                throw e;
            }
        }

        [HttpPost]
        public async Task<ResponseModel> AddNewTeacher([FromBody]TeacherRequestModel model)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var teachNo = await GenerateTeacherNo();

                    var res = await _dataHelper.AddNewTeacher(new PostTeacherModel
                    {
                        TeacherName = model.TeacherName,
                        IsActive = true,
                        TeacherAddress = model.TeacherAddress,
                        TeacherNo = teachNo,
                        Image = model.Image,
                        SubjectId = model.SubjectId
                    });
                    _ = await _dataHelper.AddNewUser(new PostUserModel
                    {
                        Username = model.TeacherName,
                        Password = teachNo,
                        RoleId = _configuration.GetValue<int>("App:TeachRole")
                    });
                    return new ResponseModel(res);
                }
                return new ResponseModel("An error occurred, please check the inputs");

            }
            catch (Exception e)
            {
                _logger.LogError(e, $"An error occurred executing {nameof(AddNewTeacher)} on Admin Controller.");
                return new ResponseModel(e.Message);
                throw e;
            }
        }

        #endregion post

        //Image Uploader
        [HttpPost]
       // public string SaveImage(IFormFile file)
        public string SaveImage()
        {
            try
            {
                var file = Request.Form.Files[0];
                string folderName = "Uploads";
                string webRootPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory);
                string newPath = Path.Combine(webRootPath, folderName);
                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }
                if (file.Length > 0)
                {
                    string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    string fullPath = Path.Combine(newPath, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                        return fullPath;
                    }
                }
                return null;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"An error occurred executing {nameof(SaveImage)} on Admin Controller. {e.Message}");
                throw e;
            }
        }
        #region Helper methods
        public async Task<string> GenerateStudentNo()
        {
            var students = await _dataHelper.GetAllStudents();
            var std = students.ToArray();
            var lastNo = 0;
            if (std.Length == 0)
            {
                lastNo = 1;
            }
            else
            {
                lastNo = std.Length + 1;
            }
            var stdNo = _configuration.GetValue<string>("App:StudentNo") + lastNo.ToString();
            return stdNo;
        }

        public async Task<string> GenerateTeacherNo()
        {
            var teacher = await _dataHelper.GetAllTeachers();
            var std = teacher.ToArray();
            var lastNo = 0;
            if (std.Length == 0)
            {
                lastNo = 1;
            }
            else
            {
                lastNo = std.Length + 1;
            }
            var stdNo = _configuration.GetValue<string>("App:TeacherNo") + lastNo.ToString();
            return stdNo;
        }

        private static int CalculateAge(DateTime dateOfBirth)
        {
            int age = 0;
            age = DateTime.Now.Year - dateOfBirth.Year;
            if (DateTime.Now.DayOfYear < dateOfBirth.DayOfYear)
                age = age - 1;

            return age;
        }

        static string CalculateYourAge(DateTime Dob)
        {
            DateTime Now = DateTime.Now;
            int Years = new DateTime(DateTime.Now.Subtract(Dob).Ticks).Year - 1;
            DateTime PastYearDate = Dob.AddYears(Years);
            int Months = 0;
            for (int i = 1; i <= 12; i++)
            {
                if (PastYearDate.AddMonths(i) == Now)
                {
                    Months = i;
                    break;
                }
                else if (PastYearDate.AddMonths(i) >= Now)
                {
                    Months = i - 1;
                    break;
                }
            }
            int Days = Now.Subtract(PastYearDate.AddMonths(Months)).Days;
            int Hours = Now.Subtract(PastYearDate).Hours;
            int Minutes = Now.Subtract(PastYearDate).Minutes;
            int Seconds = Now.Subtract(PastYearDate).Seconds;
            return String.Format("Age: {0} Year(s) {1} Month(s) {2} Day(s) {3} Hour(s) {4} Second(s)",
            Years, Months, Days, Hours, Seconds);
        }

        #endregion helpers

    }
}
