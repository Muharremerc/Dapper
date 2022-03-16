using DapperWebService.Model;
using DapperWebService.Request.Student;
using DapperWebService.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperWebService.Service.Interfaces
{
    public interface IStudentService
    {
        Task<IEnumerable<Student>> GetStudentList();
        Task<IEnumerable<StudentLessonResponse>> GetStudentLessonsList();
        Task<IEnumerable<StudentLessonResponse>> GetStudentLessonsListById(int id);
        Task<Student> GetStudentLessonsListById2(int id);
        Task<Student> CreateStudent(StudentCreateRequest student);
        Task<Student> GetStudentById(int id);
        Task UpdateStudent(int id, StudentUpdateRequest student);
        Task DeleteStudent(int id);
    }
}
