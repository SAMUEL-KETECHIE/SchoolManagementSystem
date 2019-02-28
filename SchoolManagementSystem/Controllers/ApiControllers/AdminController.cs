using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SchoolManagementSystem.Data.Helpers;
using SchoolManagementSystem.Data.Utilities;

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

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
