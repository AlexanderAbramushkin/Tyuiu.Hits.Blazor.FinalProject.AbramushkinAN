using StudentJournal.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentJournal.Data.Interfaces
{
    public interface IGradeService
    {
        Task<IEnumerable<Grade>> GetGradesAsync();
        Task<Grade?> GetGradeByIdAsync(int id);
        Task AddGradeAsync(Grade grade);
        Task UpdateGradeAsync(Grade updatedGrade);
        Task DeleteGradeAsync(int id);
        Task<IEnumerable<Grade>> GetGradesByStudentIdAsync(int studentId);
        Task<IEnumerable<Grade>> GetGradesByCourseIdAsync(int courseId);
        Task<IEnumerable<Grade>> GetFilteredGradesAsync(int? studentId = null, int? courseId = null, int? studentCourse = null);
        Task<bool> GradeExistsAsync(int studentId, int courseId);

        Task<bool> GradeExistsAsyncExcludingId(int studentId, int courseId, int excludeId);
    }
}
