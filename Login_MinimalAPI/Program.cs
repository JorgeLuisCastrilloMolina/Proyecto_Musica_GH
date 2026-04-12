using Microsoft.EntityFrameworkCore;
using Login_MinimalAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Conexión a SQLite usando la cadena en appsettings.json
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Crear la BD si no existe
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

// Swagger UI en desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

#region Endpoints

// Endpoint de registro
app.MapPost("/register", async (Usuario nuevoUsuario, AppDbContext db) =>
{
    db.Usuarios.Add(nuevoUsuario);
    await db.SaveChangesAsync();

    return Results.Created($"/usuarios/{nuevoUsuario.Usuario_ID}", new
    {
        message = "Usuario registrado correctamente",
        usuario = nuevoUsuario.Nombre,
        email = nuevoUsuario.Email,
        usuario_ID = nuevoUsuario.Usuario_ID   //  Esto fue lo que se agregó

    });
})
.WithName("Register")
.WithOpenApi();

// Endpoint de login
app.MapPost("/login", async (LoginRequest req, AppDbContext db) =>
{
    var user = await db.Usuarios
        .FirstOrDefaultAsync(u => u.Email == req.Email && u.Password == req.Password);

    if (user == null)
        return Results.Unauthorized();

    return Results.Ok(new
    {
        message = "Login exitoso",
        usuario = user.Nombre,
        email = user.Email,
        usuario_ID = user.Usuario_ID   //  Esto fue lo que se agregó

    });
})
.WithName("Login")
.WithOpenApi();

#endregion

app.Run();

#region Clases auxiliares

public class LoginRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}

#endregion