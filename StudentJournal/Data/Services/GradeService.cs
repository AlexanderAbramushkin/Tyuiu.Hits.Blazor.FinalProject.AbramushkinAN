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
            return await _context.Grades.ToListAsync();
        }

        public async Task<Grade?> GetGradeByIdAsync(int id)
        {
            return await _context.Grades.FindAsync(id);
        }

        public async Task AddGradeAsync(Grade grade)
        {
            _context.Grades.Add(grade);
            await _context.SaveChangesAsync();
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
                .Where(g => g.StudentId == studentId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Grade>> GetGradesByCourseIdAsync(int courseId)
        {
            return await _context.Grades
                .Where(g => g.CourseId == courseId)
                .ToListAsync();
        }
    }
}