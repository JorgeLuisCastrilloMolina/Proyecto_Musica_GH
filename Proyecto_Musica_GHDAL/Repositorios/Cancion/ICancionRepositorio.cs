using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto_Musica_GHDAL.Repositorios.Cancion
{
    public interface ICancionRepositorio
    {
        List<Entidades.Cancion> ObtenerCanciones();
        Entidades.Cancion ObtenerCancionPorId(int id);
        bool AgregarCancion(Entidades.Cancion cancion);
        bool ActualizarCancion(Entidades.Cancion cancion);
        bool EliminarCancion(int id);
    }

}
