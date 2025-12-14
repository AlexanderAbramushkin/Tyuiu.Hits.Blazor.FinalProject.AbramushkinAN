using Microsoft.EntityFrameworkCore;
using StudentJournal.Data.Models;
using StudentJournal.Data.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentJournal.Data.Services
{
    public class StudentService : IStudentService
    {
        private readonly ApplicationDbContext _context;

        public StudentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Student>> GetStudentsAsync()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task<Student?> GetStudentByIdAsync(int id)
        {
            return await _context.Students.FindAsync(id);
        }

        public async Task<bool> StudentExistsAsync(string name, string group)
        {
            return await _context.Students
                .AnyAsync(s =>
                    s.Name.ToLower().Trim() == name.ToLower().Trim() &&
                    s.Group.ToLower().Trim() == group.ToLower().Trim());
        }

        public async Task AddStudentAsync(Student student)
        {
            var exists = await StudentExistsAsync(student.Name, student.Group);
            if (exists)
            {
                throw new InvalidOperationException($"Студент {student.Name} уже существует в группе {student.Group}");
            }

            _context.Students.Add(student);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateStudentAsync(Student updatedStudent)
        {
            var existingStudent = await _context.Students.FindAsync(updatedStudent.Id);
            if (existingStudent == null)
                throw new ArgumentException("Студент не найден");

            var duplicateExists = await _context.Students
                .AnyAsync(s =>
                    s.Id != updatedStudent.Id &&
                    s.Name.ToLower().Trim() == updatedStudent.Name.ToLower().Trim() &&
                    s.Group.ToLower().Trim() == updatedStudent.Group.ToLower().Trim());

            if (duplicateExists)
            {
                throw new InvalidOperationException($"Студент {updatedStudent.Name} уже существует в группе {updatedStudent.Group}");
            }

            _context.Entry(existingStudent).CurrentValues.SetValues(updatedStudent);
            await _context.SaveChangesAsync();
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

        public async Task<IEnumerable<Student>> GetStudentsByGroupAsync(string group)
        {
            return await _context.Students
                .Where(s => s.Group == group)
                .ToListAsync();
        }

        public async Task<IEnumerable<Student>> GetStudentsByCourseAsync(int course)
        {
            return await _context.Students
                .Where(s => s.Course == course)
                .ToListAsync();
        }
    }
}