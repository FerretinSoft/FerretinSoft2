using pe.edu.pucp.ferretin.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace pe.edu.pucp.ferretin.controller
{
    /// <summary>
    /// Esta es la Clase Común de Servicios de la cual todos los servicios deben Heredar
    /// para poder obtener los Datos Comunes en cualquier Clase Servicio de la Aplicación
    /// </summary>
    public partial class ComunService : BaseService
    {

        private static IEnumerable<UbigeoDepartamento> _departamentos;
        /// <summary>
        /// Obtiene una lista de todos los Departamentos contenidos en la Base de Datos
        /// </summary>
        public static IEnumerable<UbigeoDepartamento> departamentos
        {
            get
            {
                if (_departamentos == null)
                    _departamentos = from d in db.UbigeoDepartamento select d;
                return _departamentos;
            }
        }

        private static IEnumerable<UbigeoProvincia> _provincias;
        /// <summary>
        /// Obtiene una lista de todos las Provincias contenidas en la Base de Datos
        /// </summary>
        public static IEnumerable<UbigeoProvincia> provincias
        {
            get
            {
                if (_provincias == null)
                    _provincias = from p in db.UbigeoProvincia select p;
                return _provincias;
            }
        }
        private static IEnumerable<UbigeoDistrito> _distritos;
        /// <summary>
        /// Obtiene una lista de todos los Distritos contenidos en la Base de Datos
        /// </summary>
        public static IEnumerable<UbigeoDistrito> distritos
        {
            get
            {
                if (_distritos == null)
                    _distritos = from d in db.UbigeoDistrito select d;
                return _distritos;
            }
        }

        private static IEnumerable<UnidadMedida> _unidadesMedida;

        /// <summary>
        /// Obtiene una lista de las Unidades de Medidas contenidas en la Base de datos
        /// </summary>
        public static IEnumerable<UnidadMedida> unidadesMedida
        {
            get
            {
                if (_unidadesMedida == null)
                {
                    _unidadesMedida = from u in db.UnidadMedida select u;
                }
                return _unidadesMedida;
            }
        }
        /*
        private static IEnumerable<Dia> _dias;
        public static IEnumerable<Dia> dias
        {
            get
            {
                if (_dias == null) _dias = from d in db.Dia select d;
                return _dias;
            }
        }
        */
        private static IEnumerable<Tienda> _almacenes;
        public static IEnumerable<Tienda> almacenes
        {
            get
            {
                if(_almacenes==null)_almacenes = db.Tienda.Where(t => t.tipo == 1);
                return _almacenes;
            }
            set
            {
                _almacenes = value;
            }
        }
        /************************************************/
        public static string userlog;
        public static void usuarioLogeado (Usuario user)
        {
             userlog = user.nombre;
        }
        /************************************************/
        public static int idVent;
        public static void idVentana(int id)
        {
            idVent = id;
            Console.WriteLine("VALOR DE IDVENT lslsl : " + id + " -- " + idVent);
        }
        /************************************************/
        private static IEnumerable<Tienda> _tiendas;
        public static IEnumerable<Tienda> tiendas
        {
            get
            {
                if (_tiendas == null) _tiendas = db.Tienda;
                return _tiendas;
            }
            set
            {
                _tiendas = value;
            }
        }

        //Usuario Logueado al sistema.
        public static Usuario usuarioL;
        public static void usuarioLo(Usuario user)
        {
            usuarioL = user;
        }
        /*********************************************/
        //Tienda que esta siendo agregada al sistema.
        public static Tienda tienda;
        public static void tiendaAg(Tienda tnd)
        {            
            tienda = tnd;
        }
        /*********************************************/
        //Permisos del usuario logueado al sistema.
        public static List<PerfilMenu> usuarioLpermisos;
        public static void obtenerPermisos(Usuario usuario)
        {
            IEnumerable<PerfilMenu> permisos = null;

            permisos = db.PerfilMenu.Where(t => t.id_perfil == usuario.id_perfil);
            usuarioLpermisos = permisos.OrderBy(p => p.id_menu).ToList(); ;

        }

    }
}
