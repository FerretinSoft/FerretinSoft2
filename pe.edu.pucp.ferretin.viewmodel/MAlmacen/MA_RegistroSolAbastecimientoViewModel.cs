using pe.edu.pucp.ferretin.controller.MAlmacen;
using pe.edu.pucp.ferretin.model;
using pe.edu.pucp.ferretin.viewmodel.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace pe.edu.pucp.ferretin.viewmodel.MAlmacen
{
    public class MA_RegistroSolAbastecimientoViewModel : ViewModelBase
    {
        #region Valores para el cuadro de Búsqueda

        public SolicitudAbastecimientoEstado _searchEstado;
        public SolicitudAbastecimientoEstado searchEstado
        {
            get
            {
                return _searchEstado;
            }
            set
            {
                _searchEstado = value;
                NotifyPropertyChanged("searchEstado");
            }
        }

        public DateTime _searchFechaDesde = new DateTime(2000, 1, 1);
        public DateTime searchFechaDesde
        {
            get
            {
                return _searchFechaDesde;
            }
            set
            {
                _searchFechaDesde = value;
                NotifyPropertyChanged("searchFechaDesde");
            }
        }

        public DateTime _searchFechaHasta = DateTime.Today;
        public DateTime searchFechaHasta
        {
            get
            {
                return _searchFechaHasta;
            }
            set
            {
                _searchFechaHasta = value;
                NotifyPropertyChanged("searchFechaHasta");
            }
        }

        #endregion

        #region Manejo de los Tabs
        public enum Tab
        {
            //Pestañas virtuales:
            //0       1        2         
            BUSQUEDA, AGREGAR, DETALLES
        }
        private Tab _statusTab = Tab.BUSQUEDA; //pestaña default 
        public Tab statusTab
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
                switch (_statusTab)
                {
                    case Tab.BUSQUEDA: detallesTabHeader = "Nueva Solicitud"; solicitud = new SolicitudAbastecimiento(); solicitud.Tienda = currentTienda; break;//Si es agregar, creo un nuevo objeto Almacen
                    case Tab.AGREGAR: detallesTabHeader = "Nueva Solicitud"; solicitud = new SolicitudAbastecimiento(); solicitud.Tienda = currentTienda; break;//Si es agregar, creo un nuevo objeto Almacen
                    case Tab.DETALLES: detallesTabHeader = "Detalles"; break;
                    default: detallesTabHeader = "Nueva Solicitud"; solicitud = new SolicitudAbastecimiento(); solicitud.Tienda = currentTienda; break;//Si es agregar, creo un nuevo objeto Almacen
                }
                NotifyPropertyChanged("statusTab");
                //Cuando se cambia el status, tambien se tiene que actualizar el currentIndex del tab
                NotifyPropertyChanged("currentIndexTab"); //Hace que cambie el tab automaticamente
            }
        }
        //Usado para mover los tabs de acuerdo a las acciones realizadas
        public int currentIndexTab
        {
            get { return _statusTab == Tab.BUSQUEDA ? 0 : 1; }
            set { statusTab = value == 0 ? Tab.BUSQUEDA : Tab.AGREGAR; }
        }
        private String _detallesTabHeader = "Nueva Solicitud"; //Default
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
        #endregion

        #region Lista de Solicitudes y Edición de Solicitudes

        public Tienda currentTienda
        {
            get
            {
                return tiendas.ElementAt(0);
            }
        }

        private SolicitudAbastecimiento _solicitud;
        public SolicitudAbastecimiento solicitud
        {
            get
            {
                return _solicitud;
            }
            set
            {
                _solicitud = value;
                NotifyPropertyChanged("solicitud");
            }
        }

        private IEnumerable<SolicitudAbastecimiento> _listaSolicitudes;
        public IEnumerable<SolicitudAbastecimiento> listaSolicitudes
        {
            get
            {
                _listaSolicitudes = MA_SolicitudAbastecimientoService.buscar(currentTienda, searchEstado, searchFechaDesde, searchFechaHasta);
                return _listaSolicitudes;
            }
            set
            {
                _listaSolicitudes = value;
                NotifyPropertyChanged("listaSolicitudes");
            }
        }

        private string _codigoNuevoProducto = "";
        public string codigoNuevoProducto
        {
            get
            {
                return _codigoNuevoProducto;
            }
            set
            {
                _codigoNuevoProducto = value;
            }
        }

        public IEnumerable<SolicitudAbastecimientoProducto> solicitudNuevoProducto
        {
            get
            {
                if (solicitud.SolicitudAbastecimientoProducto != null)
                {
                    return solicitud.SolicitudAbastecimientoProducto;
                }
                else
                {
                    return new SolicitudAbastecimientoProducto[] { };
                }
            }
        }

        #endregion

        #region RelayCommand
        
        RelayCommand _actualizarListaSolicitudesCommand;
        public ICommand actualizarListaSolicitudesCommand
        {
            get
            {
                if (_actualizarListaSolicitudesCommand == null)
                {
                    _actualizarListaSolicitudesCommand = new RelayCommand(param => NotifyPropertyChanged("listaSolicitudes"));
                }
                return _actualizarListaSolicitudesCommand;
            }
        }

        RelayCommand _agregarSolicitudCommand;
        public ICommand agregarSolicitudCommand
        {
            get
            {
                if (_agregarSolicitudCommand == null)
                {
                    _agregarSolicitudCommand = new RelayCommand(p => statusTab = Tab.AGREGAR);
                }
                return _agregarSolicitudCommand;
            }
        }

        RelayCommand _viewEditSolicitudCommand;
        public ICommand viewEditSolicitudCommand
        {
            get
            {
                if (_viewEditSolicitudCommand == null)
                {
                    _viewEditSolicitudCommand = new RelayCommand(viewEditSolicitud);
                }
                return _viewEditSolicitudCommand;
            }
        }

        RelayCommand _saveSolicitudCommand;
        public ICommand saveSolicitudCommand
        {
            get
            {
                if (_saveSolicitudCommand == null)
                {
                    _saveSolicitudCommand = new RelayCommand(saveSolicitud);
                }
                return _saveSolicitudCommand;
            }
        }

        RelayCommand _cancelSolicitudCommand;
        public ICommand cancelSolicitudCommand
        {
            get
            {
                if (_cancelSolicitudCommand == null)
                {
                    _cancelSolicitudCommand = new RelayCommand(cancelSolicitud);
                }
                return _cancelSolicitudCommand;
            }
        }

        RelayCommand _agregarNuevoProductoCommand;
        public ICommand agregarNuevoProductoCommand
        {
            get
            {
                if (_agregarNuevoProductoCommand == null)
                {
                    _agregarNuevoProductoCommand = new RelayCommand(agregarNuevoProducto);
                }
                return _agregarNuevoProductoCommand;
            }
        }
        
        #endregion

        #region Comandos

        public void viewEditSolicitud(Object id)
        {
            try
            {
                int idSolicitud = Int32.Parse(id.ToString());
                this.solicitud = listaSolicitudes.Single(sol => sol.id == idSolicitud);
                this.statusTab = Tab.DETALLES;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void saveSolicitud(Object obj)
        {

            if (solicitud.id > 0)//Si existe
            {
                if (!MA_SolicitudAbastecimientoService.enviarCambios())
                {
                    MessageBox.Show("No se pudo actualizar la solicitud de abastecimiento");
                }
                else
                {
                    MessageBox.Show("La solicitud de abastecimiento fue guardada con éxito");
                }
            }
            else
            {
                if (!MA_SolicitudAbastecimientoService.insertarSolicitud(solicitud))
                {
                    MessageBox.Show("No se pudo agregar la nueva solicitud");
                }
                else
                {
                    MessageBox.Show("La solicitud fue agregada con éxito");
                }
            }
            NotifyPropertyChanged("listaSolicitudes");
        }

        public void cancelSolicitud(Object obj)
        {
            this.statusTab = Tab.BUSQUEDA;
            listaSolicitudes = MA_SolicitudAbastecimientoService.listaSolicitudes;
        }

        public void agregarNuevoProducto(Object atr)
        {
            Producto producto = null;
            try
            { 
                producto = MA_ProductoService.obtenerTodosProductos()
                    .First(p => !String.IsNullOrEmpty(p.codigo) && p.codigo.Equals(codigoNuevoProducto)); 
            }
            catch { }

            if (producto != null && solicitud.SolicitudAbastecimientoProducto.Count(mp => mp.Producto == producto) <= 0)
            {
                SolicitudAbastecimientoProducto sProducto = new SolicitudAbastecimientoProducto 
                                            { cantidad = 1, SolicitudAbastecimiento = solicitud, Producto = producto };
                solicitud.SolicitudAbastecimientoProducto.Add(sProducto);
                NotifyPropertyChanged("solicitud");
                NotifyPropertyChanged("solicitud.SolicitudAbastecimientoProducto");
            }
            else
            {
                MessageBox.Show("No se encontró un producto con el código \"" + codigoNuevoProducto + "\".", 
                    "No se encontró el Producto", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        
    }
}
