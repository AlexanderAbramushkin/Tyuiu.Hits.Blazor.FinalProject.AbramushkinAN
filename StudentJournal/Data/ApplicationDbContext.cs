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

            builder.Entity<Grade>()
                .HasIndex(g => new { g.StudentId, g.CourseId })
                .IsUnique();

            // ? 50 СТУДЕНТОВ (5 институтов ? 10 групп)
            builder.Entity<Student>().HasData(
                // Строительный институт (ИИПБ)
                new Student { Id = 1, Name = "Иван Иванов", Institute = "Строительный институт", Group = "ИИПБ-24-1", Course = 2 },
                new Student { Id = 2, Name = "Мария Петрова", Institute = "Строительный институт", Group = "ИИПБ-24-2", Course = 2 },
                new Student { Id = 3, Name = "Алексей Смирнов", Institute = "Строительный институт", Group = "ИИПБ-23-1", Course = 3 },
                new Student { Id = 4, Name = "Екатерина Кузнецова", Institute = "Строительный институт", Group = "ИИПБ-23-2", Course = 3 },
                new Student { Id = 5, Name = "Дмитрий Соколов", Institute = "Строительный институт", Group = "ИИПБ-25-1", Course = 1 },
                new Student { Id = 6, Name = "Анна Попова", Institute = "Строительный институт", Group = "ИИПБ-25-2", Course = 1 },
                new Student { Id = 7, Name = "Сергей Лебедев", Institute = "Строительный институт", Group = "ИИПБ-22-1", Course = 4 },
                new Student { Id = 8, Name = "Ольга Морозова", Institute = "Строительный институт", Group = "ИИПБ-22-2", Course = 4 },
                new Student { Id = 9, Name = "Никита Павлов", Institute = "Строительный институт", Group = "ИИПБ-21-1", Course = 5 },
                new Student { Id = 10, Name = "Елена Васильева", Institute = "Строительный институт", Group = "ИИПБ-21-2", Course = 5 },

                // Высшая школа цифровых технологий (ВШЦТ)
                new Student { Id = 11, Name = "Максим Федоров", Institute = "Высшая школа цифровых технологий", Group = "ВШЦТ-24-1", Course = 2 },
                new Student { Id = 12, Name = "Алина Михайлова", Institute = "Высшая школа цифровых технологий", Group = "ВШЦТ-24-2", Course = 2 },
                new Student { Id = 13, Name = "Игорь Новиков", Institute = "Высшая школа цифровых технологий", Group = "ВШЦТ-23-1", Course = 3 },
                new Student { Id = 14, Name = "Светлана Семенова", Institute = "Высшая школа цифровых технологий", Group = "ВШЦТ-23-2", Course = 3 },
                new Student { Id = 15, Name = "Владимир Егоров", Institute = "Высшая школа цифровых технологий", Group = "ВШЦТ-25-1", Course = 1 },
                new Student { Id = 16, Name = "Татьяна Крылова", Institute = "Высшая школа цифровых технологий", Group = "ВШЦТ-25-2", Course = 1 },
                new Student { Id = 17, Name = "Роман Николаев", Institute = "Высшая школа цифровых технологий", Group = "ВШЦТ-22-1", Course = 4 },
                new Student { Id = 18, Name = "Марина Орлова", Institute = "Высшая школа цифровых технологий", Group = "ВШЦТ-22-2", Course = 4 },
                new Student { Id = 19, Name = "Павел Козлов", Institute = "Высшая школа цифровых технологий", Group = "ВШЦТ-21-1", Course = 5 },
                new Student { Id = 20, Name = "Юлия Белова", Institute = "Высшая школа цифровых технологий", Group = "ВШЦТ-21-2", Course = 5 },

                // Технологический институт (ТИ)
                new Student { Id = 21, Name = "Артем Сидоров", Institute = "Технологический институт", Group = "ТИ-24-1", Course = 2 },
                new Student { Id = 22, Name = "Виктория Романова", Institute = "Технологический институт", Group = "ТИ-24-2", Course = 2 },
                new Student { Id = 23, Name = "Даниил Морозов", Institute = "Технологический институт", Group = "ТИ-23-1", Course = 3 },
                new Student { Id = 24, Name = "Елизавета Зайцева", Institute = "Технологический институт", Group = "ТИ-23-2", Course = 3 },
                new Student { Id = 25, Name = "Кирилл Леонтьев", Institute = "Технологический институт", Group = "ТИ-25-1", Course = 1 },
                new Student { Id = 26, Name = "Надежда Григорьева", Institute = "Технологический институт", Group = "ТИ-25-2", Course = 1 },
                new Student { Id = 27, Name = "Олег Киселев", Institute = "Технологический институт", Group = "ТИ-22-1", Course = 4 },
                new Student { Id = 28, Name = "Полina Андреева", Institute = "Технологический институт", Group = "ТИ-22-2", Course = 4 },
                new Student { Id = 29, Name = "Руслан Захаров", Institute = "Технологический институт", Group = "ТИ-21-1", Course = 5 },
                new Student { Id = 30, Name = "София Беляева", Institute = "Технологический институт", Group = "ТИ-21-2", Course = 5 },

                // Институт архитектуры и дизайна (ИАД)
                new Student { Id = 31, Name = "Тимур Воробьев", Institute = "Институт архитектуры и дизайна", Group = "ИАД-24-1", Course = 2 },
                new Student { Id = 32, Name = "Ульяна Соколова", Institute = "Институт архитектуры и дизайна", Group = "ИАД-24-2", Course = 2 },
                new Student { Id = 33, Name = "Федор Гаврилов", Institute = "Институт архитектуры и дизайна", Group = "ИАД-23-1", Course = 3 },
                new Student { Id = 34, Name = "Христина Денисова", Institute = "Институт архитектуры и дизайна", Group = "ИАД-23-2", Course = 3 },
                new Student { Id = 35, Name = "Эмиль Ефимов", Institute = "Институт архитектуры и дизайна", Group = "ИАД-25-1", Course = 1 },
                new Student { Id = 36, Name = "Яна Жукова", Institute = "Институт архитектуры и дизайна", Group = "ИАД-25-2", Course = 1 },
                new Student { Id = 37, Name = "Захар Иванов", Institute = "Институт архитектуры и дизайна", Group = "ИАД-22-1", Course = 4 },
                new Student { Id = 38, Name = "Агния Казакова", Institute = "Институт архитектуры и дизайна", Group = "ИАД-22-2", Course = 4 },
                new Student { Id = 39, Name = "Богдан Лазарев", Institute = "Институт архитектуры и дизайна", Group = "ИАД-21-1", Course = 5 },
                new Student { Id = 40, Name = "Вероника Макарова", Institute = "Институт архитектуры и дизайна", Group = "ИАД-21-2", Course = 5 },

                // Институт сервиса и отраслевого управления (ИС)
                new Student { Id = 41, Name = "Глеб Назаров", Institute = "Институт сервиса и отраслевого управления", Group = "ИС-24-1", Course = 2 },
                new Student { Id = 42, Name = "Дарья Осипова", Institute = "Институт сервиса и отраслевого управления", Group = "ИС-24-2", Course = 2 },
                new Student { Id = 43, Name = "Евгений Петухов", Institute = "Институт сервиса и отраслевого управления", Group = "ИС-23-1", Course = 3 },
                new Student { Id = 44, Name = "Жанна Рябова", Institute = "Институт сервиса и отраслевого управления", Group = "ИС-23-2", Course = 3 },
                new Student { Id = 45, Name = "Зиновий Савельев", Institute = "Институт сервиса и отраслевого управления", Group = "ИС-25-1", Course = 1 },
                new Student { Id = 46, Name = "Изольда Титова", Institute = "Институт сервиса и отраслевого управления", Group = "ИС-25-2", Course = 1 },
                new Student { Id = 47, Name = "Константин Уваров", Institute = "Институт сервиса и отраслевого управления", Group = "ИС-22-1", Course = 4 },
                new Student { Id = 48, Name = "Людмила Филиппова", Institute = "Институт сервиса и отраслевого управления", Group = "ИС-22-2", Course = 4 },
                new Student { Id = 49, Name = "Мирон Хохлов", Institute = "Институт сервиса и отраслевого управления", Group = "ИС-21-1", Course = 5 },
                new Student { Id = 50, Name = "Наталья Чернова", Institute = "Институт сервиса и отраслевого управления", Group = "ИС-21-2", Course = 5 }
            );

            // ? 15 ПРЕДМЕТОВ
            builder.Entity<Course>().HasData(
                new Course { Id = 1, Name = "Математика", Description = "Высшая математика" },
                new Course { Id = 2, Name = "Физика", Description = "Общая физика" },
                new Course { Id = 3, Name = "Информатика", Description = "Программирование и алгоритмы" },
                new Course { Id = 4, Name = "Химия", Description = "Общая химия" },
                new Course { Id = 5, Name = "История", Description = "История России и мира" },
                new Course { Id = 6, Name = "Архитектура", Description = "Основы проектирования зданий" },
                new Course { Id = 7, Name = "Дизайн", Description = "Основы графического и промышленного дизайна" },
                new Course { Id = 8, Name = "Английский язык", Description = "Бизнес английский" },
                new Course { Id = 9, Name = "Экономика", Description = "Микро- и макроэкономика" },
                new Course { Id = 10, Name = "Менеджмент", Description = "Основы управления" },
                new Course { Id = 11, Name = "Статистика", Description = "Математическая статистика" },
                new Course { Id = 12, Name = "Технологии программирования", Description = "Современные языки программирования" },
                new Course { Id = 13, Name = "Базы данных", Description = "Реляционные СУБД" },
                new Course { Id = 14, Name = "Веб-разработка", Description = "Frontend и Backend" },
                new Course { Id = 15, Name = "Проектный менеджмент", Description = "Управление проектами" }
            );

            // ? 750 ОЦЕНОК (50 студентов ? 15 предметов)
            var grades = new List<Grade>();
            int gradeId = 1;
            var random = new Random(42);

            for (int studentId = 1; studentId <= 50; studentId++)
            {
                for (int courseId = 1; courseId <= 15; courseId++)
                {
                    grades.Add(new Grade
                    {
                        Id = gradeId++,
                        StudentId = studentId,
                        CourseId = courseId,
                        Score = random.Next(50, 101)
                    });
                }
            }

            builder.Entity<Grade>().HasData(grades);
        }
    }
}
