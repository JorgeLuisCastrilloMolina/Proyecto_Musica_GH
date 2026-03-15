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

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<Proyecto_Musica_GHDbContext>();

    dbContext.Database.EnsureCreated();

    if (!dbContext.Usuarios.Any())
    {
        dbContext.Usuarios.Add(new Proyecto_Musica_GHDAL.Entidades.Usuario
        {
            Nombre = "Usuario Demo",
            Email = "demo@ucr.local",
            Password = "demo123"
        });
    }

    if (!dbContext.Albums.Any())
    {
        dbContext.Albums.Add(new Proyecto_Musica_GHDAL.Entidades.Album
        {
            Titulo = "Album Demo",
            Fecha_publicacion = "2026-03-15"
        });
    }

    dbContext.SaveChanges();
}

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
