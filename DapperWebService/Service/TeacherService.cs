using Dapper;
using DapperWebService.Model;
using DapperWebService.Request.Teacher;
using DapperWebService.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DapperWebService.Service
{
    public class TeacherService : ITeacherService
    {
        private readonly DapperContext _context;
        public TeacherService(DapperContext context)
        {
            _context = context;
        }

        public async Task<Teacher> CreateTeacherWithLessons(TeacherCreateWithLessonsRequest request)
        {
            int teacherId = 0;
            var queryTeacher = "INSERT INTO Teacher (Name) VALUES (@Name)" +
                               "SELECT CAST(SCOPE_IDENTITY() as int)";
            var queryTeacherLessons = "INSERT INTO TeacherLesson (LessonId, TeacherId) VALUES (@LessonId, @TeacherId)";
            string indexList = "";
            for (int i = 0; i < request.LessonIdList.Count; i++)
            {
                indexList += request.LessonIdList[i];
                if (i != request.LessonIdList.Count - 1)
                    indexList += ",";
            }
            var queryLessonList = $"SELECT * FROM Lesson AS L WHERE L.Id IN ({indexList})";
            using (var connection = _context.CreateConnection())
            {
                connection.Open();
             
                using (var transaction = connection.BeginTransaction())
                {
                    #region Create Teacher 

                    var teacherCreateparameters = new DynamicParameters();
                    teacherCreateparameters.Add("Name", request.Name, DbType.String);
                    teacherId = await connection.QuerySingleAsync<int>(queryTeacher, teacherCreateparameters, transaction);

                    #endregion

                    var LessonListparameters = new DynamicParameters();
                    var lessonList = await connection.QueryAsync<Lesson>(queryLessonList,transaction: transaction);

                    #region LessonList

                    #endregion
                    foreach (var lesson in lessonList)
                    {
                        var parameters = new DynamicParameters();
                        parameters.Add("LessonId", lesson.Id, DbType.Int64);
                        parameters.Add("TeacherId", teacherId, DbType.Int64);
                        await connection.ExecuteAsync(queryTeacherLessons, parameters, transaction: transaction);
                    }
                    transaction.Commit();
                }
            }
            return new Teacher
            {
                Id = teacherId,
                Name = request.Name
            };
        }

        public async Task<Teacher> GetTeacherById(int id)
        {
            var query = "SELECT * FROM Teacher WHERE Id = @Id";
            using (var connection = _context.CreateConnection())
            {
                var teacher = await connection.QuerySingleOrDefaultAsync<Teacher>(query, new { id });
                return teacher;
            }
        }
    }
}
