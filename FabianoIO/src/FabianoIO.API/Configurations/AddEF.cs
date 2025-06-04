using FabianoIO.Core.Enums;
using FabianoIO.ManagementCourses.Data;
using FabianoIO.ManagementPayments.Data;
using FabianoIO.ManagementStudents.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FabianoIO.API.Configurations
{
    public static class AddEF
    {
        public static WebApplicationBuilder AddContext(this WebApplicationBuilder builder, EDatabases databases)
        {
            switch (databases)
            {
                case EDatabases.SQLServer:
                    builder.Services.AddDbContext<CoursesContext>(opt =>
                    {
                        opt.UseSqlServer(builder.Configuration.GetConnectionString("SQLServer"));
                    });
                    builder.Services.AddDbContext<StudentsContext>(options =>
                        options.UseSqlServer(builder.Configuration.GetConnectionString("SQLServer"))
                    );
                    builder.Services.AddDbContext<PaymentsContext>(options =>
                        options.UseSqlServer(builder.Configuration.GetConnectionString("SQLServer"))
                    );
                    break;

                case EDatabases.SQLite:
                    builder.Services.AddDbContext<CoursesContext>(opt =>
                    {
                        opt.UseSqlServer(builder.Configuration.GetConnectionString("SQLite"));
                    });
                    builder.Services.AddDbContext<StudentsContext>(options =>
                        options.UseSqlite(builder.Configuration.GetConnectionString("SQLite"))
                    );
                    builder.Services.AddDbContext<PaymentsContext>(options =>
                       options.UseSqlite(builder.Configuration.GetConnectionString("SQLite"))
                    );
                    break;

                default:
                    throw new ArgumentException($"Banco de dados {databases} não suportado.");
            }

            builder.Services.AddDefaultIdentity<IdentityUser<Guid>>()
              .AddRoles<IdentityRole<Guid>>()
              .AddEntityFrameworkStores<StudentsContext>()
              .AddSignInManager()
              .AddRoleManager<RoleManager<IdentityRole<Guid>>>()
              .AddDefaultTokenProviders();

            return builder;
        }
    }
}
