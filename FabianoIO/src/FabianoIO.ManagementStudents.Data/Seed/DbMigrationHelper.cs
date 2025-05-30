﻿using FabianoIO.ManagementCourses.Domain;
using FabianoIO.ManagementStudents.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace FabianoIO.ManagementStudents.Data.Seed
{
    public static class DbMigrationHelperExtension
    {
        public static void UseDbMigrationHelper(this WebApplication app)
        {
            DbMigrationHelper.EnsureSeedData(app).Wait();
        }
    }

    public static class DbMigrationHelper
    {
        public static async Task EnsureSeedData(WebApplication application)
        {
            var services = application.Services.CreateScope().ServiceProvider;
            await EnsureSeedData(services);
        }

        public static async Task EnsureSeedData(IServiceProvider serviceProvider)
        {
            var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();

            var context = scope.ServiceProvider.GetRequiredService<StudentsContext>();

            if (env.IsDevelopment())
            {
                await context.Database.MigrateAsync();
                await EnsureSeedData(context);
            }
        }

        private static async Task EnsureSeedData(StudentsContext context)
        {
            await SeedUsers(context);
            await SeedCourses(context);
            await SeedLessons(context);
            await SeedRegistrations(context);
            await SeedCertifications(context);
        }

        public static async Task SeedUsers(StudentsContext context)
        {
            if (context.Users.Any()) return;

            #region ADMIN SEED
            var ADMIN_ROLE_ID = Guid.NewGuid();
            await context.Roles.AddAsync(new IdentityRole<Guid>
            {
                Name = "Admin",
                NormalizedName = "ADMIN",
                Id = ADMIN_ROLE_ID,
                ConcurrencyStamp = ADMIN_ROLE_ID.ToString()
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

            var user = new User(adminUser.Id, adminUser.UserName, "Admin", "Admin", adminUser.Email, DateTime.Now.AddYears(-20));
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
                SecurityStamp = Guid.NewGuid().ToString(),
            };
            user1.PasswordHash = ph.HashPassword(user1, "Teste@123");
            await context.Users.AddAsync(user1);

            var systemUser1 = new User(user1.Id, user1.UserName, "User1", "User1", user1.Email, DateTime.Now.AddYears(21));
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
                SecurityStamp = Guid.NewGuid().ToString(),
            };
            user2.PasswordHash = ph.HashPassword(user2, "Teste@123");
            await context.Users.AddAsync(user2);

            var systemUser2 = new User(user2.Id, user2.UserName, "User2", "User2", user2.Email, DateTime.Now.AddYears(-22));
            await context.SystemUsers.AddAsync(systemUser2);

            context.SaveChanges();
            #endregion
        }

        public static async Task SeedRegistrations(StudentsContext context)
        {
            if (!context.Registrations.Any())
            {
                var user1 = context.SystemUsers.FirstOrDefault(u => u.UserName.Equals("user1@fabianoio.com"));
                var user2 = context.SystemUsers.FirstOrDefault(u => u.UserName.Equals("user2@fabianoio.com"));
                var courseDominios = context.Courses.FirstOrDefault(u => u.Name.Equals("Modelagem de Dominios Ricos"));
                var courseTests = context.Courses.FirstOrDefault(u => u.Name.Equals("Dominando Testes de Software"));

                var registrations = new List<Registration>
                {
                    new(user1.Id, courseDominios.Id, DateTime.Now)
                    {
                        CreatedDate = DateTime.Now,
                        Deleted = false,
                        UpdatedDate = DateTime.Now
                    },
                    new(user1.Id, courseTests.Id, DateTime.Now)
                    {
                        CreatedDate = DateTime.Now,
                        Deleted = false,
                        UpdatedDate = DateTime.Now
                    },
                    new(user2.Id, courseDominios.Id, DateTime.Now)
                    {
                        CreatedDate = DateTime.Now,
                        Deleted = false,
                        UpdatedDate = DateTime.Now
                    },
                    new(user2.Id, courseTests.Id, DateTime.Now)
                    {
                        CreatedDate = DateTime.Now,
                        Deleted = false,
                        UpdatedDate = DateTime.Now
                    },
                };

                await context.Registrations.AddRangeAsync(registrations);
                context.SaveChanges();
            }
        }

        public static async Task SeedCertifications(StudentsContext context)
        {
            if (!context.Certifications.Any())
            {
                var user1 = context.SystemUsers.FirstOrDefault(u => u.UserName.Equals("user1@fabianoio.com"));
                var user2 = context.SystemUsers.FirstOrDefault(u => u.UserName.Equals("user2@fabianoio.com"));
                var courseDominios = context.Courses.FirstOrDefault(u => u.Name.Equals("Modelagem de Dominios Ricos"));
                var courseTests = context.Courses.FirstOrDefault(u => u.Name.Equals("Dominando Testes de Software"));

                var certifications = new List<Certification>
                {
                    new()
                    {
                        CourseId = courseDominios.Id,
                        StudentId = user1.Id,
                        CreatedDate = DateTime.Now,
                        Deleted = false,
                        UpdatedDate = DateTime.Now
                    },
                    new()
                    {
                        CourseId = courseTests.Id,
                        StudentId = user2.Id,
                        CreatedDate = DateTime.Now,
                        Deleted = false,
                        UpdatedDate = DateTime.Now
                    },
                };

                await context.Certifications.AddRangeAsync(certifications);
                context.SaveChanges();
            }
        }

        public static async Task SeedCourses(StudentsContext context)
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
                        TotalHours = 81
                    },
                   new()
                    {
                        Name = "Dominando Testes de Software",
                        CreatedDate = DateTime.Now,
                        Deleted = false,
                        UpdatedDate = DateTime.Now,
                        Description = "Este e um curso sobre testes de software",
                        TotalHours = 90
                    },
                };

                await context.Courses.AddRangeAsync(courses);
                context.SaveChanges();
            }
        }

        public static async Task SeedLessons(StudentsContext context)
        {
            if (!context.Lessons.Any())
            {
                var courseDominios = context.Courses.FirstOrDefault(u => u.Name.Equals("Modelagem de Dominios Ricos"));
                var courseTests = context.Courses.FirstOrDefault(u => u.Name.Equals("Dominando Testes de Software"));
                if (courseDominios != null)
                {
                    var lessons = new List<Lesson>
                    {
                        new("Lesson 1", "test")
                        {
                            CreatedDate = DateTime.Now,
                            Deleted = false,
                            UpdatedDate = DateTime.Now,
                            CourseId = courseDominios.Id
                        },
                       new("Lesson 2", "test")
                        {
                            CreatedDate = DateTime.Now,
                            Deleted = false,
                            UpdatedDate = DateTime.Now,
                            CourseId = courseDominios.Id
                        },
                    };

                    await context.Lessons.AddRangeAsync(lessons);
                    context.SaveChanges();
                }

                if (courseTests != null)
                {
                    var lessons = new List<Lesson>
                    {
                        new("Lesson 1", "test")
                        {
                            CreatedDate = DateTime.Now,
                            Deleted = false,
                            UpdatedDate = DateTime.Now,
                            CourseId = courseTests.Id
                        },
                       new("Lesson 2", "test")
                        {
                            CreatedDate = DateTime.Now,
                            Deleted = false,
                            UpdatedDate = DateTime.Now,
                            CourseId = courseTests.Id
                        },
                    };

                    await context.Lessons.AddRangeAsync(lessons);
                    context.SaveChanges();
                }
            }
        }
    }
}