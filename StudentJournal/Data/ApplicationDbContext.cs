using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentJournal.Data.Models;

namespace StudentJournal.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Grade> Grades { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // --- Студенты ---
            builder.Entity<Student>().HasData(
                new Student { Id = 1, Name = "Иван Иванов", Institute = "Строительный институт", Group = "ИИПБ-24-1" },
                new Student { Id = 2, Name = "Мария Петрова", Institute = "Высшая школа цифровых технологий", Group = "ВШЦТ-24-1" },
                new Student { Id = 3, Name = "Алексей Смирнов", Institute = "Технологический институт", Group = "ТИ-24-2" },
                new Student { Id = 4, Name = "Екатерина Кузнецова", Institute = "Институт архитектуры и дизайна", Group = "ИАД-24-1" },
                new Student { Id = 5, Name = "Дмитрий Соколов", Institute = "Институт сервиса", Group = "ИС-24-2" },
                new Student { Id = 6, Name = "Анна Попова", Institute = "Строительный институт", Group = "ИИПБ-24-2" },
                new Student { Id = 7, Name = "Сергей Лебедев", Institute = "Высшая школа цифровых технологий", Group = "ВШЦТ-24-2" },
                new Student { Id = 8, Name = "Ольга Морозова", Institute = "Технологический институт", Group = "ТИ-24-1" },
                new Student { Id = 9, Name = "Никита Павлов", Institute = "Институт архитектуры и дизайна", Group = "ИАД-24-2" },
                new Student { Id = 10, Name = "Елена Васильева", Institute = "Институт сервиса", Group = "ИС-24-1" },
                new Student { Id = 11, Name = "Максим Федоров", Institute = "Строительный институт", Group = "ИИПБ-24-3" },
                new Student { Id = 12, Name = "Алина Михайлова", Institute = "Высшая школа цифровых технологий", Group = "ВШЦТ-24-3" },
                new Student { Id = 13, Name = "Игорь Новиков", Institute = "Технологический институт", Group = "ТИ-24-3" },
                new Student { Id = 14, Name = "Светлана Семенова", Institute = "Институт архитектуры и дизайна", Group = "ИАД-24-3" },
                new Student { Id = 15, Name = "Владимир Егоров", Institute = "Институт сервиса", Group = "ИС-24-3" },
                new Student { Id = 16, Name = "Татьяна Крылова", Institute = "Строительный институт", Group = "ИИПБ-24-4" },
                new Student { Id = 17, Name = "Роман Николаев", Institute = "Высшая школа цифровых технологий", Group = "ВШЦТ-24-4" },
                new Student { Id = 18, Name = "Марина Орлова", Institute = "Технологический институт", Group = "ТИ-24-4" },
                new Student { Id = 19, Name = "Павел Козлов", Institute = "Институт архитектуры и дизайна", Group = "ИАД-24-4" },
                new Student { Id = 20, Name = "Юлия Белова", Institute = "Институт сервиса", Group = "ИС-24-4" }
            );

            // --- Курсы / предметы ---
            builder.Entity<Course>().HasData(
                new Course { Id = 1, Name = "Математика", Description = "Высшая математика" },
                new Course { Id = 2, Name = "Физика", Description = "Общая физика" },
                new Course { Id = 3, Name = "Информатика", Description = "Программирование и алгоритмы" },
                new Course { Id = 4, Name = "Химия", Description = "Общая химия" },
                new Course { Id = 5, Name = "История", Description = "История России и мира" },
                new Course { Id = 6, Name = "Архитектура", Description = "Основы проектирования зданий" },
                new Course { Id = 7, Name = "Дизайн", Description = "Основы графического и промышленного дизайна" }
            );

            // --- Генерация оценок ---
            var grades = new List<Grade>();
            var random = new Random();
            int gradeId = 1;

            for (int studentId = 1; studentId <= 20; studentId++)
            {
                for (int courseId = 1; courseId <= 7; courseId++)
                {
                    grades.Add(new Grade
                    {
                        Id = gradeId++,
                        StudentId = studentId,
                        CourseId = courseId,
                        Score = random.Next(50, 101) // оценки от 50 до 100
                    });
                }
            }

            builder.Entity<Grade>().HasData(grades);
        }

    }
}
