using Server.Repositories;
using Server.Contexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
string _connectionstring = "server=api-db;database=appdatabase;user=apiuser;password=apipassword;"; //builder.Configuration["ConnectionStrings:DefaultConnection"];
builder.Services.AddDbContext<SQLContext>(opt => opt.UseMySql(_connectionstring, ServerVersion.AutoDetect(_connectionstring)));
builder.Services.AddScoped<ITodoRepo, TodoRepo>();
builder.Services.AddScoped<INoTeRepo, NoteRepo>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
