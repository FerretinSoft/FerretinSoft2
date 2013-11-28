using pe.edu.pucp.ferretin.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pe.edu.pucp.ferretin.controller.MVentas
{
    public class MV_ServicioService : MV_ComunService
    {

        public static IEnumerable<model.Servicio> buscarServicios(string codServSearch, DateTime fechaDesdeSearch, DateTime fechaHastaSearch, int estadoSearch)
        {
            return db.Servicio;
        }

        public static bool insertarServicio(model.Servicio servicio)
        {
            throw new NotImplementedException();
        }

        public static ServicioTipo obtenerServicioxCodigo(string codServTipoAgregar)
        {
            throw new NotImplementedException();
        }
    }
}
