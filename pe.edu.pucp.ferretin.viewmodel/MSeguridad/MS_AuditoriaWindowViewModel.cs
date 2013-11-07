using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

using pe.edu.pucp.ferretin.controller;
using pe.edu.pucp.ferretin.controller.MSeguridad;
using pe.edu.pucp.ferretin.controller.MRecursosHumanos;
using pe.edu.pucp.ferretin.model;
using pe.edu.pucp.ferretin.viewmodel.Helper;

namespace pe.edu.pucp.ferretin.viewmodel.MSeguridad
{
    public class MS_AuditoriaWindowViewModel : ViewModelBase
    {
        #region Valores para el cuadro de Búsqueda        
        private String _searchNombreUsuario = null;
        public String searchNombreUsuario { get { return _searchNombreUsuario; } set { _searchNombreUsuario = value; } }

        private Perfil _searchPerfil = null;
        public Perfil searchPerfil { get { return _searchPerfil; } set { _searchPerfil = value; NotifyPropertyChanged("searchPerfil"); } }
        
        public DateTime? _searchFechaDesde = null;
        public DateTime? searchFechaDesde { get { return _searchFechaDesde; } set { _searchFechaDesde = value; NotifyPropertyChanged("searchFechaDesde"); } }

        public DateTime? _searchFechaHasta = null;
        public DateTime? searchFechaHasta { get { return _searchFechaHasta; } set { _searchFechaHasta = value; NotifyPropertyChanged("searchFechaHasta"); } }
        #endregion

        #region Manejo de los Tabs
        /************************************************/
        public enum tabs
        {
            //Pestañas virtuales:
            //0       1        2          3
            BUSQUEDA, AGREGAR, MODIFICAR, DETALLES
        }
        /************************************************/
        private tabs _statusTab = tabs.BUSQUEDA; //pestaña default 
        public tabs statusTab
        {
            get
            {
                return _statusTab;
            }
            set
            {
                _statusTab = value;
                //Si cambió el estado de las pestañas también cambio los Header
                //Si la pestaña es para agregar nuevo, limpio los input
                switch (value)
                {
                    case tabs.BUSQUEDA: detallesTabHeader = "Agregar"; transaccion = new Transaccion(); break;//Si es agregar, creo un nuevo objeto Usuario
                    case tabs.AGREGAR: detallesTabHeader = "Agregar"; transaccion = new Transaccion(); break;//Si es agregar, creo un nuevo objeto Usuario
                    case tabs.MODIFICAR: detallesTabHeader = "Modificar"; break;
                    case tabs.DETALLES: detallesTabHeader = "Detalles"; break;
                    default: detallesTabHeader = "Agregar"; transaccion = new Transaccion(); break;//Si es agregar, creo un nuevo objeto Cliente
                }
                //Cuando se cambia el status, tambien se tiene que cambiar el currentIndex del tab
                //currentIndexTab = _statusTab == 0 ? 0 : 1;
                NotifyPropertyChanged("statusTab");
                //Cuando se cambia el status, tambien se tiene que actualizar el currentIndex del tab
                NotifyPropertyChanged("currentIndexTab"); //Hace que cambie el tab automaticamente
                NotifyPropertyChanged("editEmpleadoEnabled");
            }
        }
        /************************************************/
        //Usado para mover los tabs de acuerdo a las acciones realizadas
        public int currentIndexTab
        {
            get { return _statusTab == tabs.BUSQUEDA ? 0 : 1; }
            set { statusTab = value == 0 ? tabs.BUSQUEDA : tabs.AGREGAR; }

        }
        /************************************************/
        private String _detallesTabHeader = "Agregar"; //Default
        public String detallesTabHeader
        {
            get
            {
                return _detallesTabHeader;
            }
            set
            {
                _detallesTabHeader = value;
                NotifyPropertyChanged("detallesTabHeader");
            }
        }
        /************************************************/
        #endregion

        #region Lista de Transacciones y Edición de Transacciones
        /**************************************************/
        private Transaccion _transaccion = new Transaccion();
        public Transaccion transaccion
        {
            get
            {
                return _transaccion;
            }
            set
            {
                _transaccion = value;               
                NotifyPropertyChanged("transaccion");
            }
        }
        /**************************************************/
        public IEnumerable<Perfil> perfiles
        {
            get
            {
                //Creo una nueva secuencia
                var sequence = Enumerable.Empty<Perfil>();
                //Primero agrego un item de Todos para que salga al inicio
                //Pongo el ID en 0 para que al buscar, no filtre nada cuando se selecciona todos
                IEnumerable<Perfil> items = new Perfil[] { new Perfil { id = 0, nombre = "Todos" } };
                //Luego concateno el itemcon los elementos del combobox
                return items.Concat(MS_UsuarioService.obtenerPerfiles());
            }
        }
        /**************************************************/
        private IEnumerable<Transaccion> _listaTransacciones;
        public IEnumerable<Transaccion> listaTransacciones
        {
            get
            {
                _listaTransacciones = MS_TransaccionService.buscar(searchNombreUsuario, searchPerfil, searchFechaDesde, searchFechaHasta);
                return _listaTransacciones;
            }
            set
            {
                _listaTransacciones = value;
                NotifyPropertyChanged("listaTransacciones");
            }
        }
        /**************************************************/
        #endregion

        #region RalayCommand
        RelayCommand _actualizarListaTransaccionesCommand;
        public ICommand actualizarListaTransaccionesCommand
        {
            get
            {
                if (_actualizarListaTransaccionesCommand == null)
                {
                    _actualizarListaTransaccionesCommand = new RelayCommand(param => NotifyPropertyChanged("listaTransacciones"));
                }
                return _actualizarListaTransaccionesCommand;
            }
        }
        #endregion

    }
}
