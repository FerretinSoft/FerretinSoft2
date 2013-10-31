using pe.edu.pucp.ferretin.model;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pe.edu.pucp.ferretin.controller.MAlmacen
{
    /// <summary>
    /// Esta Clase contiene todos los Servicios que serán ofrecidos a otros Módulos
    /// </summary>
    public class MA_SharedService : ComunService
    {
        private static List<MovimientoEstado> _estadosMovimiento;
        public static List<MovimientoEstado> estadosMovimiento
        {
            get
            {
                if (_estadosMovimiento == null) _estadosMovimiento = db.MovimientoEstado.ToList();
                return _estadosMovimiento;
            }
            set
            {
                _estadosMovimiento = value;
            }
        }

        private static IEnumerable<MovimientoTipo> _tiposMovimientos;

        public static IEnumerable<MovimientoTipo> tiposMovimientos
        {
            get
            {
                if (_tiposMovimientos == null)
                {
                    _tiposMovimientos = db.MovimientoTipo;
                }
                //pesimista
                db.Refresh(RefreshMode.OverwriteCurrentValues, _tiposMovimientos);
                return _tiposMovimientos;
            }
            set
            {
                _tiposMovimientos = value;
            }
        }

        private static IEnumerable<SolicitudAbastecimientoEstado> _estadosSolicitud;
        public static IEnumerable<SolicitudAbastecimientoEstado> estadosSolicitud
        {
            get
            {
                if (_estadosSolicitud == null) _estadosSolicitud = db.SolicitudAbastecimientoEstado.ToList();
                return _estadosSolicitud;
            }
            set
            {
                _estadosSolicitud = value;
            }
        }
    }
}
