using pe.edu.pucp.ferretin.controller.MVentas;
using pe.edu.pucp.ferretin.model;
using pe.edu.pucp.ferretin.viewmodel.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace pe.edu.pucp.ferretin.viewmodel.MVentas
{
    public class MV_ClientesViewModel : ViewModelBase
    {
        #region Constructor
        public MV_ClientesViewModel()
        {
            _cliente = new Cliente();
        }
        #endregion

        private bool _soloSeleccionarCliente = false;
        public bool soloSeleccionarCliente
        {
            get
            {
                return _soloSeleccionarCliente;
            }
            set
            {
                _soloSeleccionarCliente = value;
                NotifyPropertyChanged("soloSeleccionarCliente");
                NotifyPropertyChanged("nombreBotonGuardar");
                NotifyPropertyChanged("noSoloSeleccionarCliente");
                detallesTabHeader = value ? "Detalles" : "Agregar";
            }
        }
        public bool noSoloSeleccionarCliente
        {
            get
            {
                return !soloSeleccionarCliente;
            }
        }
        public String nombreBotonGuardar
        {
            get
            {
                return soloSeleccionarCliente ? "SELECCIONAR" : "GUARDAR";
            }
        }

        #region Valores para el cuadro de Búsqueda
        public String _searchNroDoc = "";
        public String searchNroDoc { get { return _searchNroDoc; } set { _searchNroDoc = value; NotifyPropertyChanged("searchNroDoc"); } }
        
        public String _searchNombre = "";
        public String searchNombre { get { return _searchNombre; } set { _searchNombre = value; NotifyPropertyChanged("searchNombre"); } }
        
        public String _searchApPaterno = "";
        public String searchApPaterno { get { return _searchApPaterno; } set { _searchApPaterno = value; NotifyPropertyChanged("searchApPaterno"); } }
        
        public String _searchApMaterno = "";
        public String searchApMaterno { get { return _searchApMaterno; } set { _searchApMaterno = value; NotifyPropertyChanged("searchApMaterno"); } }
        
        public int _searchTipoDocumento = 0;
        public int searchTipoDocumento { get { return _searchTipoDocumento; } set { _searchTipoDocumento = value; NotifyPropertyChanged("searchTipoDocumento"); } }
        #endregion

        #region Manejo de los Tabs
        public enum Tab
        {
            //Pestañas virtuales:
            //0       1        2          3
            BUSQUEDA, AGREGAR, MODIFICAR, DETALLES
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
                if (value == Tab.DETALLES && cliente == null)
                {

                }
                _statusTab = value;
                //Si cambió el estado de las pestañas también cambio los Header
                //Si la pestaña es para agregar nuevo, limpio los input
                switch (_statusTab)
                {
                    case Tab.BUSQUEDA: detallesTabHeader = soloSeleccionarCliente?"Detalles":"Agregar"; break;//Si es agregar, creo un nuevo objeto Cliente
                    case Tab.AGREGAR: detallesTabHeader = "Agregar"; cliente = new Cliente(); break;//Si es agregar, creo un nuevo objeto Cliente
                    case Tab.MODIFICAR: detallesTabHeader = "Modificar"; break;
                    case Tab.DETALLES: detallesTabHeader = "Detalles"; break;
                }
                NotifyPropertyChanged("statusTab");
                //Cuando se cambia el status, tambien se tiene que actualizar el currentIndex del tab
                NotifyPropertyChanged("currentIndexTab"); //Hace que cambie el tab automaticamente
            }
        }
        //Usado para mover los tabs de acuerdo a las acciones realizadas
        public int currentIndexTab
        {
            get
            {
                return _statusTab == Tab.BUSQUEDA ? 0 : 1;
            }
            set
            {
                if (soloSeleccionarCliente)
                {
                    statusTab = value == 0 ? Tab.BUSQUEDA : Tab.DETALLES;
                }
                else
                {
                    statusTab = value == 0 ? Tab.BUSQUEDA : Tab.AGREGAR;
                }
            }
        }
        private String _detallesTabHeader = ""; //Default
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

        #region Lista Clientes y Edicion de Cliente
        private Cliente _cliente;
        public Cliente cliente
        {
            get
            {
                return _cliente;
            }
            set
            {
                _cliente = value;
                if( value.id_ubigeo != null ){
                    String id_distrito = value.id_ubigeo;
                    String id_provincia = value.UbigeoDistrito.id_ubig_provincia;
                    String id_departamento = value.UbigeoDistrito.UbigeoProvincia.id_ubig_departamento;
                    distritos = MV_ClienteService.distritos.Where(distrito => distrito.id_ubig_provincia.Equals(id_provincia)) ;
                }
                NotifyPropertyChanged("cliente");
            }
        }

        private IEnumerable<Cliente> _listaClientes;
        public IEnumerable<Cliente> listaClientes
        {
            get
            {
                String searchTipoDocumento = this.searchTipoDocumento == 1 ? "DNI" : (this.searchTipoDocumento == 2 ? "RUC" : "");
                _listaClientes = MV_ClienteService.buscarClientes(searchNroDoc, searchNombre, searchApPaterno, searchApMaterno, searchTipoDocumento);
                
                return _listaClientes;
            }
            set
            {
                _listaClientes = value;
                NotifyPropertyChanged("listaClientes");
            }
        }
        #endregion
        
        #region RalayCommand
        RelayCommand _actualizarListaClientesCommand;
        public ICommand actualizarListaClientesCommand
        {
            get
            {
                if (_actualizarListaClientesCommand == null)
                {
                    _actualizarListaClientesCommand = new RelayCommand(param => NotifyPropertyChanged("listaClientes"));
                }
                return _actualizarListaClientesCommand;
            }
        }
        RelayCommand _viewEditClienteCommand;
        public ICommand viewEditClienteCommand
        {
            get
            {
                if (_viewEditClienteCommand == null)
                {
                    _viewEditClienteCommand = new RelayCommand(viewEditCliente);
                }
                return _viewEditClienteCommand;
            }
        }
        RelayCommand _saveClienteCommand;
        public ICommand saveClienteCommand
        {
            get
            {
                if (_saveClienteCommand == null)
                {
                    _saveClienteCommand = new RelayCommand(saveCliente, canSaveExecute);
                }
                return _saveClienteCommand;
            }
        }

        RelayCommand _cancelClienteCommand;
        public ICommand cancelClienteCommand
        {
            get
            {
                if (_cancelClienteCommand == null)
                {
                    _cancelClienteCommand = new RelayCommand(cancelCliente);
                }
                return _cancelClienteCommand;
            }
        }
        #endregion

        #region Comandos
        
        public void viewEditCliente(Object id)
        {
            try
            {
                this.cliente = listaClientes.Single(cliente => cliente.id == (int)id);
                if (this.cliente.id_ubigeo != null)
                {
                    selectedProvincia = this.cliente.UbigeoDistrito.UbigeoProvincia;
                    selectedDepartamento = selectedProvincia.UbigeoDepartamento;
                }
                if( soloSeleccionarCliente )
                    this.statusTab = Tab.DETALLES;
                else
                    this.statusTab = Tab.MODIFICAR;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        public void saveCliente(Object obj)
        {

            if (soloSeleccionarCliente)
            {

            }
            else
            {

                if (cliente.id > 0)//Si existe
                {
                    if (!MV_ClienteService.enviarCambios())
                    {
                        MessageBox.Show("No se pudo actualizar el cliente");
                    }
                    else
                    {
                        MessageBox.Show("El cliente fue guardado con éxito");
                    }
                }
                else
                {
                    if (!MV_ClienteService.insertarCliente(cliente))
                    {
                        MessageBox.Show("No se pudo agregar el nuevo cliente");
                    }
                    else
                    {
                        MessageBox.Show("El cliente fue agregado con éxito");
                    }
                }
            }
        }
        public void cancelCliente(Object obj)
        {
            this.statusTab = Tab.BUSQUEDA;
            listaClientes = MV_ClienteService.listaClientes;
        }

        private bool canSaveExecute(object obj)
        {
            if (soloSeleccionarCliente)
            {
                return cliente!=null;
            }
            return base.UIValidationErrorCount == 0 && this.cliente.Errors.Count == 0;
        }
        #endregion

        
    }
}
