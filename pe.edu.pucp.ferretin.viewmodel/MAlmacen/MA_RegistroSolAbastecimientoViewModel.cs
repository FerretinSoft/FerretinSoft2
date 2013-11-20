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
using System.Data.Linq;
using System.Collections.ObjectModel;

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

        public DateTime _searchFechaDesde = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
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
                    case Tab.BUSQUEDA: 
                        detallesTabHeader = "Nueva Solicitud";
                        isCreating = false;
                        //if (solicitud == null) solicitud = new SolicitudAbastecimiento();
                        //solicitud.fecha = DateTime.Today; 
                        //solicitud.Tienda = currentTienda; 
                        break;
                    case Tab.AGREGAR: 
                        detallesTabHeader = "Nueva Solicitud"; 
                        if(solicitud == null || solicitud.id > 0) solicitud = new SolicitudAbastecimiento(); 
                        solicitud.fecha = DateTime.Today; 
                        solicitud.Tienda = currentTienda;
                        solicitud.SolicitudAbastecimientoProducto = 
                                    MA_SolicitudAbastecimientoService.initProductosPorSolicitud(usuarioLogueado.Empleado.tiendaActual, solicitud);
                        isCreating = true;
                        break;//Si es agregar, creo un nuevo objeto Almacen
                    case Tab.DETALLES: 
                        detallesTabHeader = "Detalles";
                        isCreating = false;
                        break;
                    default: 
                        detallesTabHeader = "Nueva Solicitud";
                        isCreating = false;
                        break;//Si es agregar, creo un nuevo objeto Almacen
                }
                NotifyPropertyChanged("statusTab");
                //Cuando se cambia el status, tambien se tiene que actualizar el currentIndex del tab
                NotifyPropertyChanged("currentIndexTab"); //Hace que cambie el tab automaticamente
                NotifyPropertyChanged("isCreating");
                NotifyPropertyChanged("solicitud");
                NotifyPropertyChanged("productosPorSolicitud");
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

        private bool _isCreating;
        public bool isCreating
        {
            get
            {
                return _isCreating;
            }

            set
            {
                _isCreating = value;
            }
        }

        public Tienda currentTienda
        {
            get
            {
                return usuarioLogueado.Empleado.tiendaActual;
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
                NotifyPropertyChanged("productosPorSolicitud");                
            }
        }

        private ObservableCollection<MA_SolicitudAbastecimientoService.ProductoPorSolicitudTienda> _productosPorSolicitud;
        public ObservableCollection<MA_SolicitudAbastecimientoService.ProductoPorSolicitudTienda> productosPorSolicitud
        {
            get
            {
                _productosPorSolicitud = new ObservableCollection<MA_SolicitudAbastecimientoService.ProductoPorSolicitudTienda>(
                                                    MA_SolicitudAbastecimientoService.buscarProductosPorSolicitud(currentTienda, solicitud));
                return _productosPorSolicitud;
            }
            set
            {
                _productosPorSolicitud = value;
                NotifyPropertyChanged("productosPorSolicitud");
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
                //isCreating = false;
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
                MA_ComunService.idVentana(26);
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
                //if (solicitud.codigo == null || solicitud.codigo == "") MessageBox.Show("Debe insertar el código de la solicitud");
                if (solicitud.SolicitudAbastecimientoProducto.Count <= 0) MessageBox.Show("Debe insertar al menos un producto en su solicitud");
                else
                {
                    SolicitudAbastecimientoEstado estadoInicial = estadoSolicitud.FirstOrDefault(s => s.nombre == "Pendiente");
                    if (estadoInicial != null) solicitud.SolicitudAbastecimientoEstado = estadoInicial;
                    for (int i = 0; i < solicitud.SolicitudAbastecimientoProducto.Count; i++)
                    {
                        solicitud.SolicitudAbastecimientoProducto[i].cantidadAtendida = 0;
                        solicitud.SolicitudAbastecimientoProducto[i].cantidadRestante = solicitud.SolicitudAbastecimientoProducto[i].cantidad;
                    }
                    MA_ComunService.idVentana(25);
                    if (!MA_SolicitudAbastecimientoService.insertarSolicitud(solicitud))
                    {
                        MessageBox.Show("No se pudo agregar la nueva solicitud");
                    }
                    else
                    {
                        isCreating = false;
                        NotifyPropertyChanged("isCreating");
                        MessageBox.Show("La solicitud fue agregada con éxito");
                    }
                }
            }
            NotifyPropertyChanged("listaSolicitudes");
        }

        public void cancelSolicitud(Object obj)
        {
            MessageBoxResult result = MessageBox.Show("Al salir, perderá todos los datos ingresados. ¿Desea continuar?",
            "ATENCIÓN", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            if (result == MessageBoxResult.OK)
            {
                this.statusTab = Tab.BUSQUEDA;
                listaSolicitudes = MA_SolicitudAbastecimientoService.listaSolicitudes;
            }
        }

        public void agregarNuevoProducto(Object atr)
        {
            if (codigoNuevoProducto != null && codigoNuevoProducto.Length > 0)
            {
                Producto producto = null;
                try
                {
                    producto = MA_SharedService.obtenerProductoxCodigo(codigoNuevoProducto);
                }
                catch { }

                if (producto != null)
                {
                    if (solicitud.SolicitudAbastecimientoProducto.Count(sap => sap.Producto.id == producto.id) == 1) // el producto ya fue incluido
                    {
                        solicitud.SolicitudAbastecimientoProducto.Single(sap => sap.Producto.id == producto.id).cantidad++;
                    }
                    else
                    {
                        SolicitudAbastecimientoProducto solProducto = new SolicitudAbastecimientoProducto();
                        solProducto.SolicitudAbastecimiento = solicitud;
                        solProducto.Producto = producto;
                        solProducto.cantidad = 1;

                        solicitud.SolicitudAbastecimientoProducto.Add(solProducto);

                    }
                    NotifyPropertyChanged("solicitud");
                    NotifyPropertyChanged("productosPorSolicitud");
                }
                else
                {
                    MessageBox.Show("No se encontro un producto con el código \"" + codigoNuevoProducto + "\".", "No se encontro el Producto", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }    
        }
        #endregion

        
    }
}
