using MediatR;
using WebArchitecturesExamples.Clean.Application.Commands;
using WebArchitecturesExamples.Clean.Application.Interfaces;
using WebArchitecturesExamples.Clean.Presentation.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblyContaining<StartAttendanceSessionCommand>();
});

builder.Services.AddSingleton<IAttendanceRepository, InMemoryAttendanceRepository>();
builder.Services.AddSingleton<IStudentRepository, InMemoryStudentRepository>();
builder.Services.AddSingleton<IStudentNotifier, InMemoryStudentNotifier>();

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