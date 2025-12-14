using StudentJournal.Data.Models;

namespace StudentJournal.Data.Interfaces
{
    public interface ICourseService
    {
        Task<IEnumerable<Course>> GetCoursesAsync();
        Task<Course?> GetCourseByIdAsync(int id);
        Task AddCourseAsync(Course course);
        Task UpdateCourseAsync(Course course);
        Task DeleteCourseAsync(int id);
        Task<bool> CourseNameExistsAsync(string name, int? excludeId = null);
    }
}
