using pe.edu.pucp.ferretin.model;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pe.edu.pucp.ferretin.controller.MAlmacen
{
    public class MA_SolicitudAbastecimientoService : MA_ComunService
    {
        public static IEnumerable<SolicitudAbastecimiento> _listaSolicitudes;
        public static IEnumerable<SolicitudAbastecimiento> listaSolicitudes
        {
            get
            {
                if (_listaSolicitudes == null)
                {
                    _listaSolicitudes = db.SolicitudAbastecimiento;
                }
                //Usando concurrencia pesimista:
                ///La lista de clientes se actualizara para ver los cambios
                ///Si quisiera usar concurrencia optimista quito la siguiente linea
                db.Refresh(RefreshMode.OverwriteCurrentValues, _listaSolicitudes);
                return _listaSolicitudes;
            }
            set
            {
                _listaSolicitudes = value;
            }
        }


        public static SolicitudAbastecimiento obtenerSolicitudByCodigo(String codigo)
        {
            IEnumerable<SolicitudAbastecimiento> solicitudes = (from s in listaSolicitudes
                                           where s.codigo != null && s.codigo.Equals(codigo)
                                           select s);
            if (solicitudes.Count() > 0)
                return solicitudes.First();
            else
                return null;
        }


        public static void actualizarSolicitud(SolicitudAbastecimiento solicitud)
        {
            db.SubmitChanges();
        }

        public static IEnumerable<SolicitudAbastecimiento> buscar(SolicitudAbastecimientoEstado estado, DateTime fechaDesde, DateTime fechaHasta)
        {
            return from s in listaSolicitudes
                   where (
                       //Cada fila es un filtro
                          (estado == null || (s.SolicitudAbastecimientoEstado != null && s.SolicitudAbastecimientoEstado.Equals(estado)))
                       && (fechaDesde == null || (s.fecha != null && s.fecha >= fechaDesde))
                       && (fechaHasta == null || (s.fecha != null && s.fecha <= fechaHasta))
                    )
                   orderby s.codigo
                   select s;
        }

        public static bool insertarSolicitud(SolicitudAbastecimiento solicitud)
        {
            if (!db.SolicitudAbastecimiento.Contains(solicitud))
            {
                //if (!db.TiendaHorario.Equals(almacen.TiendaHorario))
                //    db.TiendaHorario.InsertAllOnSubmit(almacen.tiendasH);

                db.SolicitudAbastecimiento.InsertOnSubmit(solicitud);
                return enviarCambios();
            }
            else
            {
                return false;
            }
        }
    }
}
