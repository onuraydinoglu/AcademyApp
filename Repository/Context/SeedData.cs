using AcademyApp.Entities;
using AcademyApp.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace AcademyApp.Repository
{
    public static class SeedData
    {
        public static void TestData(IApplicationBuilder app)
        {
            var context = app.ApplicationServices.CreateScope().ServiceProvider.GetService<AppDbContext>();
            if (context != null)
            {
                if (context.Database.GetPendingMigrations().Any())
                {
                    context.Database.Migrate();
                }

                if (!context.Categories.Any())
                {
                    context.Categories.AddRange(
                        new Category { Name = "Yazılım", Image = "yazilim.png" },
                        new Category { Name = "Sistem", Image = "sistem.png" },
                        new Category { Name = "İşletme", Image = "isletme.png" },
                        new Category { Name = "Tasarım", Image = "tasarim.png" },
                        new Category { Name = "kariyer", Image = "kariyer.png" }
                    );
                    context.SaveChanges();
                }

                if (!context.Instructors.Any())
                {
                    context.Instructors.AddRange(
                        new Instructor
                        {
                            FirstName = "Onur",
                            LastName = "Aydinoglu",
                            Email = "onur@info.com"
                        },
                        new Instructor
                        {
                            FirstName = "Nisa",
                            LastName = "Işık",
                            Email = "nisa@info.com"
                        }
                    );
                    context.SaveChanges();
                }

                if (!context.Students.Any())
                {
                    context.Students.AddRange(
                        new Student
                        {
                            FirstName = "Elisa",
                            LastName = "Aydinoglu",
                            Email = "elisa@info.com"
                        }
                    );
                    context.SaveChanges();
                }

                if (!context.Courses.Any())
                {
                    context.Courses.AddRange(
                        new Course
                        {
                            Title = "C#",
                            Description = "C# Dersleri",
                            Image = "csharp.png",
                            CategoryId = 1,
                            InstructorId = 1
                        },
                        new Course
                        {
                            Title = "Java",
                            Description = "Java Dersleri",
                            Image = "java.png",
                            CategoryId = 1,
                            InstructorId = 1
                        },
                        new Course
                        {
                            Title = "Python",
                            Description = "Python Dersleri",
                            Image = "python.png",
                            CategoryId = 1,
                            InstructorId = 1
                        },
                        new Course
                        {
                            Title = "Linux",
                            Description = "Linux Dersleri",
                            Image = "linux.png",
                            CategoryId = 2,
                            InstructorId = 2
                        },
                        new Course
                        {
                            Title = "Windows",
                            Description = "Windows Dersleri",
                            Image = "windows.png",
                            CategoryId = 2,
                            InstructorId = 1
                        },
                        new Course
                        {
                            Title = "İşletme",
                            Description = "İşletme Dersleri",
                            Image = "isletme.png",
                            CategoryId = 3,
                            InstructorId = 1
                        },
                        new Course
                        {
                            Title = "Pazarlama",
                            Description = "Pazarlama Dersleri",
                            Image = "pazarlama.png",
                            CategoryId = 3,
                            InstructorId = 1
                        },
                        new Course
                        {
                            Title = "UX",
                            Description = "UX Dersleri",
                            Image = "ux.png",
                            CategoryId = 4,
                            InstructorId = 2
                        },
                        new Course
                        {
                            Title = "Kariyer",
                            Description = "Kariyer Dersleri",
                            Image = "kariyer.png",
                            CategoryId = 5,
                            InstructorId = 1
                        }
                    );
                    context.SaveChanges();
                }
            }
        }
    }
}