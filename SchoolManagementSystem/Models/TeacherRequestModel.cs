using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Models
{
    public class TeacherRequestModel
    {
        public string TeacherName { get; set; }
        public string TeacherNo { get; set; }
        public string TeacherAddress { get; set; }
        public string Image { get; set; }
        public int SubjectId { get; set; }
        public bool IsActive { get; set; }
    }
}
