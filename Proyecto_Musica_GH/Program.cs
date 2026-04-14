

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

//  HttpClient para consumir la Minimal API
builder.Services.AddHttpClient<Proyecto_Musica_GH.Services.LoginApiClient>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7026"); 
});

//  Controladores con vistas
builder.Services.AddControllersWithViews();

//  Base de datos
builder.Services.AddDbContext<Proyecto_Musica_GHDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

//  Cache y configuración de sesión
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // tiempo de expiración
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddHttpContextAccessor();

//  Servicios y repositorios
builder.Services.AddScoped<ICancionRepositorio, CancionRepositorio>();
builder.Services.AddScoped<ICancionServicio, CancionServicio>();

builder.Services.AddScoped<IPlaylistRepositorio, PlaylistRepositorio>();
builder.Services.AddScoped<IPlaylistServicio, PlaylistServicio>();

builder.Services.AddScoped<IRelacionListaCancionRepositorio, RelacionListaCancionRepositorio>();
builder.Services.AddScoped<IRelacionListaCancionServicio, RelacionListaCancionServicio>();

builder.Services.AddScoped<IReproductorServicio, ReproductorServicio>();

builder.Services.AddScoped<IAlbumRepositorio, AlbumRepositorio>();
builder.Services.AddScoped<IAlbumServicio, AlbumServicio>();

builder.Services.AddScoped<IArtistaRepositorio, ArtistaRepositorio>();
builder.Services.AddScoped<IArtistaServicio, ArtistaServicio>();

//  AutoMapper
builder.Services.AddAutoMapper(cfg => { }, typeof(MapeoClases));

var app = builder.Build();

//  Inicialización de datos demo
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

//  Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

//  Activar sesión en el pipeline
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
