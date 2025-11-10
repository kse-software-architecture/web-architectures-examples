using ThreeLayered.Application.Interfaces;
using ThreeLayered.Application.Services;
using ThreeLayered.DataLayer.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IAttendanceService, AttendanceService>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddSingleton<IAttendanceRepository, InMemoryAttendanceRepository>();
builder.Services.AddSingleton<IStudentRepository, InMemoryStudentRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();