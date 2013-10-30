using pe.edu.pucp.ferretin.controller.MRecursosHumanos;
using pe.edu.pucp.ferretin.model;
using pe.edu.pucp.ferretin.viewmodel.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace pe.edu.pucp.ferretin.viewmodel.MRecursosHumanos
{
    public class MR_AdministrarPersonalViewModel : ViewModelBase
    {
        #region Constructor
        public MR_AdministrarPersonalViewModel()
        {
            _empleado = new Empleado();        
        }
        #endregion

        #region Valores para el cuadro de Búsqueda
        public String _searchDni = "";
        public String searchDni { get { return _searchDni; } set { _searchDni = value; NotifyPropertyChanged("searchDni"); } }
        
        public String _searchNombre = "";
        public String searchNombre { get { return _searchNombre; } set { _searchNombre = value; NotifyPropertyChanged("searchNombre"); } }

        public Cargo _searchCargo = null;
        public Cargo searchCargo { get { return _searchCargo; } set { _searchCargo = value; NotifyPropertyChanged("searchCargo"); } }

        public Tienda _searchTienda = null;
        public Tienda searchTienda { get { return _searchTienda; } set { _searchTienda = value; NotifyPropertyChanged("searchTienda"); } }

        public int _searchCodigo =0 ;
        public int searchCodigo { get { return _searchCodigo; } set { _searchCodigo = value; NotifyPropertyChanged("searchCodigo"); } }
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
                _statusTab = value;
                //Si cambió el estado de las pestañas también cambio los Header
                //Si la pestaña es para agregar nuevo, limpio los input
                switch (_statusTab)
                {
                    case Tab.BUSQUEDA: detallesTabHeader = "Agregar"; empleado = new Empleado(); break;//Si es agregar, creo un nuevo objeto Cliente
                    case Tab.AGREGAR: detallesTabHeader = "Agregar"; empleado = new Empleado(); break;//Si es agregar, creo un nuevo objeto Cliente
                    case Tab.MODIFICAR: detallesTabHeader = "Modificar"; break;
                    case Tab.DETALLES: detallesTabHeader = "Detalles"; break;
                    default: detallesTabHeader = "Agregar"; empleado = new Empleado(); break;//Si es agregar, creo un nuevo objeto Cliente
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
        #endregion


        #region Lista de Empleado y Edicion de Empleado
        private Empleado _empleado;
        public Empleado empleado
        {
            get
            {
                return _empleado;
            }
            set
            {
                _empleado = value;
                if (value.id_ubigeo_distrito != null)
                {
                    String id_distrito = value.id_ubigeo_distrito;
                    String id_provincia = value.UbigeoDistrito.id_ubig_provincia;
                    String id_departamento = value.UbigeoDistrito.UbigeoProvincia.id_ubig_departamento;
                    distritos = MR_EmpleadoService.distritos.Where(distrito => distrito.id_ubig_provincia.Equals(id_provincia));
                }
                NotifyPropertyChanged("empleado");
  
            }
        }

        private IEnumerable<Empleado> _listaEmpleados;
        public IEnumerable<Empleado> listaEmpleados
        {
            get
            {
              
                _listaEmpleados = MR_EmpleadoService.buscarEmpleados(searchDni, searchNombre, searchCargo, searchTienda, searchCodigo);
                return _listaEmpleados;
            }
            set
            {
                _listaEmpleados = value;
                NotifyPropertyChanged("lisaEmpleados");
            }
        }
        #endregion

        #region RelayCommand
        RelayCommand _actualizarListaEmpleadosCommand;
        public ICommand actualizarListaEmpleadosCommand
        {
            get
            {
                if (_actualizarListaEmpleadosCommand == null)
                {
                    _actualizarListaEmpleadosCommand = new RelayCommand(param => NotifyPropertyChanged("listasEmpleados"));
                }
                return _actualizarListaEmpleadosCommand;
            }
        }
        RelayCommand _viewEditEmpleadoCommand;
        public ICommand viewEditEmpleadoCommand
        {
            get
            {
                if (_viewEditEmpleadoCommand == null)
                {
                    _viewEditEmpleadoCommand = new RelayCommand(viewEditEmpleado);
                }
                return _viewEditEmpleadoCommand;
            }
        }
        RelayCommand _saveEmpleadoCommand;
        public ICommand saveEmpleadoCommand
        {
            get
            {
                if (_saveEmpleadoCommand == null)
                {
                    _saveEmpleadoCommand = new RelayCommand(saveEmpleado);
                }
                return _saveEmpleadoCommand;
            }
        }
        RelayCommand _cancelClienteCommand;
        public ICommand cancelClienteCommand
        {
            get
            {
                if (_cancelClienteCommand == null)
                {
                    _cancelClienteCommand = new RelayCommand(cancelEmpleado);
                }
                return _cancelClienteCommand;
            }
        }
        #endregion

        #region Comandos
        public void viewEditEmpleado(Object id)
        {
            try
            {
                this.empleado = listaEmpleados.Single(empleado => empleado.id == (int)id);
                if (this.empleado.id_ubigeo_distrito != null)
                {
                    selectedProvincia = this.empleado.UbigeoDistrito.UbigeoProvincia;
                    selectedDepartamento = selectedProvincia.UbigeoDepartamento;
                }

                this.statusTab = Tab.MODIFICAR;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        public void saveEmpleado(Object obj)
        {

            if (empleado.id > 0)//Si existe
            {
                if (!MR_EmpleadoService.enviarCambios())
                {
                    MessageBox.Show("No se pudo actualizar el empleado");
                }
                else
                {
                    MessageBox.Show("El empleado fue guardado con éxito");
                }
            }
            else
            {
                if (!MR_EmpleadoService.insertarEmpleado(empleado))
                {
                    MessageBox.Show("No se pudo agregar el nuevo empleado");
                }
                else
                {
                    MessageBox.Show("El empleado fue agregado con éxito");
                }
            }
        }
        public void cancelEmpleado(Object obj)
        {
            this.statusTab = Tab.BUSQUEDA;
            listaEmpleados = MR_EmpleadoService.listaEmpleados;
        }
        #endregion

    }
}
