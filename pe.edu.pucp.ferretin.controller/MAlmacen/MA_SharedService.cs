using pe.edu.pucp.ferretin.model;
using System;
using System.Collections.Generic;
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

        private static List<MovimientoTipo> _tiposMovimiento;
        public static List<MovimientoTipo> tiposMovimiento
        {
            get
            {
                if (_tiposMovimiento == null) _tiposMovimiento = db.MovimientoTipo.ToList();
                return _tiposMovimiento;
            }
            set
            {
                _tiposMovimiento = value;
            }
        }

        private static List<SolicitudAbastecimientoEstado> _estadosSolicitud;
        public static List<SolicitudAbastecimientoEstado> estadosSolicitud
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
