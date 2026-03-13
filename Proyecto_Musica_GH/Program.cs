using Proyecto_Musica_GHBLL;
using Proyecto_Musica_GHBLL.Servicios.Cancion;
using Proyecto_Musica_GHBLL.Servicios.Playlist;
using Proyecto_Musica_GHBLL.Servicios.RelacionListaCancion;
using Proyecto_Musica_GHDAL.Data;
using Proyecto_Musica_GHDAL.Repositorios.Cancion;
using Proyecto_Musica_GHDAL.Repositorios.Playlist;
using Proyecto_Musica_GHDAL.Repositorios.RelacionListaCancion;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Configuración de DbContext con SQLite
builder.Services.AddDbContext<Proyecto_Musica_GHDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositorios y servicios de Canción
builder.Services.AddScoped<ICancionRepositorio, CancionRepositorio>();
builder.Services.AddScoped<ICancionServicio, CancionServicio>();

// Repositorios y servicios de Playlist
builder.Services.AddScoped<IPlaylistRepositorio, PlaylistRepositorio>();
builder.Services.AddScoped<IPlaylistServicio, PlaylistServicio>();

// Repositorios y servicios de RelacionListaCancion
builder.Services.AddScoped<IRelacionListaCancionRepositorio, RelacionListaCancionRepositorio>();
builder.Services.AddScoped<IRelacionListaCancionServicio, RelacionListaCancionServicio>();

// AutoMapper con perfil de mapeo
builder.Services.AddAutoMapper(cfg => { }, typeof(MapeoClases));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();