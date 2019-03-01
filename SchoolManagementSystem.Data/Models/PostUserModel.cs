using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagementSystem.Data.Models
{
    public class PostUserModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
    }
}
