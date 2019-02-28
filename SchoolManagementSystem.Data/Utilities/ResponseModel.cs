using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagementSystem.Data.Utilities
{
    public class ResponseModel
    {
        public Array Data { get; set; }
        public int Count { get; set; }
        public string Errors { get; set; }


        public ResponseModel(Array data, int count)
        {
            this.Data = data;
            this.Count = count;
        }

        public ResponseModel(Array data)
        {
            this.Data = data;
        }

        public ResponseModel(string errors)
        {
            this.Errors = errors;
        }

        public ResponseModel()
        {

        }
    }
}
