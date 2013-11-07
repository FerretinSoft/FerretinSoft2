using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using pe.edu.pucp.ferretin.controller;
using pe.edu.pucp.ferretin.controller.MAlmacen;
using pe.edu.pucp.ferretin.controller.MRecursosHumanos;
using pe.edu.pucp.ferretin.controller.MSeguridad;
using pe.edu.pucp.ferretin.controller.MVentas;
using pe.edu.pucp.ferretin.model;
using pe.edu.pucp.ferretin.viewmodel.Helper;

namespace pe.edu.pucp.ferretin.viewmodel.MVentas
{
    public class MV_DevolucionesViewModel : ViewModelBase
    {
        #region Constructor
        public MV_DevolucionesViewModel()
        {

            _devolucion = new Devolucion();
            {

            };

            _notaCredito = new NotaCredito();
            {

            }
        }
        #endregion


        #region Valores para el cuadro de Búsqueda
        public String _searchNroDocumento = "";
        public String searchNroDocumento { get { return _searchNroDocumento; } set { _searchNroDocumento = value; NotifyPropertyChanged("searchNroDocumento"); } }

        public String _loadNroDocumento = "";
        public String loadNroDocumento { get { return _loadNroDocumento; } set { _loadNroDocumento = value; NotifyPropertyChanged("loadNroDocumento"); } }

        public String _searchNroDocCliente = "";
        public String searchNroDocCliente { get { return _searchNroDocCliente; } set { _searchNroDocCliente = value; NotifyPropertyChanged("searchNroDocCliente"); } }

        public String _searchnombreCliente = "";
        public String searchnombreCliente { get { return _searchnombreCliente; } set { _searchnombreCliente = value; NotifyPropertyChanged("searchnombreCliente"); } }

        public String _searchVendedor = "";
        public String searchVendedor { get { return _searchVendedor; } set { _searchVendedor = value; NotifyPropertyChanged("searchVendedor"); } }

        public DateTime _searchFechaInicio = DateTime.Parse("10/09/2013");
        public DateTime searchFechaInicio { get { return _searchFechaInicio; } set { _searchFechaInicio = value; NotifyPropertyChanged("searchFechaInicio"); } }

        public DateTime _searchFechaFin = DateTime.Today;
        public DateTime searchFechaFin { get { return _searchFechaFin; } set { _searchFechaFin = value; NotifyPropertyChanged("searchFechaFin"); } }

        public String _nombreCliente = "";
        public String nombreCliente { get { return _nombreCliente; } set { _nombreCliente = value; NotifyPropertyChanged("nombreCliente"); } }

        public String _nombreVendedor = "";
        public String nombreVendedor { get { return _nombreVendedor; } set { _nombreVendedor = value; NotifyPropertyChanged("nombreVendedor"); } }

        public bool _devolucionRegistrada = false;
        public bool devolucionRegistrada { get { return _devolucionRegistrada; } set { _devolucionRegistrada = value; NotifyPropertyChanged("devolucionRegistrada"); } }

        public bool _noDevolucionRegistrada = false;
        public bool noDevolucionRegistrada { get { return !devolucionRegistrada; } set { _noDevolucionRegistrada = value; NotifyPropertyChanged("noDevolucionRegistrada"); } }


        public String _searchNroDevolucion = "";
        public String searchNroDevolucion { get { return _searchNroDevolucion; } set { _searchNroDevolucion = value; NotifyPropertyChanged("searchNroDevolucion"); } }

        public long _id = 0;
        public long id { get { return _id; } set { _id = value; NotifyPropertyChanged("id"); } }


        private int _selectedTab = 0;
        public int selectedTab
        {
            get
            {
                return _selectedTab;
            }
            set
            {
                if (value == 2)
                    devolucion = new Devolucion();
                _selectedTab = value;
                NotifyPropertyChanged("selectedTab");
            }
        }
        #endregion


        #region Lista
        private Devolucion _devolucion;
        public Devolucion devolucion
        {
            get
            {
                return _devolucion;
            }
            set
            {
                
                _devolucion = value;
                NotifyPropertyChanged("devolucion");
            }
        }

        private NotaCredito _notaCredito;
        public NotaCredito notaCredito
        {
            get
            {
                return _notaCredito;
            }
            set
            {
                _notaCredito = value;
                NotifyPropertyChanged("notaCredito");
            }
        }
        private IEnumerable<Devolucion> _listaDevoluciones;
        public IEnumerable<Devolucion> listaDevoluciones
        {
            get
            {

                _listaDevoluciones = MV_DevolucionService.buscarDevoluciones(searchNroDevolucion, searchNroDocumento, searchNroDocCliente, searchFechaInicio, searchFechaFin, searchVendedor);

                return _listaDevoluciones;
            }
            set
            {
                _listaDevoluciones = value;
                NotifyPropertyChanged("listaDevoluciones");
            }
        }

        private IEnumerable<DevolucionProducto> _listaProductos;
        public IEnumerable<DevolucionProducto> listaProductos
        {
            get
            {
                return _listaProductos;
            }
            set
            {
                _listaProductos = value;
                NotifyPropertyChanged("listaProductos");
            }
        }

        private IEnumerable<VentaProducto> _listaProductosComprados;
        public IEnumerable<VentaProducto> listaProductosComprados
        {
            get
            {
                return _listaProductosComprados;
            }
            set
            {
                _listaProductosComprados = value;
                NotifyPropertyChanged("listaProductosComprados");
            }
        }

        private IEnumerable<DevolucionProducto> _listaProductosDev;
        public IEnumerable<DevolucionProducto> listaProductosDev
        {
            get
            {
                return _listaProductosDev;
            }
            set
            {
                _listaProductosDev = value;
                NotifyPropertyChanged("listaProductosDev");
            }
        }
        #endregion

        #region RalayCommand
        RelayCommand _actualizarListaDevolucionesCommand;
        public ICommand actualizarListaDevolucionesCommand
        {
            get
            {
                if (_actualizarListaDevolucionesCommand == null)
                {
                    _actualizarListaDevolucionesCommand = new RelayCommand(param => NotifyPropertyChanged("listaDevoluciones"));
                }
                return _actualizarListaDevolucionesCommand;
            }
        }
        RelayCommand _viewDetailDevolucionCommand;
        public ICommand viewDetailDevolucionCommand
        {
            get
            {
                if (_viewDetailDevolucionCommand == null)
                {
                    _viewDetailDevolucionCommand = new RelayCommand(viewDetailDevolucion);
                }
                return _viewDetailDevolucionCommand;
            }
        }

        RelayCommand _addProductDevCommand;
        public ICommand addProductDevCommand
        {
            get
            {
                if (_addProductDevCommand == null)
                {
                    _addProductDevCommand = new RelayCommand(addProductDev);
                }
                return _addProductDevCommand;
            }
        }

        RelayCommand _saveDevolucionCommand;
        public ICommand saveDevolucionCommand
        {
            get
            {
                if (_saveDevolucionCommand == null)
                {
                    _saveDevolucionCommand = new RelayCommand(saveDevolucion);
                }
                return _saveDevolucionCommand;
            }
        }



        RelayCommand _cancelDevolucionCommand;

        public ICommand cancelDevolucionCommand
        {
            get
            {
                if (_cancelDevolucionCommand == null)
                {
                    _cancelDevolucionCommand = new RelayCommand(cancelDevolucion);
                }
                return _cancelDevolucionCommand;
            }
        }

        RelayCommand _cargarClienteCommand;
        public ICommand cargarClienteCommand
        {
            get
            {
                if (_cargarClienteCommand == null)
                {
                    _cargarClienteCommand = new RelayCommand(cargarCliente);
                }
                return _cargarClienteCommand;
            }
        }

        RelayCommand _buscarVentaCommand;
        public ICommand buscarVentaCommand
        {
            get
            {
                if (_buscarVentaCommand == null)
                {
                    _buscarVentaCommand = new RelayCommand(buscarVenta);
                }
                return _buscarVentaCommand;
            }
        }

        RelayCommand _cargarVentaCommand;
        public ICommand cargarVentaCommand
        {
            get
            {
                if (_cargarVentaCommand == null)
                {
                    _cargarVentaCommand = new RelayCommand(cargarVenta);
                }
                return _cargarVentaCommand;
            }
        }

        RelayCommand _nuevaDevolucionCommand;
        public ICommand nuevaDevolucionCommand
        {
            get
            {
                if (_nuevaDevolucionCommand == null)
                {
                    _nuevaDevolucionCommand = new RelayCommand(nuevaDevolucion);
                }
                return _nuevaDevolucionCommand;
            }
        }
        RelayCommand _cargarVendedorCommand;
               public ICommand cargarVendedorCommand
        {
            get
            {
                if (_cargarVendedorCommand == null)
                {
                    _cargarVendedorCommand = new RelayCommand(cargarVendedor);
                }
                return _cargarVendedorCommand;
            }
        }
        

        #endregion

        #region commands


        public void nuevaDevolucion(Object id)
        {
           this.listaProductosComprados = null;           
           devolucion.id_empleado = usuarioLogueado.Empleado.id;
           this.selectedTab = 2;
           devolucion.fecEmision = DateTime.Now;
           devolucion.codigo = MV_DevolucionService.obtenerCodDevolucion();
           NotifyPropertyChanged("devolucion");
           
        }


        public void cargarVendedor(Object id)
        {
            Empleado buscado = null;
            try
            {
                buscado = MR_EmpleadoService.obtenerEmpleadoByNroDoc(searchVendedor);
                nombreVendedor = buscado.nombreCompleto;            
            }
            catch { }

            if (buscado == null)
            {
                nombreVendedor = "";
                MessageBox.Show("No se encontro ningún vendedor con el número de documento proporcionado", "No se encontro", MessageBoxButton.OK, MessageBoxImage.Question);
            }
           
        }

        public void cargarVenta(Object id)
        {
            Venta buscado = null;
            try
            {
                buscado = MV_VentaService.obtenerVentaByCodVenta(loadNroDocumento);
                this.listaProductosComprados = MV_VentaService.obtenerProductosSinPuntosbyIdVenta(buscado.id);
                this.devolucion.Venta = buscado;
                this.devolucion.fecEmision = DateTime.Now;
                devolucion.codigo = MV_DevolucionService.obtenerCodDevolucion();
                devolucion.id_empleado = usuarioLogueado.Empleado.id;
                NotifyPropertyChanged("devolucion");
            }
            catch { }

            if (buscado == null)
            {
                this.listaProductosComprados = null;
                this.devolucion = new Devolucion();
                MessageBox.Show("No se encontro ninguna Venta con el número de documento proporcionado", "No se encontro", MessageBoxButton.OK, MessageBoxImage.Question);
            }
           
        }

        public void buscarVenta(Object id)
        {
            Venta buscado = null;
            try
            {
                buscado = MV_VentaService.obtenerVentaByCodVenta(searchNroDocumento);
            }
            catch { }

            if (buscado == null)
            {
                MessageBox.Show("No se encontro ninguna Venta con el número de documento proporcionado", "No se encontro", MessageBoxButton.OK, MessageBoxImage.Question);
            }
            NotifyPropertyChanged("searchnombreCliente");
            NotifyPropertyChanged("searchNroDocCliente");
            NotifyPropertyChanged("searchNroDocumento");
        }

        public void cargarCliente(Object id)
        {
            Cliente buscado = null;
            try
            {
                buscado = MV_ClienteService.obtenerClienteByNroDoc(searchNroDocCliente);
                searchnombreCliente = buscado.nombreCompleto;
                searchNroDocCliente = buscado.nroDoc;
            }
            catch { }

            if (buscado == null)
            {
                MessageBox.Show("No se encontro ningún cliente con el número de documento proporcionado", "No se encontro", MessageBoxButton.OK, MessageBoxImage.Question);
                searchnombreCliente = "";
                searchNroDocCliente = "";
            }
            
            NotifyPropertyChanged("searchnombreCliente");
            NotifyPropertyChanged("searchNroDocCliente");
        }



        public void cancelDevolucion(Object obj)
        {
            selectedTab = 0;
            this.listaDevoluciones = MV_DevolucionService.listaDevoluciones;
        }

        public void saveDevolucion(Object obj)
        {
            devolucion.id_empleado = usuarioLogueado.Empleado.id;
            devolucion.codigo = MV_DevolucionService.obtenerCodDevolucion();
            notaCredito.fechaEmision = DateTime.Now;
            devolucion.igv = devolucion.subTotal * (decimal)MS_ParametroService.obtenerIGV()/100;
            devolucion.total = devolucion.igv + devolucion.subTotal;
            notaCredito.importe = devolucion.total;
            notaCredito.estado = 0;
            notaCredito.codigo = devolucion.codigo;
            notaCredito.fechaVencimiento = DateTime.Now.AddDays(Convert.ToInt32(MS_ParametroService.obtenerParametro("vigencia de notas de credito")));
            ComunService.idVentana(7);
                    if (!MV_DevolucionService.insertarDevolucion(devolucion))
                    {
                        MessageBox.Show("No se pudo agregar la nuevo devolución");
                    }
                    else
                    {
                        MessageBox.Show("La devolución fue agregado con éxito");
                    }
            notaCredito.id_devolucion = devolucion.id;
            ComunService.idVentana(7);
                    if (!MV_NotaCreditoService.insertarNotaCredito(notaCredito))
                    {
                        MessageBox.Show("No se pudo agregar la nueva Nota de Crédito");
                    }
                    else
                    {
                        //MessageBox.Show("La Nota de Crédito fue agregado con éxito");
                        NotifyPropertyChanged("selectedTab");
                    }
                    try
                    {
                        string resp = MA_SharedService.registrarDevolucion(devolucion.Empleado.tiendaActual, devolucion.DevolucionProducto);
                    }
                    catch
                    {
                        try
                        {
                            MessageBox.Show("error en almácen");
                        }

                        catch { }
                    }
                    this.devolucionRegistrada = true;
                    this.noDevolucionRegistrada = false;
        }

        public void viewDetailDevolucion(Object id)
        {
            try
            {
                this.devolucion = new Devolucion();
                this.devolucion = listaDevoluciones.Single(devolucion => devolucion.id == (long)id);
                this.listaProductos = MV_DevolucionService.obtenerProductosbyIdDevolucion((long)id);
                this.notaCredito = MV_DevolucionService.obtenerNotaCredbyIdDevolucion((long)id);
                selectedTab = 1;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        public void addProductDev(Object id)
        {
            try
            {
                 DevolucionProducto prodDev = new DevolucionProducto();
                 VentaProducto prodSelec = MV_VentaService.obtenerVentaProductobyId((long)id);
                 prodDev.Producto = prodSelec.Producto;
                 prodDev.cantidad = 0;                
                 prodDev.id_producto = prodSelec.id_producto;
                 prodDev.monto = prodDev.cantidad * prodDev.Producto.precioLista;
                 devolucion.total = 0;
                 devolucion.id_venta = prodSelec.Venta.id;
                 
                 devolucion.DevolucionProducto.Add(prodDev);
                 NotifyPropertyChanged("devolucion");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }



        
        
        #endregion
    }
}
