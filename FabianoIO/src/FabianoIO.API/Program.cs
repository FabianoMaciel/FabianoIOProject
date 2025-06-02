using FabianoIO.API.Configurations;
using FabianoIO.Core.Enums;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder
    .AddJwt()
    .AddContext(EDatabases.SQLServer)
    .AddRepositories()
    .AddServices()
    .AddSwaggerConfiguration();

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
app.UseDbMigrationHelper();

app.Run();
