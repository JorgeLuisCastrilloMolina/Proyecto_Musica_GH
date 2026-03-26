using Proyecto_Musica_GHBLL.Dtos.Cancion;
using System.Collections.Generic;

namespace Proyecto_Musica_GHBLL.Servicios.Reproductor
{
    public interface IReproductorServicio
    {
        List<Proyecto_Musica_GHDAL.Entidades.Cancion> ObtenerListaOrdenada();
        CancionDto Mapear(Proyecto_Musica_GHDAL.Entidades.Cancion cancion);
    }
}