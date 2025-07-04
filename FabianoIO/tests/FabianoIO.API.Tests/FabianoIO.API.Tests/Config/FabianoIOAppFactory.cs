﻿using FabianoIO.ManagementCourses.Data;
using FabianoIO.ManagementCourses.Domain;
using FabianoIO.ManagementPayments.Data;
using FabianoIO.ManagementStudents.Data;
using FabianoIO.ManagementStudents.Domain;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using static System.Net.Mime.MediaTypeNames;

namespace FabianoIO.Api.Tests.Config
{
    public class FabianoIOAppFactory : WebApplicationFactory<Program>
    {
        private SqliteConnection _connection;

        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.UseEnvironment("Testing");

            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();

            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<StudentsContext>));
                if (descriptor != null)
                    services.Remove(descriptor);

                services.AddDbContext<StudentsContext>(options =>
                {
                    options.UseSqlite(_connection);
                });

                services.AddDbContext<CoursesContext>(options =>
                {
                    options.UseSqlite(_connection);
                });

                services.AddDbContext<PaymentsContext>(options =>
                {
                    options.UseSqlite(_connection);
                });

                var sp = services.BuildServiceProvider();
                using var scope = sp.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<StudentsContext>();
                var dbCourses = scope.ServiceProvider.GetRequiredService<CoursesContext>();
                var dbPayments = scope.ServiceProvider.GetRequiredService<PaymentsContext>();

                db.Database.EnsureCreated();
                dbCourses.Database.Migrate();
                dbPayments.Database.Migrate();

                Task.Run(async () =>
                {
                    await SeedTestUsers(db);
                }).GetAwaiter().GetResult();

                Task.Run(async () =>
                {
                    await SeedTestCourses(dbCourses);
                }).GetAwaiter().GetResult();

                Task.Run(async () =>
                {
                    await SeedTestLessons(dbCourses);
                }).GetAwaiter().GetResult();
            });

            return base.CreateHost(builder);
        }

        protected static async Task SeedTestUsers(StudentsContext context)
        {
            if (context.Users.Any()) return;

            #region ADMIN SEED
            var ADMIN_ROLE_ID = Guid.NewGuid();
            await context.Roles.AddAsync(new IdentityRole<Guid>
            {
                Name = "ADMIN",
                NormalizedName = "ADMIN",
                Id = ADMIN_ROLE_ID,
                ConcurrencyStamp = ADMIN_ROLE_ID.ToString()
            });

            var STUDENT_ROLE_ID = Guid.NewGuid();
            await context.Roles.AddAsync(new IdentityRole<Guid>
            {
                Name = "STUDENT",
                NormalizedName = "STUDENT",
                Id = STUDENT_ROLE_ID,
                ConcurrencyStamp = STUDENT_ROLE_ID.ToString()
            });

            var ADMIN_ID = Guid.NewGuid();
            var adminUser = new IdentityUser<Guid>
            {
                Id = ADMIN_ID,
                Email = "admin@fabianoio.com",
                EmailConfirmed = true,
                UserName = "admin@fabianoio.com",
                NormalizedUserName = "admin@fabianoio.com".ToUpper(),
                NormalizedEmail = "admin@fabianoio.com".ToUpper(),
                LockoutEnabled = true,
                SecurityStamp = ADMIN_ROLE_ID.ToString(),
            };

            //set user password
            PasswordHasher<IdentityUser<Guid>> ph = new PasswordHasher<IdentityUser<Guid>>();
            adminUser.PasswordHash = ph.HashPassword(adminUser, "Teste@123");
            await context.Users.AddAsync(adminUser);

            var user = new User(adminUser.Id, adminUser.UserName, "Admin", "Admin", adminUser.Email, DateTime.Now.AddYears(-20), true);
            await context.SystemUsers.AddAsync(user);

            await context.UserRoles.AddAsync(new IdentityUserRole<Guid>
            {
                RoleId = ADMIN_ROLE_ID,
                UserId = ADMIN_ID
            });

            context.SaveChanges();
            #endregion

            #region NON-ADMIN USERS SEED
            var user1Id = Guid.NewGuid();
            var user1 = new IdentityUser<Guid>
            {
                Id = user1Id,
                Email = "user1@fabianoio.com",
                EmailConfirmed = true,
                UserName = "user1@fabianoio.com",
                NormalizedUserName = "user1@fabianoio.com".ToUpper(),
                NormalizedEmail = "user1@fabianoio.com".ToUpper(),
                LockoutEnabled = true,
                SecurityStamp = user1Id.ToString(),
            };
            user1.PasswordHash = ph.HashPassword(user1, "Teste@123");
            await context.Users.AddAsync(user1);

            var systemUser1 = new User(user1.Id, user1.UserName, "User1", "User1", user1.Email, DateTime.Now.AddYears(21), false);
            await context.SystemUsers.AddAsync(systemUser1);

            var user2Id = Guid.NewGuid();
            var user2 = new IdentityUser<Guid>
            {
                Id = user2Id,
                Email = "user2@fabianoio.com",
                EmailConfirmed = true,
                UserName = "user2@fabianoio.com",
                NormalizedUserName = "user2@fabianoio.com".ToUpper(),
                NormalizedEmail = "user2@fabianoio.com".ToUpper(),
                LockoutEnabled = true,
                SecurityStamp = user2Id.ToString(),
            };
            user2.PasswordHash = ph.HashPassword(user2, "Teste@123");
            await context.Users.AddAsync(user2);

            await context.UserRoles.AddAsync(new IdentityUserRole<Guid>
            {
                RoleId = STUDENT_ROLE_ID,
                UserId = user1Id
            });

            await context.UserRoles.AddAsync(new IdentityUserRole<Guid>
            {
                RoleId = STUDENT_ROLE_ID,
                UserId = user2Id
            });


            var systemUser2 = new User(user2.Id, user2.UserName, "User2", "User2", user2.Email, DateTime.Now.AddYears(-22), false);
            await context.SystemUsers.AddAsync(systemUser2);

            context.SaveChanges();
            #endregion
        }

        public static async Task SeedTestCourses(CoursesContext context)
        {
            if (!context.Courses.Any())
            {
                var courses = new List<Course>
                {
                    new()
                    {
                        Name = "Modelagem de Dominios Ricos",
                        CreatedDate = DateTime.Now,
                        Deleted = false,
                        UpdatedDate = DateTime.Now,
                        Description = "Este e um curso sobre modelagem de dominios ricos",
                        Price = 500
                    },
                   new()
                    {
                        Name = "Dominando Testes de Software",
                        CreatedDate = DateTime.Now,
                        Deleted = false,
                        UpdatedDate = DateTime.Now,
                        Description = "Este e um curso sobre testes de software",
                        Price = 350
                    },
                };

                await context.Courses.AddRangeAsync(courses);
                context.SaveChanges();
            }
        }

        public static async Task SeedTestLessons(CoursesContext context)
        {
            if (!context.Lessons.Any())
            {
                var courseDominios = context.Courses.FirstOrDefault(u => u.Name.Equals("Modelagem de Dominios Ricos"));
                var courseTests = context.Courses.FirstOrDefault(u => u.Name.Equals("Dominando Testes de Software"));
                if (courseDominios != null)
                {
                    var lessons = new List<Lesson>
                    {
                        new("Lesson 1", "Aula de dominios ricos 1", 80, courseDominios.Id),
                       new("Lesson 2", "Aulas de dominios ricos 2", 75, courseDominios.Id),
                    };

                    await context.Lessons.AddRangeAsync(lessons);
                    context.SaveChanges();
                }

                if (courseTests != null)
                {
                    var lessons = new List<Lesson>
                    {
                       new("Lesson 1", "Aula de testes 1", 60, courseTests.Id),
                       new("Lesson 2", "Aula de testes 2", 55, courseTests.Id)
                    };

                    await context.Lessons.AddRangeAsync(lessons);
                    context.SaveChanges();
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            _connection?.Close();
            _connection?.Dispose();
        }
    }
}
