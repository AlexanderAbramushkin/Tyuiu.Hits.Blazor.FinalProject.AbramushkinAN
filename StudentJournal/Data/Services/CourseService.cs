using Microsoft.EntityFrameworkCore;
using StudentJournal.Data.Models;
using StudentJournal.Data.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentJournal.Data.Services
{
    public class CourseService : ICourseService
    {
        private readonly ApplicationDbContext _context;

        public CourseService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Course>> GetCoursesAsync()
        {
            return await _context.Courses.ToListAsync();
        }

        public async Task<Course?> GetCourseByIdAsync(int id)
        {
            return await _context.Courses.FindAsync(id);
        }

        public async Task<bool> CourseNameExistsAsync(string name, int? excludeId = null)
        {
            var normalizedName = name.Trim().ToLower();
            var query = _context.Courses.Where(c => c.Name.Trim().ToLower() == normalizedName);

            if (excludeId.HasValue)
                query = query.Where(c => c.Id != excludeId.Value);

            return await query.AnyAsync();
        }

        public async Task AddCourseAsync(Course course)
        {
            if (await CourseNameExistsAsync(course.Name))
                throw new InvalidOperationException("Учебная дисциплина с таким названием уже существует");

            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCourseAsync(Course updatedCourse)
        {
            // Находим существующий курс
            var existingCourse = await _context.Courses.FindAsync(updatedCourse.Id);
            if (existingCourse == null)
                throw new ArgumentException("Курс не найден");

            // Проверяем, не изменилось ли имя (если изменилось - проверяем дубликат)
            if (existingCourse.Name != updatedCourse.Name)
            {
                var exists = await CourseNameExistsAsync(updatedCourse.Name, updatedCourse.Id);
                if (exists)
                    throw new InvalidOperationException("Учебная дисциплина с таким названием уже существует");
            }

            // Обновляем свойства
            existingCourse.Name = updatedCourse.Name;
            existingCourse.Description = updatedCourse.Description;

            // Сохраняем изменения
            await _context.SaveChangesAsync();
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