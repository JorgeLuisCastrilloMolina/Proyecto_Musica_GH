using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Login_MinimalAPI.Data
{ 

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>()
                .HasKey(u => u.Usuario_ID);

            modelBuilder.Entity<Usuario>()
                .ToTable("USUARIO"); // <-- aquí le dices a EF que use la tabla USUARIO
        }


    }


}