using Microsoft.EntityFrameworkCore;
using StudentJournal.Data.Interfaces;
using StudentJournal.Data.Models;

namespace StudentJournal.Data.Services
{
    public class StudentService : IStudentService
    {
        private readonly ApplicationDbContext _context;
        public StudentService(ApplicationDbContext context) => _context = context;

        public async Task<IEnumerable<Student>> GetStudentsAsync() =>
            await _context.Students.AsNoTracking().ToListAsync();

        public async Task<Student?> GetStudentByIdAsync(int id) =>
            await _context.Students.FindAsync(id);

        public async Task AddStudentAsync(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateStudentAsync(Student student)
        {
            var existing = await _context.Students.FindAsync(student.Id);
            if (existing != null)
            {
                existing.Name = student.Name;
                existing.Institute = student.Institute;
                existing.Group = student.Group;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteStudentAsync(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Student>> GetStudentsByGroupAsync(string group) =>
            await _context.Students
                .Where(s => s.Group == group)
                .AsNoTracking()
                .ToListAsync();
    }
}
