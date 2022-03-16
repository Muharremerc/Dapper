using Dapper;
using DapperWebService.Model;
using DapperWebService.Request.Student;
using DapperWebService.Response;
using DapperWebService.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DapperWebService.Service
{
    public class StudentService : IStudentService
    {
        private readonly DapperContext _context;
        public StudentService(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<StudentLessonResponse>> GetStudentLessonsList()
        {

            var query = @"SELECT S.Id AS 'StudentId',S.Name+' '+S.Surname 'StudentName' ,T.Name AS 'TeacherName',L.Name AS 'LessonName' FROM Teacher AS T	
                                     JOIN TeacherLesson AS TL ON TL.TeacherId = T.Id
                                          JOIN Lesson AS L ON L.Id = TL.LessonId
                                               JOIN StudentTeacherLesson STL ON STL.TeacherLessonId = TL.Id
                                                    JOIN Student AS S ON S.Id = STL.StudentId";
            using (var connection = _context.CreateConnection())
            {
                var companies = await connection.QueryAsync<StudentLessonResponse>(query);
                return companies.ToList();
            }
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<StudentLessonResponse>> GetStudentLessonsListById(int id)
        {
            var procedureName = "GetStudentLessonsListById";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var studentLessons = await connection.QueryAsync<StudentLessonResponse>
                    (procedureName, parameters, commandType: CommandType.StoredProcedure);
                return studentLessons;
            }
        }

        public async Task<IEnumerable<Student>> GetStudentList()
        {
            var query = "SELECT * FROM Student";
            using (var connection = _context.CreateConnection())
            {
                var companies = await connection.QueryAsync<Student>(query);
                return companies.ToList();
            }
            throw new NotImplementedException();
        }

        public async Task<Student> CreateStudent(StudentCreateRequest student)
        {
            var query = "INSERT INTO Student (Name, Surname) VALUES (@Name, @Surname)" +
                        "SELECT CAST(SCOPE_IDENTITY() as int)";
            var parameters = new DynamicParameters();
            parameters.Add("Name", student.Name, DbType.String);
            parameters.Add("Surname", student.Surname, DbType.String);
            using (var connection = _context.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<int>(query, parameters);
                var createdStudent = new Student
                {
                    Id = id,
                    Name = student.Name,
                    Surname = student.Surname
                };
                return createdStudent;
            }
        }

        public async Task<Student> GetStudentById(int id)
        {
            var query = "SELECT * FROM Student WHERE Id = @Id";
            using (var connection = _context.CreateConnection())
            {
                var student = await connection.QuerySingleOrDefaultAsync<Student>(query, new { id });
                return student;
            }
        }

        public async Task<Student> GetStudentLessonsListById2(int id)
        {
            var query = "SELECT * FROM Student WHERE Id = @Id;" +
           @"SELECT L.* FROM Teacher AS T	
                   JOIN TeacherLesson AS TL ON TL.TeacherId = T.Id
                        JOIN Lesson AS L ON L.Id = TL.LessonId
                             JOIN StudentTeacherLesson STL ON STL.TeacherLessonId = TL.Id
                                  JOIN Student AS S ON S.Id = STL.StudentId Where S.Id = @Id";

            using (var connection = _context.CreateConnection())
            using (var multi = await connection.QueryMultipleAsync(query, new
            {
                id
            }))
            {
                var student = await multi.ReadSingleOrDefaultAsync<Student>();
                if (student != null)
                    student.LessonList = (await multi.ReadAsync<Lesson>()).ToList();
                return student;
            }
        }

        public async Task UpdateStudent(int id, StudentUpdateRequest student)
        {
            var query = "UPDATE Student SET Name = @Name, Surname = @Surname WHERE Id = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);
            parameters.Add("Name", student.Name, DbType.String);
            parameters.Add("Surname", student.Surname, DbType.String);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task DeleteStudent(int id)
        {
            var query = "DELETE FROM Student WHERE Id = @Id";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { id });
            }
        }
    }
}
