using FabianoIO.API.Configurations;
using FabianoIO.Core.Enums;
using FabianoIO.ManagementStudents.Application.Commands;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IRequestHandler<AddUserCommand, bool>, UserCommandHandler>();
builder.AddContext(EDatabases.SQLServer);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
