using FabianoIO.API.Extensions;
using FabianoIO.Core.Interfaces;
using FabianoIO.Core.Interfaces.Repositories;
using FabianoIO.Core.Interfaces.Services;
using FabianoIO.Core.Messages.IntegrationCommands;
using FabianoIO.Core.Notifications;
using FabianoIO.ManagementCourses.Application.Commands;
using FabianoIO.ManagementCourses.Application.Queries;
using FabianoIO.ManagementCourses.Data.Repository;
using FabianoIO.ManagementPayments.AntiCorruption;
using FabianoIO.ManagementPayments.Application.Query;
using FabianoIO.ManagementPayments.Business;
using FabianoIO.ManagementPayments.Business.Handlers;
using FabianoIO.ManagementPayments.Data.Repository;
using FabianoIO.ManagementStudents.Application.Commands;
using FabianoIO.ManagementStudents.Application.Handler;
using FabianoIO.ManagementStudents.Data.Repository;
using FabianoIO.ManagementStudents.Domain;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace FabianoIO.API.Configurations
{
    public static class ServicesConfiguration
    {
        public static WebApplicationBuilder AddRepositories(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<ICourseRepository, CourseRepository>();
            builder.Services.AddScoped<ILessonRepository, LessonRepository>();
            builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
            builder.Services.AddScoped<IRegistrationRepository, RegistrationRepository>();

            return builder;
        }

        public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
        {
            builder.Services.TryAddScoped<IAppUserService, AppUserService>();
            builder.Services.AddScoped<INotifier, Notifier>();

            builder.Services.AddScoped<IPaymentService, PaymentService>();
            builder.Services.AddScoped<IPaymentCreditCardFacade, PaymentCreditCardFacade>();
            builder.Services.AddScoped<IPayPalGateway, PayPalGateway>();

            builder.Services.AddScoped<ICourseQuery, CourseQuery>();
            builder.Services.AddScoped<ILessonQuery, LessonQuery>();
            builder.Services.AddScoped<IPaymentQuery, PaymentQuery>();
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<AddUserCommand>());
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<AddLessonCommand>());
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<AddCourseCommand>());
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<PaymentCommandHandler>());
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<RegistrationCommandHandler>());

            builder.Services.AddHttpContextAccessor();

            return builder;
        }
    }
}
