using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagementSystem.Data.Models
{
    public class Teachers
    {
        public int TeacherId { get; set; }
        public string TeacherName { get; set; }
        public string TeacherNo { get; set; }
        public string TeacherAddress { get; set; }
        public string Image { get; set; }
        public bool IsActive { get; set; }
        public int SubjectId { get; set; }
       // public IEnumerable<Subjects> Subjects { get; set; }
    }
}
