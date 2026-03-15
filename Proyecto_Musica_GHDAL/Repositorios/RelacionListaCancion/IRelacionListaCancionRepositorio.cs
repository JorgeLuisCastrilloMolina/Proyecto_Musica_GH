using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto_Musica_GHDAL.Repositorios.RelacionListaCancion
{
    public interface IRelacionListaCancionRepositorio
    {
        List<Entidades.RelacionListaCancion> ObtenerCancionesPorPlaylist(int playlistId);
        bool ExisteRelacion(int playlistId, int cancionId);
        bool ExistePlaylist(int playlistId);
        bool ExisteCancion(int cancionId);
        bool AgregarCancionAPlaylist(Entidades.RelacionListaCancion relacion);
        bool EliminarCancionDePlaylist(int lc_rel_id);
    }

}
