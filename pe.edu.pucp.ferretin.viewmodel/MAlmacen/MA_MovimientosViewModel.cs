using pe.edu.pucp.ferretin.controller.MAlmacen;
using pe.edu.pucp.ferretin.model;
using pe.edu.pucp.ferretin.viewmodel.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace pe.edu.pucp.ferretin.viewmodel.MAlmacen
{
    public class MA_MovimientosViewModel : ViewModelBase
    {
        public IEnumerable<MovimientoEstado> estadosMovimientoS
        {
            get
            {
                var sequence = Enumerable.Empty<MovimientoEstado>();
                IEnumerable<MovimientoEstado> items = new MovimientoEstado[] { new MovimientoEstado { id = 0, nombre = "Todos" } };
                return items.Concat(MA_MovimientosService.estadosMovimiento);
            }
        }

        public IEnumerable<MovimientoEstado> estadosMovimiento
        {
            get
            {
                return MA_MovimientosService.estadosMovimiento;
            }
        }

        /*public ObservableCollection<char> categoriasMovimiento
        {
            get
            {
                ObservableCollection<char> milista = new ObservableCollection<char>();
                milista.Add('E');
                milista.Add('S');
                return milista;
            }
        }*/

        public bool isCreating
        {
            get
            {
                if (statusTab == Tab.NUEVO)
                {
                    return true; //Se Activaran
                }
                else
                {
                    return false; //Se bloquearan par que no sean editables
                }
            }
        }

        public bool estadoEditable
        {
            get
            {
                if (movimiento != null && movimiento.MovimientoEstado != null && 
                    (movimiento.MovimientoEstado.nombre == "Finalizado" || movimiento.MovimientoEstado.nombre == "Anulado"))
                {
                    return false; 
                }
                else
                {
                    return true; 
                }
            }
        }
        
        public IEnumerable<Tienda> almacenesSearch
        {
            get
            {
                //Creo una nueva secuencia
                var sequence = Enumerable.Empty<Tienda>();
                //Primero agrego un item de Todos para que salga al inicio
                //Pongo el ID en 0 para que al buscar, no filtre nada cuando se selecciona todos
                IEnumerable<Tienda> items = new Tienda[] { new Tienda { id = 0, nombre = "Todos" } };
                //Luego concateno el itemcon los elementos del combobox
                return items.Concat(tiendas);
            }
        }

        #region Lista Movimientos y Edicion de Movimientos
        private Movimiento _movimiento;
        
        public Movimiento movimiento
        {
            get
            {
                return _movimiento;
            }
            set
            {
                _movimiento = value;
                NotifyPropertyChanged("movimiento");
                NotifyPropertyChanged("productosPorMovimiento");                
            }
        }

        private IEnumerable<Movimiento> _listaMovimientos;
        public IEnumerable<Movimiento> listaMovimientos
        {
            get
            {
                _listaMovimientos = MA_MovimientosService.buscarMovimientos(searchAlmacen,searchEstado,searchFechaDesde,searchFechaHasta);
                return _listaMovimientos;
            }
            set
            {
                _listaMovimientos = value;
                NotifyPropertyChanged("listaMovimientos");
            }
        }

        private IEnumerable<MA_MovimientosService.MovimientoProductoTienda> _productosPorMovimiento;
        public IEnumerable<MA_MovimientosService.MovimientoProductoTienda> productosPorMovimiento
        {
            get
            {
                _productosPorMovimiento = MA_MovimientosService.buscarProductosPorMovimientoTienda(movimiento);
                return _productosPorMovimiento;
            }
            set
            {
                _productosPorMovimiento = value;
                NotifyPropertyChanged("productosPorMovimiento");
            }
        }

        private IEnumerable<MovimientoTipo> _tiposMovimiento;
        public IEnumerable<MovimientoTipo> tiposMovimiento
        {
            get
            {
                
                _tiposMovimiento = MA_SharedService.tiposMovimientos;
                
                return _tiposMovimiento;
            }
            set
            {
                _tiposMovimiento = value;
                NotifyPropertyChanged("tiposMovimiento");
            }
        }

        private Movimiento _selectedMovimiento;
        public Movimiento selectedMovimiento
        {
            get 
            {
                return _selectedMovimiento;
            }
            set
            {
                _selectedMovimiento = value;
            }
        }
        #endregion

        #region Constructor
        public MA_MovimientosViewModel()
        {
            _movimiento = new Movimiento();
            //_movimiento.fecha = DateTime.Today;
            //_movimiento.codigo = Movimiento.generateCode();
        }
        #endregion

        #region Valores para el cuadro de Búsqueda
        public Tienda _searchAlmacen;
        public Tienda searchAlmacen { get { return _searchAlmacen; } set { _searchAlmacen = value; NotifyPropertyChanged("searchAlmacen"); } }

        public MovimientoEstado _searchEstado;
        public MovimientoEstado searchEstado { get { return _searchEstado; } set { _searchEstado = value; NotifyPropertyChanged("searchEstado"); } }
        
        public DateTime _searchFechaDesde = DateTime.Today.AddDays(-30);
        public DateTime searchFechaDesde { get { return _searchFechaDesde; } set { _searchFechaDesde= value; NotifyPropertyChanged("searchFechaDesde"); } }

        public DateTime _searchFechaHasta = DateTime.Today;
        public DateTime searchFechaHasta { get { return _searchFechaHasta; } set { _searchFechaHasta= value; NotifyPropertyChanged("searchFechaHasta"); } }

        #endregion

        #region Manejo de los Tabs
        public enum Tab
        {
            //Pestañas virtuales:
            //0       1        2          3     
            BUSQUEDA, NUEVO, DETALLES, TIPOMOV
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
                        detallesTabHeader = "Nuevo"; 
                        movimiento = new Movimiento(); 
                        movimiento.fecha = DateTime.Today;
                        movimiento.codigo = Movimiento.generateCode();
                        break;//Si es agregar, creo un nuevo objeto Cliente
                    case Tab.NUEVO: 
                        detallesTabHeader = "Nuevo"; 
                        movimiento = new Movimiento(); 
                        movimiento.fecha = DateTime.Today;
                        movimiento.codigo = Movimiento.generateCode();
                        break;//Si es agregar, creo un nuevo objeto Cliente
                    case Tab.DETALLES: 
                        detallesTabHeader = "Detalles"; 
                        break;
                    default: 
                        detallesTabHeader = "Nuevo"; 
                        movimiento = new Movimiento(); 
                        movimiento.fecha = DateTime.Today;
                        movimiento.codigo = Movimiento.generateCode();
                        break;//Si es agregar, creo un nuevo objeto Cliente
                }
                NotifyPropertyChanged("statusTab");
                //Cuando se cambia el status, tambien se tiene que actualizar el currentIndex del tab
                NotifyPropertyChanged("currentIndexTab"); //Hace que cambie el tab automaticamente
                NotifyPropertyChanged("isCreating"); //Para que se activen o desactiven los inputs
                NotifyPropertyChanged("estadoEditable");
            }
        }
        //Usado para mover los tabs de acuerdo a las acciones realizadas
        public int currentIndexTab
        {
            get { return _statusTab == Tab.BUSQUEDA ? 0 : (_statusTab == Tab.TIPOMOV ? 2 : 1); }
            set { statusTab = value == 0 ? Tab.BUSQUEDA : (value == 2 ? Tab.TIPOMOV : Tab.NUEVO); }
        }
        private String _detallesTabHeader = "Nuevo"; //Default
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

        #region RelayCommand
        RelayCommand _actualizarListaMovimientosCommand;
        public ICommand actualizarListaMovimientosCommand
        {
            get
            {
                if (_actualizarListaMovimientosCommand == null)
                {
                    _actualizarListaMovimientosCommand = new RelayCommand(param => NotifyPropertyChanged("listaMovimientos"));
                }
                return _actualizarListaMovimientosCommand;
            }
        }
        RelayCommand _viewEditMovimientoCommand;
        public ICommand viewEditMovimientoCommand
        {
            get
            {
                if (_viewEditMovimientoCommand == null)
                {
                    _viewEditMovimientoCommand = new RelayCommand(viewEditMovimiento);
                }
                return _viewEditMovimientoCommand;
            }
        }
        RelayCommand _saveMovimientoCommand;
        public ICommand saveMovimientoCommand
        {
            get
            {
                if (_saveMovimientoCommand == null)
                {
                    _saveMovimientoCommand = new RelayCommand(saveMovimiento);
                }
                return _saveMovimientoCommand;
            }
        }
        RelayCommand _cancelMovimientoCommand;
        public ICommand cancelMovimientoCommand
        {
            get
            {
                if (_cancelMovimientoCommand == null)
                {
                    _cancelMovimientoCommand = new RelayCommand(cancelMovimiento);
                }
                return _cancelMovimientoCommand;
            }
        }

        RelayCommand _newMovimientoCommand;
        public ICommand newMovimientoCommand
        {
            get
            {
                if (_newMovimientoCommand == null)
                {
                    _newMovimientoCommand = new RelayCommand(newMovimiento);
                }
                return _newMovimientoCommand;
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
        RelayCommand _saveTipoMovimientoCommand;
        public ICommand saveTipoMovimientoCommand
        {
            get
            {
                if (_saveTipoMovimientoCommand == null)
                {
                    _saveTipoMovimientoCommand = new RelayCommand(saveTipoMovimiento);//, canSaveTipoMovimientoExecute);
                }
                return _saveTipoMovimientoCommand;
            }
        }

        RelayCommand _cancelTipoMovimientoCommand;
        public ICommand cancelTipoMovimientoCommand
        {
            get
            {
                if (_cancelTipoMovimientoCommand == null)
                {
                    _cancelTipoMovimientoCommand = new RelayCommand(cancelTipoMovimiento);
                }
                return _cancelTipoMovimientoCommand;
            }
        }
        #endregion

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

        public IEnumerable<MovimientoProducto> movimientosNuevoProducto {
            get
            {
                if (movimiento.MovimientoProducto != null)
                {
                    return movimiento.MovimientoProducto;
                }
                else
                {
                    return new MovimientoProducto[] { };
                }
            }
        }
        

        #region Comandos

        public void saveTipoMovimiento(object param)
        {
            /*for (int i = 0; i < tiposMovimiento.Count(); i++)
            {
                if (tiposMovimiento.ElementAt(i).categoria != 'S' && tiposMovimiento.ElementAt(i).categoria != 'E')
                {
                    MessageBox.Show("Valor inválido de categoría en la fila " + (i + 1) + 
                        ".\nLos valores permitidos son 'E' para los movimientos de entrada y 'S' para los de salida." );
                    return;
                }

            }*/
            if (!MA_MovimientosService.enviarCambios())
            {
                MessageBox.Show("Ocurrio un error al guardar, reintente.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBox.Show("Se guardo correctamente","Guardado",MessageBoxButton.OK,MessageBoxImage.Information);
            }
            NotifyPropertyChanged("tiposMovimiento");
        }

        public void cancelTipoMovimiento(object param)
        {
            NotifyPropertyChanged("tiposMovimiento");
        }

        public void agregarNuevoProducto(Object atr)
        {
            Producto producto = null;
            try
            { producto = MA_ProductoService.obtenerTodosProductos().First(p => !String.IsNullOrEmpty(p.codigo) && p.codigo.Equals(codigoNuevoProducto)); }
            catch { }

            if (producto != null && movimiento.MovimientoProducto.Count(mp => mp.Producto == producto) <= 0)
            {
                MovimientoProducto mproducto = new MovimientoProducto { cantidad = 1, Movimiento = movimiento, Producto = producto };
                movimiento.MovimientoProducto.Add(mproducto);
                NotifyPropertyChanged("movimiento");
                NotifyPropertyChanged("productosPorMovimiento");
            }
            else
            {
                MessageBox.Show("No se encontro un producto con el código \"" + codigoNuevoProducto + "\".","No se encontro el Producto",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }

        public void viewEditMovimiento(Object id)
        {
            try
            {
                int selId = Int32.Parse(id.ToString());
                this.movimiento = listaMovimientos.Single(movimiento => movimiento.id == selId);
                this.statusTab = Tab.DETALLES;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void newMovimiento(Object id)
        {
            try
            {
                this.statusTab = Tab.NUEVO;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void saveMovimiento(Object obj)
        {

            if (movimiento.id > 0)//Si existe
            {
                MA_ComunService.idVentana(23);
                if (!MA_MovimientosService.enviarCambios())
                {
                    MessageBox.Show("No se pudo actualizar el movimiento");
                }
                else
                {
                    MessageBox.Show("El movimiento fue guardado con éxito");                    
                }
            }
            else
            {
                //validación
                if (movimiento.codigo == null || movimiento.codigo == "") MessageBox.Show("Debe llenar el campo código.");
                else if (movimiento.MovimientoTipo == null) MessageBox.Show("Debe seleccionar un tipo de movimiento");
                else if (movimiento.Tienda == null && movimiento.MovimientoTipo.categoriaEnum == MovimientoTipo.CategoriaMovimiento.SALIDA) MessageBox.Show("Debe seleccionar un almacén de salida de mercancía");
                else if (movimiento.Tienda1 == null && movimiento.MovimientoTipo.categoriaEnum == MovimientoTipo.CategoriaMovimiento.ENTRADA) MessageBox.Show("Debe seleccionar un almacén destino para la entrada de mercancía");
                else if ((movimiento.Tienda == null || movimiento.Tienda1 == null) && movimiento.MovimientoTipo.categoriaEnum == MovimientoTipo.CategoriaMovimiento.TRANSFERENCIA) MessageBox.Show("Debe seleccionar un almacén de salida de mercancía y un almacén de entrada");
                else if (movimiento.MovimientoEstado == null) MessageBox.Show("Debe seleccionar un estado para el movimiento");
                else if (movimiento.MovimientoProducto.Count <= 0) MessageBox.Show("Debe registrar al menos un Producto en su movimiento");
                else
                {
                    MA_ComunService.idVentana(22);
                    if (!MA_MovimientosService.InsertarMovimiento(movimiento))
                    {
                        MessageBox.Show("No se pudo agregar el nuevo movimiento");
                    }
                    else
                    {
                        MessageBox.Show("El movimiento fue agregado con éxito");                        
                    }
                }
            }
            NotifyPropertyChanged("listaMovimientos");
        }

        public void cancelMovimiento(Object obj)
        {
            this.statusTab = Tab.BUSQUEDA;
        }

        private bool canSaveTipoMovimientoExecute(object obj)
        {
            for (int i = 0; i < tiposMovimiento.Count(); i++)
            {
                if (base.UIValidationErrorCount != 0 || this.tiposMovimiento.ElementAt(i).Errors.Count != 0)
                    return false;
            }
            return true;
        }
        #endregion
    }
}
