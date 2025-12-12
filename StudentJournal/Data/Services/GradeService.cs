using StudentJournal.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentJournal.Data.Interfaces;

namespace StudentJournal.Data.Services
{
    public class GradeService : IGradeService
    {
        private List<Grade> _grades = new();
        private int _nextId = 1;

        public GradeService()
        {
            // Добавим тестовые данные для проверки
            _grades.Add(new Grade { Id = _nextId++, StudentId = 1, CourseId = 1, Score = 85 });
            _grades.Add(new Grade { Id = _nextId++, StudentId = 1, CourseId = 2, Score = 92 });
            _grades.Add(new Grade { Id = _nextId++, StudentId = 2, CourseId = 1, Score = 78 });
            _grades.Add(new Grade { Id = _nextId++, StudentId = 2, CourseId = 2, Score = 65 });
        }

        public async Task<IEnumerable<Grade>> GetGradesAsync()
        {
            return await Task.FromResult(_grades);
        }

        public async Task<Grade?> GetGradeByIdAsync(int id)
        {
            return await Task.FromResult(_grades.FirstOrDefault(g => g.Id == id));
        }

        public async Task AddGradeAsync(Grade grade)
        {
            grade.Id = _nextId++;
            _grades.Add(grade);
            await Task.CompletedTask;
        }

        public async Task UpdateGradeAsync(Grade updatedGrade)
        {
            var grade = _grades.FirstOrDefault(g => g.Id == updatedGrade.Id);
            if (grade != null)
            {
                grade.StudentId = updatedGrade.StudentId;
                grade.CourseId = updatedGrade.CourseId;
                grade.Score = updatedGrade.Score;
            }
            await Task.CompletedTask;
        }

        public async Task DeleteGradeAsync(int id)
        {
            var grade = _grades.FirstOrDefault(g => g.Id == id);
            if (grade != null)
            {
                _grades.Remove(grade);
            }
            await Task.CompletedTask;
        }

        public Task<IEnumerable<Grade>> GetGradesByStudentIdAsync(int studentId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Grade>> GetGradesByCourseIdAsync(int courseId)
        {
            throw new NotImplementedException();
        }
    }
}