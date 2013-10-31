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
        private static IEnumerable<MovimientoEstado> _estadosMovimiento;
        public static IEnumerable<MovimientoEstado> estadosMovimiento
        {
            get
            {
                if (_estadosMovimiento == null) _estadosMovimiento = db.MovimientoEstado;
                return _estadosMovimiento;
            }
            set
            {
                _estadosMovimiento = value;
            }
        }

        private static IEnumerable<MovimientoTipo> _tiposMovimiento;
        public static IEnumerable<MovimientoTipo> tiposMovimiento
        {
            get
            {
                if (_tiposMovimiento == null) _tiposMovimiento = db.MovimientoTipo;
                return _tiposMovimiento;
            }
            set
            {
                _tiposMovimiento = value;
            }
        }
    }
}
