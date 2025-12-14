using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StudentJournal.Components;
using StudentJournal.Components.Account;
using StudentJournal.Data;
using StudentJournal.Data.Interfaces;
using StudentJournal.Data.Services;

namespace StudentJournal
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = "Server=(localdb)\\mssqllocaldb;Database=StudentJournalDB;Trusted_Connection=true;MultipleActiveResultSets=true;TrustServerCertificate=true";

            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            builder.Services.AddCascadingAuthenticationState();
            builder.Services.AddScoped<IdentityUserAccessor>();
            builder.Services.AddScoped<IdentityRedirectManager>();
            builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            })
            .AddIdentityCookies();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddSignInManager()
                .AddDefaultTokenProviders();

            builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

            builder.Services.AddScoped<IStudentService, StudentService>();
            builder.Services.AddScoped<ICourseService, CourseService>();
            builder.Services.AddScoped<IGradeService, GradeService>();

            var app = builder.Build();

            if (!app.Urls.Any())
            {
                app.Urls.Add("http://localhost:5000");
            }

            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.MapAdditionalIdentityEndpoints();

            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

                try
                {
                    logger.LogInformation("Проверяем подключение к базе данных...");

                    await dbContext.Database.EnsureCreatedAsync();
                    logger.LogInformation("База данных StudentJournalDB создана с данными!");

                    var studentCount = await dbContext.Students.CountAsync();
                    if (studentCount == 0)
                    {
                        logger.LogInformation("Создаем тестовые данные...");
                        await dbContext.SaveChangesAsync();
                        logger.LogInformation("Тестовые данные созданы!");
                    }
                    else
                    {
                        logger.LogInformation($"Данные: {studentCount} студентов, {await dbContext.Courses.CountAsync()} предметов");
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Ошибка: {Message}", ex.Message);
                    throw;
                }
            }

            // Явно указываем порт
            if (!app.Urls.Any())
            {
                app.Urls.Add("http://localhost:5000");
            }

            // Автоматически открываем браузер
            app.Lifetime.ApplicationStarted.Register(() =>
            {
                try
                {
                    System.Diagnostics.Process.Start(
                        new System.Diagnostics.ProcessStartInfo
                        {
                            FileName = "http://localhost:5000",
                            UseShellExecute = true
                        });
                }
                catch { /* Игнорируем ошибки открытия браузера */ }
            });

            Console.WriteLine("=".PadRight(50, '='));
            Console.WriteLine("StudentJournal запущен!");
            Console.WriteLine("Откройте: http://localhost:5000");
            Console.WriteLine("=".PadRight(50, '='));

            await app.RunAsync();
        }
    }
}
