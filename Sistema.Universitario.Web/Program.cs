
using Microsoft.EntityFrameworkCore;
using Sistema.Universitario.Infra.Context;
using Sistema.Universitario.Web.Services;
using Sistema.Universitario.Web.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<SUDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<IAlunoService, AlunoService>();
builder.Services.AddScoped<ICursoService, CursoService>();
builder.Services.AddScoped<ITurmaService, TurmaService>();
builder.Services.AddScoped<IDisciplinaService, DisciplinaService>();


var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Cursos}/{action=Index}/{id?}");

app.Run();
