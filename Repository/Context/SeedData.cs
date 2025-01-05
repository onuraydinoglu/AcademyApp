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
                        new Category { Name = "Software", Image = "yazilim.png" },
                        new Category { Name = "System", Image = "sistem.png" },
                        new Category { Name = "Business", Image = "isletme.png" },
                        new Category { Name = "Design", Image = "tasarim.png" },
                        new Category { Name = "Career", Image = "kariyer.png" },
                        new Category { Name = "Personal Development", Image = "gelisim.png" }
                    );
                    context.SaveChanges();
                }

                if (!context.Levels.Any())
                {
                    context.Levels.AddRange(
                        new Level { Name = "Beginner" },
                        new Level { Name = "Intermediate" },
                        new Level { Name = "Advanced" }
                    );
                    context.SaveChanges();
                }

                if (!context.Roles.Any())
                {
                    context.Roles.AddRange(
                        new Role { Name = "Admin" },
                        new Role { Name = "Instructor" },
                        new Role { Name = "Student" }
                    );
                    context.SaveChanges();
                }

                if (!context.Users.Any())
                {
                    context.Users.AddRange(
                        new User
                        {
                            FirstName = "Onur",
                            LastName = "Aydınoğlu",
                            RoleId = 1,
                            Email = "admin@admin.com",
                            Password = "123",
                        },
                        new User
                        {
                            FirstName = "Onur",
                            LastName = "Aydınoğlu",
                            RoleId = 2,
                            Email = "onur@info.com",
                            Password = "123",
                        },
                        new User
                        {
                            FirstName = "Nisa Nur",
                            LastName = "Işık",
                            RoleId = 2,
                            Email = "nisa@info.com",
                            Password = "123",
                        },
                        new User
                        {
                            FirstName = "Elisa",
                            LastName = "Aı",
                            RoleId = 3,
                            Email = "elisa@info.com",
                            Password = "123",
                        },
                        new User
                        {
                            FirstName = "Yağız",
                            LastName = "Aı",
                            RoleId = 3,
                            Email = "yagiz@info.com",
                            Password = "123",
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
                            Description = "C# course",
                            Image = "csharp.png",
                            LevelId = 1,
                            Hours = 24,
                            Rating = 5,
                            CategoryId = 1,
                            UserId = 2
                        },
                        new Course
                        {
                            Title = "Java",
                            Description = "Java course",
                            Image = "java.png",
                            LevelId = 2,
                            Hours = 32,
                            Rating = 4,
                            CategoryId = 1,
                            UserId = 2
                        },
                        new Course
                        {
                            Title = "Python",
                            Description = "Python course",
                            Image = "python.png",
                            LevelId = 3,
                            Hours = 40,
                            Rating = 4,
                            CategoryId = 1,
                            UserId = 2
                        },
                        new Course
                        {
                            Title = "Linux",
                            Description = "Linux course",
                            Image = "linux.png",
                            LevelId = 1,
                            Hours = 24,
                            Rating = 5,
                            CategoryId = 2,
                            UserId = 2
                        },
                        new Course
                        {
                            Title = "Windows",
                            Description = "Windows course",
                            Image = "windows.png",
                            LevelId = 2,
                            Hours = 32,
                            Rating = 4,
                            CategoryId = 2,
                            UserId = 3
                        },
                        new Course
                        {
                            Title = "Business",
                            Description = "Business course",
                            Image = "isletme.png",
                            LevelId = 3,
                            Hours = 40,
                            Rating = 4,
                            CategoryId = 3,
                            UserId = 3
                        },
                        new Course
                        {
                            Title = "Marketing",
                            Description = "Marketing course",
                            Image = "pazarlama.png",
                            LevelId = 1,
                            Hours = 24,
                            Rating = 5,
                            CategoryId = 3,
                            UserId = 3
                        },
                        new Course
                        {
                            Title = "UX",
                            Description = "UX course",
                            Image = "ux.png",
                            LevelId = 2,
                            Hours = 32,
                            Rating = 4,
                            CategoryId = 4,
                            UserId = 3
                        },
                        new Course
                        {
                            Title = "Career",
                            Description = "Career course",
                            Image = "kariyer.png",
                            LevelId = 3,
                            Hours = 40,
                            Rating = 4,
                            CategoryId = 5,
                            UserId = 3
                        }
                    );
                    context.SaveChanges();
                }
            }
        }
    }
}