using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagementSystem.Data.Utilities
{
    public class ResponseModel
    {
        public dynamic Data { get; set; }
        public int Count { get; set; }
        public string Errors { get; set; }


        public ResponseModel(dynamic data, int count)
        {
            this.Data = data;
            this.Count = count;
        }

        public ResponseModel(dynamic data)
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
