using Microsoft.EntityFrameworkCore;
using StudentJournal.Data.Interfaces;
using StudentJournal.Data.Models;

namespace StudentJournal.Data.Services
{
    public class CourseService : ICourseService
    {
        private readonly ApplicationDbContext _context;
        public CourseService(ApplicationDbContext context) => _context = context;

        public async Task<IEnumerable<Course>> GetCoursesAsync() =>
            await _context.Courses.AsNoTracking().ToListAsync();

        public async Task<Course?> GetCourseByIdAsync(int id) =>
            await _context.Courses.FindAsync(id);

        public async Task AddCourseAsync(Course course)
        {
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCourseAsync(Course course)
        {
            var existing = await _context.Courses.FindAsync(course.Id);
            if (existing != null)
            {
                existing.Name = course.Name;
                existing.Description = course.Description;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteCourseAsync(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course != null)
            {
                _context.Courses.Remove(course);
                await _context.SaveChangesAsync();
            }
        }
    }
}
