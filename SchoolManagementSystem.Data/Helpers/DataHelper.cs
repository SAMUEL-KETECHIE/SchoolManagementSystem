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
                    var reader = await command.ExecuteReaderAsync();

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

        #region Post Data

        public async Task<Students> AddNewStudent(PostStudentModel model)
        {
            try
            {
                using (var conn = new NpgsqlConnection(_ConnectionStr))
                {
                    var result = new Students();
                    var command = _dbHelper.CreateCommand("addstudents", _ConnectionStr);
                    command.Parameters.Add(new NpgsqlParameter("studentname", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new NpgsqlParameter("address", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new NpgsqlParameter("studentno", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new NpgsqlParameter("dateofbirth", NpgsqlTypes.NpgsqlDbType.Timestamp));
                    command.Parameters.Add(new NpgsqlParameter("age", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Parameters.Add(new NpgsqlParameter("gender", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new NpgsqlParameter("parentname", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new NpgsqlParameter("enrolleddate", NpgsqlTypes.NpgsqlDbType.Timestamp));
                    command.Parameters.Add(new NpgsqlParameter("image", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new NpgsqlParameter("classid", NpgsqlTypes.NpgsqlDbType.Integer));

                    command.Parameters[0].Value = model.StudentName;
                    command.Parameters[1].Value = model.StudentAddress;
                    command.Parameters[2].Value = model.StudentNo;
                    command.Parameters[3].Value = model.DateOfBirth;
                    command.Parameters[4].Value = model.Age;
                    command.Parameters[5].Value = model.Gender;
                    command.Parameters[6].Value = model.ParentName;
                    command.Parameters[7].Value = model.DateEnrolled;
                    command.Parameters[8].Value = model.Image;
                    command.Parameters[9].Value = model.ClassId;
                    conn.Open();
                    var reader = await command.ExecuteReaderAsync();

                    while (reader.Read())
                    {
                        result = new Students
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
                        };
                    }
                    conn.Dispose();
                    conn.Close();
                    return result;
                }


            }
            catch (Exception e)
            {
                _logger.LogError(e, $"An error occurred executing {nameof(AddNewStudent)}");
                throw e;
            }
        }

        public async Task<Teachers> AddNewTeacher(PostTeacherModel model)
        {
            try
            {
                using (var conn = new NpgsqlConnection(_ConnectionStr))
                {
                    var result = new Teachers();
                    var command = _dbHelper.CreateCommand("addteachers", _ConnectionStr);
                    command.Parameters.Add(new NpgsqlParameter("teachername", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new NpgsqlParameter("teacherno", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new NpgsqlParameter("address", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new NpgsqlParameter("image", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new NpgsqlParameter("IsActive", NpgsqlTypes.NpgsqlDbType.Boolean));
                    command.Parameters.Add(new NpgsqlParameter("SubjectId", NpgsqlTypes.NpgsqlDbType.Integer));

                    command.Parameters[0].Value = model.TeacherName;
                    command.Parameters[1].Value = model.TeacherNo;
                    command.Parameters[2].Value = model.TeacherAddress;
                    command.Parameters[3].Value = model.Image;
                    command.Parameters[4].Value = model.IsActive;
                    command.Parameters[5].Value = model.SubjectId;
                    var reader = await command.ExecuteReaderAsync();

                    while (reader.Read())
                    {
                        result =new Teachers
                        {
                            TeacherId = reader.GetFieldValue<int>(0),
                            TeacherName = reader.GetFieldValue<string>(1),
                            TeacherNo = reader.GetFieldValue<string>(2),
                            TeacherAddress = reader.GetFieldValue<string>(3),
                            Image = reader.GetFieldValue<string>(4),
                            IsActive = reader.GetFieldValue<bool>(5),
                            SubjectId = reader.GetFieldValue<int>(6)
                        };
                    }
                    conn.Dispose();
                    conn.Close();
                    return result;
                }


            }
            catch (Exception e)
            {
                _logger.LogError(e, $"An error occurred executing {nameof(AddNewTeacher)}");
                throw e;
            }
        }

        public async Task<Subjects> AddNewSubject(PostSubjectModel model)
        {
            try
            {
                using (var conn = new NpgsqlConnection(_ConnectionStr))
                {
                    var result = new Subjects();
                    var command = _dbHelper.CreateCommand("addsubjects", _ConnectionStr);
                    command.Parameters.Add(new NpgsqlParameter("subjectname", NpgsqlTypes.NpgsqlDbType.Text));

                    command.Parameters[0].Value = model.SubjectName;
                    var reader = await command.ExecuteReaderAsync();

                    while (reader.Read())
                    {
                       result = new Subjects
                        {
                            SubjectId = reader.GetFieldValue<int>(0),
                            SubjectName = reader.GetFieldValue<string>(1)
                        };
                    }
                    conn.Dispose();
                    conn.Close();
                    return result;
                }


            }
            catch (Exception e)
            {
                _logger.LogError(e, $"An error occurred executing {nameof(AddNewSubject)}");
                throw e;
            }
        }

        public async Task<Classes> AddNewClass(PostClassModel model)
        {
            try
            {
                using (var conn = new NpgsqlConnection(_ConnectionStr))
                {
                    var result = new Classes();
                    var command = _dbHelper.CreateCommand("addclasses", _ConnectionStr);
                    command.Parameters.Add(new NpgsqlParameter("classname", NpgsqlTypes.NpgsqlDbType.Text));

                    command.Parameters[0].Value = model.ClassName;
                    var reader = await command.ExecuteReaderAsync();

                    while (reader.Read())
                    {
                        result = new Classes
                        {
                            ClassId = reader.GetFieldValue<int>(0),
                            ClassName = reader.GetFieldValue<string>(1)
                        };
                    }
                    conn.Dispose();
                    conn.Close();
                    return result;
                }


            }
            catch (Exception e)
            {
                _logger.LogError(e, $"An error occurred executing {nameof(AddNewClass)}");
                throw e;
            }
        }

        public async Task<Users> AddNewUser(PostUserModel model)
        {
            try
            {
                using (var conn = new NpgsqlConnection(_ConnectionStr))
                {
                    var result = new Users();
                    var command = _dbHelper.CreateCommand("adduser", _ConnectionStr);
                    command.Parameters.Add(new NpgsqlParameter("username", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new NpgsqlParameter("password", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new NpgsqlParameter("roleid", NpgsqlTypes.NpgsqlDbType.Integer));

                    command.Parameters[0].Value = model.Username;
                    command.Parameters[1].Value = model.Password;
                    command.Parameters[2].Value = model.RoleId;
                    var reader = await command.ExecuteReaderAsync();

                    while (reader.Read())
                    {
                        result = new Users
                        {
                            UserId = reader.GetFieldValue<int>(0),
                            Username = reader.GetFieldValue<string>(1),
                            Password = reader.GetFieldValue<string>(2),
                            IsActive = reader.GetFieldValue<bool>(3),
                            RoleId = reader.GetFieldValue<int>(4)
                        };
                    }
                    conn.Dispose();
                    conn.Close();
                    return result;
                }


            }
            catch (Exception e)
            {
                _logger.LogError(e, $"An error occurred executing {nameof(AddNewUser)}");
                throw e;
            }
        }

        public async Task<Role> AddNewRole(PostRoleModel model)
        {
            try
            {
                using (var conn = new NpgsqlConnection(_ConnectionStr))
                {
                    var result = new Role();
                    var command = _dbHelper.CreateCommand("addrole", _ConnectionStr);
                    command.Parameters.Add(new NpgsqlParameter("rolename", NpgsqlTypes.NpgsqlDbType.Text));

                    command.Parameters[0].Value = model.RoleName;
                    var reader = await command.ExecuteReaderAsync();

                    while (reader.Read())
                    {
                        result = new Role
                        {
                            RoleId = reader.GetFieldValue<int>(0),
                            RoleName = reader.GetFieldValue<string>(1)
                        };
                    }
                    conn.Dispose();
                    conn.Close();
                    return result;
                }


            }
            catch (Exception e)
            {
                _logger.LogError(e, $"An error occurred executing {nameof(AddNewRole)}");
                throw e;
            }
        }

        #endregion Posting
    }
}
