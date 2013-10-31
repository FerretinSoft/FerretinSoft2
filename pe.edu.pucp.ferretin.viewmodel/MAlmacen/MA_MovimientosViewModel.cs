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
    public class MA_MovimientosViewModel : ViewModelBase
    {
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
                /*if (value.id_ubigeo != null)
                {
                    String id_distrito = value.id_ubigeo;
                    String id_provincia = value.UbigeoDistrito.id_ubig_provincia;
                    String id_departamento = value.UbigeoDistrito.UbigeoProvincia.id_ubig_departamento;
                    distritos = MV_ClienteService.distritos.Where(distrito => distrito.id_ubig_provincia.Equals(id_provincia));
                }*/
                NotifyPropertyChanged("movimiento");
            }
        }

        private IEnumerable<Movimiento> _listaMovimientos;
        public IEnumerable<Movimiento> listaMovimientos
        {
            get
            {
                //Dictionary<String, Object> parametros = new Dictionary<String, Object>();
                //if (searchAlmacen >= 0) parametros.Add("tienda", almacenes.ElementAt(searchAlmacen).id);
                //if (searchEstado >= 0) parametros.Add("estado", estadosMovimiento.ElementAt(searchEstado).id);
                //parametros.Add("fechaDesde", searchFechaDesde);
                //parametros.Add("fechaHasta", searchFechaHasta);
                //_listaMovimientos = MA_MovimientosService.ObtenerListaMovimientos(parametros);
                _listaMovimientos = MA_MovimientosService.buscarMovimientos(-1, -1, default(DateTime), default(DateTime));
                return _listaMovimientos;
            }
            set
            {
                _listaMovimientos = value;
                NotifyPropertyChanged("listaMovimientos");
            }
        }

        private IEnumerable<MovimientoEstado> _estadosMovimiento;
        public IEnumerable<MovimientoEstado> estadosMovimiento
        {
            get
            {
                if (_estadosMovimiento == null)
                {
                    _estadosMovimiento= MA_SharedService.estadosMovimiento;                  
                    
                }
                return _estadosMovimiento;
            }
            set
            {
                _estadosMovimiento = value;
            }
        }

        private IEnumerable<MovimientoTipo> _tiposMovimiento;
        public IEnumerable<MovimientoTipo> tiposMovimiento
        {
            get
            {
                if (_tiposMovimiento == null)
                {
                    _tiposMovimiento = MA_SharedService.tiposMovimiento;

                }
                return _tiposMovimiento;
            }
            set
            {
                _tiposMovimiento = value;
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
                //if(value >= 0 && value < listaMovimientos.Count()) 
                //    movimiento = listaMovimientos.ElementAt(value);
                //MessageBox.Show(value + movimiento.codigo);
                _selectedMovimiento = value;
            }
        }
        #endregion

        #region Constructor
        public MA_MovimientosViewModel()
        {
            _movimiento = new Movimiento();
        }
        #endregion

        #region Valores para el cuadro de Búsqueda
        public int _searchAlmacen = 0;
        public int searchAlmacen { get { return _searchAlmacen; } set { _searchAlmacen = value; NotifyPropertyChanged("searchAlmacen"); } }

        public int _searchEstado = 0;
        public int searchEstado { get { return _searchEstado; } set { _searchEstado = value; NotifyPropertyChanged("searchEstado"); } }
        
        public DateTime _searchFechaDesde = new DateTime(2000, 1, 1);
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
                    case Tab.BUSQUEDA: detallesTabHeader = "Nuevo"; movimiento = new Movimiento(); break;//Si es agregar, creo un nuevo objeto Cliente
                    case Tab.NUEVO: detallesTabHeader = "Nuevo"; movimiento = new Movimiento(); break;//Si es agregar, creo un nuevo objeto Cliente
                    case Tab.DETALLES: detallesTabHeader = "Detalles"; break;
                    default: detallesTabHeader = "Nuevo"; movimiento = new Movimiento(); break;//Si es agregar, creo un nuevo objeto Cliente
                }
                NotifyPropertyChanged("statusTab");
                //Cuando se cambia el status, tambien se tiene que actualizar el currentIndex del tab
                NotifyPropertyChanged("currentIndexTab"); //Hace que cambie el tab automaticamente
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
        #endregion

        #region Comandos

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
                if (!MA_MovimientosService.InsertarMovimiento(movimiento))
                {
                    MessageBox.Show("No se pudo agregar el nuevo movimiento");
                }
                else
                {
                    MessageBox.Show("El movimiento fue agregado con éxito");
                    //listaMovimientos = MA_MovimientosService.ListaMovimientos;
                }
            }
            NotifyPropertyChanged("listaMovimientos");
        }

        public void cancelMovimiento(Object obj)
        {
            this.statusTab = Tab.BUSQUEDA;
            //listaMovimientos = MA_MovimientosService.ListaMovimientos;
        }
        #endregion
    }
}
