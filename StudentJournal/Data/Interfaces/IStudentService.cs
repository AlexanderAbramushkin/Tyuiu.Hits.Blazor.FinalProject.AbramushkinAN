using StudentJournal.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentJournal.Data.Interfaces
{
    public interface IStudentService
    {
        Task<IEnumerable<Student>> GetStudentsAsync();
        Task<Student?> GetStudentByIdAsync(int id);
        Task AddStudentAsync(Student student);
        Task UpdateStudentAsync(Student updatedStudent);
        Task DeleteStudentAsync(int id);
        Task<IEnumerable<Student>> GetStudentsByGroupAsync(string group);
        Task<IEnumerable<Student>> GetStudentsByCourseAsync(int course);
        Task<bool> StudentExistsAsync(string name, string group);
    }
}