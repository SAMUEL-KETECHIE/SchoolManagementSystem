using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql;
using SchoolManagementSystem.Data.Models;

namespace SchoolManagementSystem.Data.Helpers
{
    public class DataHelper
    {
        private readonly DbHelper _dbHelper;
        private readonly ILogger<DataHelper> _logger;
        private readonly IConfiguration _configuration;
        private static string _ConnectionStr;
        public DataHelper(DbHelper dbHelper, ILogger<DataHelper> logger,IConfiguration configuration)
        {
            _dbHelper = dbHelper;
            _logger = logger;
            _configuration = configuration;
            _ConnectionStr = _configuration.GetConnectionString("SchoolData");
        }

        #region Students Queries
        public async Task<List<Students>> GetAllStudents()
        {
            try
            {
                using (var conn = new NpgsqlConnection(_ConnectionStr))
                {
                    var result = new List<Students>();
                    var command = _dbHelper.CreateCommand("getallstudents", _ConnectionStr);
                    conn.Open();
                    var reader =await command.ExecuteReaderAsync();

                    while (reader.Read())
                    {
                        result.Add(new Students
                        {
                            StudentId = reader.GetFieldValue<int>(0),
                            StudentName = reader.GetFieldValue<string>(1),
                            StudentAddress = reader.GetFieldValue<string>(2),
                            StudentNo = reader.GetFieldValue<string>(3),
                            DateOfBirth = reader.GetFieldValue<DateTime>(4),
                            Age = reader.GetFieldValue<int>(5),
                            Gender = reader.GetFieldValue<string>(6),
                            ParentName = reader.GetFieldValue<string>(7),
                            DateEnrolled = reader.GetFieldValue<DateTime>(8),
                            IsActive = reader.GetFieldValue<bool>(9),
                            Image = reader.GetFieldValue<string>(10),
                            ClassId = reader.GetFieldValue<int>(11),
                        });
                    }
                    conn.Dispose();
                    conn.Close();
                    return result;
                }
                    

            }
            catch (Exception e)
            {
                _logger.LogError(e,$"An error occurred executing {nameof(GetAllStudents)}");
                throw e;
            }
        }

        #endregion Students

        public async Task<List<Teachers>> GetAllTeachers()
        {
            try
            {
                using (var conn = new NpgsqlConnection(_ConnectionStr))
                {
                    var result = new List<Teachers>();
                    var command = _dbHelper.CreateCommand("getallteachers", _ConnectionStr);
                    conn.Open();
                    var reader = await command.ExecuteReaderAsync();

                    while (reader.Read())
                    {
                        result.Add(new Teachers
                        {
                            TeacherId = reader.GetFieldValue<int>(0),
                            TeacherName = reader.GetFieldValue<string>(1),
                            TeacherNo = reader.GetFieldValue<string>(2),
                            TeacherAddress = reader.GetFieldValue<string>(3),
                            Image = reader.GetFieldValue<string>(4),
                            IsActive = reader.GetFieldValue<bool>(5),
                            SubjectId = reader.GetFieldValue<int>(6)
                        });
                    }
                    conn.Dispose();
                    conn.Close();
                    return result;
                }


            }
            catch (Exception e)
            {
                _logger.LogError(e, $"An error occurred executing {nameof(GetAllTeachers)}");
                throw e;
            }
        }

        public async Task<List<Subjects>> GetAllSubjects()
        {
            try
            {
                using (var conn = new NpgsqlConnection(_ConnectionStr))
                {
                    var result = new List<Subjects>();
                    var command = _dbHelper.CreateCommand("getallsubjects", _ConnectionStr);
                    conn.Open();
                    var reader = await command.ExecuteReaderAsync();

                    while (reader.Read())
                    {
                        result.Add(new Subjects
                        {
                            SubjectId = reader.GetFieldValue<int>(0),
                            SubjectName = reader.GetFieldValue<string>(1)
                        });
                    }
                    conn.Dispose();
                    conn.Close();
                    return result;
                }


            }
            catch (Exception e)
            {
                _logger.LogError(e, $"An error occurred executing {nameof(GetAllSubjects)}");
                throw e;
            }
        }

        public async Task<List<Users>> GetAllUsers()
        {
            try
            {
                using (var conn = new NpgsqlConnection(_ConnectionStr))
                {
                    var result = new List<Users>();
                    var command = _dbHelper.CreateCommand("getallusers", _ConnectionStr);
                    conn.Open();
                    var reader = await command.ExecuteReaderAsync();

                    while (reader.Read())
                    {
                        result.Add(new Users
                        {
                            UserId = reader.GetFieldValue<int>(0),
                            Username = reader.GetFieldValue<string>(1),
                            Password = reader.GetFieldValue<string>(2),
                            IsActive = reader.GetFieldValue<bool>(3),
                            RoleId = reader.GetFieldValue<int>(4)
                        });
                    }
                    conn.Dispose();
                    conn.Close();
                    return result;
                }


            }
            catch (Exception e)
            {
                _logger.LogError(e, $"An error occurred executing {nameof(GetAllUsers)}");
                throw e;
            }
        }

        public async Task<List<Classes>> GetAllClasses()
        {
            try
            {
                using (var conn = new NpgsqlConnection(_ConnectionStr))
                {
                    var result = new List<Classes>();
                    var command = _dbHelper.CreateCommand("getallclasses", _ConnectionStr);
                    conn.Open();
                    var reader = await command.ExecuteReaderAsync();

                    while (reader.Read())
                    {
                        result.Add(new Classes
                        {
                            ClassId = reader.GetFieldValue<int>(0),
                            ClassName = reader.GetFieldValue<string>(1)
                        });
                    }
                    conn.Dispose();
                    conn.Close();
                    return result;
                }


            }
            catch (Exception e)
            {
                _logger.LogError(e, $"An error occurred executing {nameof(GetAllClasses)}");
                throw e;
            }
        }

        public async Task<List<Role>> GetAllRoles()
        {
            try
            {
                using (var conn = new NpgsqlConnection(_ConnectionStr))
                {
                    var result = new List<Role>();
                    var command = _dbHelper.CreateCommand("getallroles", _ConnectionStr);
                    conn.Open();
                    var reader = await command.ExecuteReaderAsync();

                    while (reader.Read())
                    {
                        result.Add(new Role
                        {
                            RoleId = reader.GetFieldValue<int>(0),
                            RoleName = reader.GetFieldValue<string>(1)
                        });
                    }
                    conn.Dispose();
                    conn.Close();
                    return result;
                }


            }
            catch (Exception e)
            {
                _logger.LogError(e, $"An error occurred executing {nameof(GetAllRoles)}");
                throw e;
            }
        }
    }
}
