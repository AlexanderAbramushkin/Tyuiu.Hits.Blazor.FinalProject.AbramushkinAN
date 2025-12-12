using StudentJournal.Data.Models;

namespace StudentJournal.Data.Interfaces
{
    public interface IStudentService
    {
        Task<IEnumerable<Student>> GetStudentsAsync(); // Все студенты
        Task<Student?> GetStudentByIdAsync(int id);     // По Id
        Task AddStudentAsync(Student student);         // Добавить
        Task UpdateStudentAsync(Student student);      // Обновить
        Task DeleteStudentAsync(int id);               // Удалить
        Task<IEnumerable<Student>> GetStudentsByGroupAsync(string group); // Фильтр по группе
    }
}
