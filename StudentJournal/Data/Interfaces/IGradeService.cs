using StudentJournal.Data.Models;

namespace StudentJournal.Data.Interfaces
{
    public interface IGradeService
    {
        Task<IEnumerable<Grade>> GetGradesAsync();                       // Все оценки
        Task<Grade?> GetGradeByIdAsync(int id);
        Task<IEnumerable<Grade>> GetGradesByStudentIdAsync(int studentId);
        Task<IEnumerable<Grade>> GetGradesByCourseIdAsync(int courseId);
        Task AddGradeAsync(Grade grade);
        Task UpdateGradeAsync(Grade grade);
        Task DeleteGradeAsync(int id);
    }
}
