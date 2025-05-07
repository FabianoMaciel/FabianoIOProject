using FabianoIO.Core.Enums;
using FabianoIO.ManagementStudents.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

namespace FabianoIO.API.Configurations
{
    public static class AddEF
    {
        public static WebApplicationBuilder AddContext(this WebApplicationBuilder builder, EDatabases databases)
        {
            switch (databases)
            {
                case EDatabases.SQLServer:
                    builder.Services.AddDbContext<StudentsContext>(options =>
                        options.UseSqlServer(builder.Configuration.GetConnectionString("SQLServer"))
                    );
                    break;

                case EDatabases.SQLite:
                    builder.Services.AddDbContext<StudentsContext>(options =>
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
