using DapperWebService.Request.Student;
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
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetStudentList()
        {
            try
            {
                var response = await _studentService.GetStudentList();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetStudentLessonsList()
        {
            try
            {
                var response = await _studentService.GetStudentLessonsList();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentLessonsListById(int id)
        {
            try
            {
                var response = await _studentService.GetStudentLessonsListById(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentLessonsListById2(int id)
        {
            try
            {
                var response = await _studentService.GetStudentLessonsListById2(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}", Name = "StudentById")]
        public async Task<IActionResult> GetStudent(int id)
        {
            try
            {
                var response = await _studentService.GetStudentById(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(StudentCreateRequest student)
        {
            try
            {
                var createdStudent = await _studentService.CreateStudent(student);
                return CreatedAtRoute("StudentById", new { id = createdStudent.Id }, createdStudent);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, StudentUpdateRequest student)
        {
            try
            {
                var updatedStudent = await _studentService.GetStudentById(id);
                if (updatedStudent == null)
                    return NotFound();
                await _studentService.UpdateStudent(id, student);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            try
            {
                var deletedStudent = await _studentService.GetStudentById(id);
                if (deletedStudent == null)
                    return NotFound();
                await _studentService.DeleteStudent(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }

}
