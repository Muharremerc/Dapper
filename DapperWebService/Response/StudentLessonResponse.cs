using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperWebService.Response
{
    public class StudentLessonResponse
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string TeacherName { get; set; }
        public string LessonName { get; set; }
    }
}
