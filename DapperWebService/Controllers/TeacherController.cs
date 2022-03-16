using DapperWebService.Request.Student;
using DapperWebService.Request.Teacher;
using DapperWebService.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperWebService.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherService;
        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }


        [HttpGet("{id}", Name = "GetTeacherById")]
        public async Task<IActionResult> GetTeacherById(int id)
        {
            try
            {
                var teacher = await _teacherService.GetTeacherById(id);
                return Ok(teacher);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateTeacher(TeacherCreateWithLessonsRequest teacherLessons)
        {
            try
            {
                var createdTeacher = await _teacherService.CreateTeacherWithLessons(teacherLessons);
                return CreatedAtRoute("GetTeacherById", new { id = createdTeacher.Id }, createdTeacher);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
