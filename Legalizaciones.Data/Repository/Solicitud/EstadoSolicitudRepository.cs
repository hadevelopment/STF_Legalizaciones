using Legalizaciones.Interface;
using Legalizaciones.Model.ItemSolicitud;
using Legalizaciones.Data.AppDbContext;
using Legalizaciones.Data.Repository.Base;
using Legalizaciones.Interface.ISolicitud;

namespace Legalizaciones.Data.Repository
{
    public class EstadoSolicitudRepository : BaseRepository<EstadoSolicitud>, IEstadoSolicitudRepository
    {
        public EstadoSolicitudRepository(AppDataContext context) : base(context)
        {

        }

    }
}
