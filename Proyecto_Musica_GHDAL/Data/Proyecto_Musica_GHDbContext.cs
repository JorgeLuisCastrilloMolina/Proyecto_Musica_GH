using Microsoft.EntityFrameworkCore;
using Proyecto_Musica_GHDAL.Entidades;

namespace Proyecto_Musica_GHDAL.Data
{
    public partial class Proyecto_Musica_GHDbContext : DbContext
    {
        public Proyecto_Musica_GHDbContext()
        {
        }

        public Proyecto_Musica_GHDbContext(DbContextOptions<Proyecto_Musica_GHDbContext> options)
            : base(options)
        {
        }

        // DbSet para Playlist
        public virtual DbSet<Playlist> Playlists { get; set; }

        // DbSet para Cancion
        public virtual DbSet<Cancion> Canciones { get; set; }

        // DbSet para Albums
        public virtual DbSet<Album> Albums { get; set; }

        // DbSet para RelacionListaCancion
        public virtual DbSet<RelacionListaCancion> RelacionesListaCancion { get; set; }

        // DbSet para Usuario
        public virtual DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de Playlist
            modelBuilder.Entity<Playlist>(entity =>
            {
                entity.ToTable("PLAYLIST");
                entity.HasKey(e => e.Playlist_ID);

                entity.Property(e => e.Nombre);
                entity.Property(e => e.Fecha_creacion);

                entity.HasOne(e => e.Usuario)   // propiedad de navegación opcional
                      .WithMany(u => u.Playlists)
                      .HasForeignKey(e => e.Usuario_ID);
            });

            // Configuración de Usuario
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("USUARIO");
                entity.HasKey(e => e.Usuario_ID);

                entity.Property(e => e.Nombre);
                entity.Property(e => e.Email);
                entity.Property(e => e.Password);


                entity.HasMany(e => e.Playlists)
                      .WithOne(p => p.Usuario)
                      .HasForeignKey(p => p.Usuario_ID);
            });

            // Configuración de Cancion
            modelBuilder.Entity<Cancion>(entity =>
            {
                entity.ToTable("CANCION");
                entity.HasKey(e => e.Cancion_ID);

                entity.Property(e => e.Titulo);
                entity.Property(e => e.Fecha_publicacion);
                entity.Property(e => e.Duracion);
                entity.Property(e => e.URL_cancion);

                entity.HasOne(e => e.Album)
                      .WithMany()
                      .HasForeignKey(e => e.Album_ID);
            });

            // Configuración de RelacionListaCancion
            modelBuilder.Entity<RelacionListaCancion>(entity =>
            {
                entity.ToTable("RELACION_LISTA_CANCION");
                entity.HasKey(e => e.LC_REL_ID);

                entity.Property(e => e.Orden);

                entity.HasOne(e => e.Playlist)
                      .WithMany(p => p.Canciones)
                      .HasForeignKey(e => e.Playlist_ID);

                entity.HasOne(e => e.Cancion)
                      .WithMany()
                      .HasForeignKey(e => e.Cancion_ID);
            });
            // Configuración de Album
            modelBuilder.Entity<Album>(entity =>
            {
                entity.ToTable("ALBUM");
                entity.HasKey(e => e.Album_ID); 

                entity.Property(e => e.Titulo);
                entity.Property(e => e.Fecha_publicacion);

                entity.HasMany(e => e.Canciones)
                      .WithOne(c => c.Album)
                      .HasForeignKey(c => c.Album_ID);
            });


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}