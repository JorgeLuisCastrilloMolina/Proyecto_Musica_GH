using Microsoft.EntityFrameworkCore;
using Proyecto_Musica_GHBLL;
using Proyecto_Musica_GHBLL.Servicios.Artista;
using Proyecto_Musica_GHBLL.Servicios.Album;
using Proyecto_Musica_GHBLL.Servicios.Cancion;
using Proyecto_Musica_GHBLL.Servicios.Playlist;
using Proyecto_Musica_GHBLL.Servicios.RelacionListaCancion;
using Proyecto_Musica_GHBLL.Servicios.Reproductor;
using Proyecto_Musica_GHDAL.Data;
using Proyecto_Musica_GHDAL.Repositorios.Artista;
using Proyecto_Musica_GHDAL.Repositorios.Album;
using Proyecto_Musica_GHDAL.Repositorios.Cancion;
using Proyecto_Musica_GHDAL.Repositorios.Playlist;
using Proyecto_Musica_GHDAL.Repositorios.RelacionListaCancion;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// BASE DE DATOS
builder.Services.AddDbContext<Proyecto_Musica_GHDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSession();

builder.Services.AddHttpContextAccessor();

//Canción
builder.Services.AddScoped<ICancionRepositorio, CancionRepositorio>();
builder.Services.AddScoped<ICancionServicio, CancionServicio>();

//Playlist
builder.Services.AddScoped<IPlaylistRepositorio, PlaylistRepositorio>();
builder.Services.AddScoped<IPlaylistServicio, PlaylistServicio>();

//Relación Lista-Canción
builder.Services.AddScoped<IRelacionListaCancionRepositorio, RelacionListaCancionRepositorio>();
builder.Services.AddScoped<IRelacionListaCancionServicio, RelacionListaCancionServicio>();

// Reproductor
builder.Services.AddScoped<IReproductorServicio, ReproductorServicio>();

// Album
builder.Services.AddScoped<IAlbumRepositorio, AlbumRepositorio>();
builder.Services.AddScoped<IAlbumServicio, AlbumServicio>();

// Artista
builder.Services.AddScoped<IArtistaRepositorio, ArtistaRepositorio>();
builder.Services.AddScoped<IArtistaServicio, ArtistaServicio>();

// AutoMapper
builder.Services.AddAutoMapper(cfg => { }, typeof(MapeoClases));

var app = builder.Build();

app.UseSession();

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

    if (!dbContext.Artistas.Any())
    {
        dbContext.Artistas.Add(new Proyecto_Musica_GHDAL.Entidades.Artista
        {
            Nombre = "Artista Demo",
            Biografia = "Artista creado para pruebas locales del proyecto."
        });
        dbContext.SaveChanges();
    }

    if (!dbContext.Albums.Any())
    {
        var artistaDemoId = dbContext.Artistas
            .OrderBy(a => a.Artista_ID)
            .Select(a => a.Artista_ID)
            .FirstOrDefault();

        dbContext.Albums.Add(new Proyecto_Musica_GHDAL.Entidades.Album
        {
            Titulo = "Album Demo",
            Fecha_publicacion = "2026-03-15",
            Artista_ID = artistaDemoId
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
