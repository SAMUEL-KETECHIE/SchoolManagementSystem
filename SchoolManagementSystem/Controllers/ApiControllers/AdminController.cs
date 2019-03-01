using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SchoolManagementSystem.Data.Helpers;
using SchoolManagementSystem.Data.Models;
using SchoolManagementSystem.Data.Utilities;
using SchoolManagementSystem.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SchoolManagementSystem.Controllers.ApiControllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class AdminController : Controller
    {
        private readonly DataHelper _dataHelper;
        private readonly ILogger<AdminController> _logger;
        public AdminController(DataHelper dataHelper, ILogger<AdminController> logger)
        {
            _dataHelper = dataHelper;
            _logger = logger;
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

                    var res = await _dataHelper.AddNewStudent(new PostStudentModel
                    {
                        StudentName = model.StudentName,
                        StudentNo = model.StudentNo,
                        StudentAddress = model.StudentAddress,
                        DateOfBirth = model.DateOfBirth,
                        Age = age,
                        Gender = model.Gender,
                        ParentName = model.ParentName,
                        DateEnrolled = model.DateEnrolled,
                        Image = model.Image,
                        ClassId = model.ClassId
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

                    var res = await _dataHelper.AddNewSubject(new PostSubjectModel { SubjectName=model.SubjectName});

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


        #endregion post


        #region Helper methods

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
