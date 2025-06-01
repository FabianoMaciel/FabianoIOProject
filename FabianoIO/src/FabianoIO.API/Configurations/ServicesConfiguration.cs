using FabianoIO.API.Extensions;
using FabianoIO.Core.Interfaces;
using FabianoIO.Core.Interfaces.Services;
using FabianoIO.Core.Notifications;
using FabianoIO.ManagementStudents.Application.Commands;
using FabianoIO.ManagementStudents.Data.Repository;
using FabianoIO.ManagementStudents.Domain;
using MediatR;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace FabianoIO.API.Configurations
{
    public static class ServicesConfiguration
    {
        public static WebApplicationBuilder AddRepositories(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IUserRepository, UserRepository>();

            return builder;
        }

        public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
        {
            builder.Services.TryAddScoped<IAppUserService, AppUserService>();
            builder.Services.AddScoped<INotifier, Notifier>();
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<AddUserCommand>());
            //builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<AdicionarAulaCommand>());
            //builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<PagamentoService>());

            builder.Services.AddHttpContextAccessor();

            return builder;
        }
    }
}
