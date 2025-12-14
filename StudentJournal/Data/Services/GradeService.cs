using Microsoft.EntityFrameworkCore;
using StudentJournal.Data.Models;
using StudentJournal.Data.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentJournal.Data.Services
{
    public class GradeService : IGradeService
    {
        private readonly ApplicationDbContext _context;

        public GradeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Grade>> GetGradesAsync()
        {
            return await _context.Grades
                .Include(g => g.Student)
                .Include(g => g.Course)
                .ToListAsync();
        }

        public async Task<Grade?> GetGradeByIdAsync(int id)
        {
            return await _context.Grades
                .Include(g => g.Student)
                .Include(g => g.Course)
                .FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task AddGradeAsync(Grade grade)
        {
            var exists = await _context.Grades
                .AnyAsync(g => g.StudentId == grade.StudentId && g.CourseId == grade.CourseId);

            if (exists)
                throw new InvalidOperationException("Оценка по этому предмету для данного студента уже существует");

            _context.Grades.Add(grade);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> GradeExistsAsync(int studentId, int courseId)
        {
            return await _context.Grades
                .AnyAsync(g => g.StudentId == studentId && g.CourseId == courseId);
        }


        public async Task UpdateGradeAsync(Grade updatedGrade)
        {
            _context.Grades.Update(updatedGrade);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteGradeAsync(int id)
        {
            var grade = await _context.Grades.FindAsync(id);
            if (grade != null)
            {
                _context.Grades.Remove(grade);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Grade>> GetGradesByStudentIdAsync(int studentId)
        {
            return await _context.Grades
                .Include(g => g.Student)
                .Include(g => g.Course)
                .Where(g => g.StudentId == studentId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Grade>> GetGradesByCourseIdAsync(int courseId)
        {
            return await _context.Grades
                .Include(g => g.Student)
                .Include(g => g.Course)
                .Where(g => g.CourseId == courseId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Grade>> GetFilteredGradesAsync(int? studentId = null, int? courseId = null, int? studentCourse = null)
        {
            var query = _context.Grades
                .Include(g => g.Student)
                .Include(g => g.Course)
                .AsQueryable();

            if (studentId.HasValue && studentId.Value > 0)
                query = query.Where(g => g.StudentId == studentId.Value);

            if (courseId.HasValue && courseId.Value > 0)
                query = query.Where(g => g.CourseId == courseId.Value);

            if (studentCourse.HasValue && studentCourse.Value > 0)
                query = query.Where(g => g.Student.Course == studentCourse.Value);

            return await query.ToListAsync();

        }


        public async Task<bool> GradeExistsAsyncExcludingId(int studentId, int courseId, int excludeId)
        {
            return await _context.Grades
                .AnyAsync(g => g.StudentId == studentId
                            && g.CourseId == courseId
                            && g.Id != excludeId);
        }

    }
}