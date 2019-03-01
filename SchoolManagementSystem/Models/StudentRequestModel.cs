using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Models
{
    public class StudentRequestModel
    {
        public string StudentName { get; set; }
        public string StudentAddress { get; set; }
        public string StudentNo { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string ParentName { get; set; }
        public DateTime DateEnrolled { get; set; }
        public string Image { get; set; }
        public int ClassId { get; set; }
    }
}
