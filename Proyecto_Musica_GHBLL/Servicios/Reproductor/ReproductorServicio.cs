using AutoMapper;
using Proyecto_Musica_GHBLL.Dtos.Cancion;
using Proyecto_Musica_GHDAL.Repositorios.Cancion;
using System.Collections.Generic;
using System.Linq;

namespace Proyecto_Musica_GHBLL.Servicios.Reproductor
{
    public class ReproductorServicio : IReproductorServicio
    {
        private readonly ICancionRepositorio _cancionRepo;
        private readonly IMapper _mapper;

        public ReproductorServicio(ICancionRepositorio cancionRepo, IMapper mapper)
        {
            _cancionRepo = cancionRepo;
            _mapper = mapper;
        }

        public List<Proyecto_Musica_GHDAL.Entidades.Cancion> ObtenerListaOrdenada()
        {
            return _cancionRepo.ObtenerCanciones()
                               .OrderBy(c => c.Cancion_ID)
                               .ToList();
        }

        public CancionDto Mapear(Proyecto_Musica_GHDAL.Entidades.Cancion cancion)
        {
            return _mapper.Map<CancionDto>(cancion);
        }
    }
}