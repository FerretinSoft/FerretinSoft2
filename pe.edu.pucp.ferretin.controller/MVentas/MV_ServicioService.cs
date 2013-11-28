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

        public static bool insertarServicio(Servicio servicio)
        {
            servicio.fechaRegistro = DateTime.Now;
            servicio.estado = 1;
            servicio.codigo = newCodServicio;
            db.Servicio.InsertOnSubmit(servicio);
            return enviarCambios();
        }

        public static Servicio obtenerServicioxCodigo(string codServTipoAgregar)
        {
            return db.Servicio.First(st => st.codigo.Equals(codServTipoAgregar));
        }

        public static ServicioTipo obtenerServicioTipoxCodigo(string codServTipoAgregar)
        {
            return db.ServicioTipo.First(st => st.codigo.Equals(codServTipoAgregar));
        }

        public static string newCodServicio
        {
            get
            {
                Int64 numCodProf = db.Servicio.Count() + 1;
                string codDev = Convert.ToString(numCodProf);
                while (true)
                {
                    if (codDev.Length == 8)
                        break;
                    else
                        codDev = "0" + codDev;
                }
                return "SERV-" + codDev + "-" + DateTime.Now.Year.ToString();
            }
        }
    }
}
