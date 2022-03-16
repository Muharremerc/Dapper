using DapperWebService.Model;
using DapperWebService.Request.Teacher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperWebService.Service.Interfaces
{
    public interface ITeacherService
    {
        Task<Teacher> CreateTeacherWithLessons(TeacherCreateWithLessonsRequest request);
        Task<Teacher> GetTeacherById(int id);
    }
}
