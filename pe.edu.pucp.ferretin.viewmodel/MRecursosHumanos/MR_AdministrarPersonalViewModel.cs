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
        public String _searchDNI = "";
        public String searchDNI { get { return _searchDNI; } set { _searchDNI = value; NotifyPropertyChanged("searchDNI"); } }

        public String _searchNombre = "";
        public String searchNombre { get { return _searchNombre; } set { _searchNombre = value; NotifyPropertyChanged("searchNombre"); } }

        public String _searchCodigo = "";
        public String searchCodigo { get { return _searchCodigo; } set { _searchCodigo = value; NotifyPropertyChanged("searchCodigo"); } }

        public Cargo _searchCargo;
        public Cargo searchCargo { get { return _searchCargo; } set { _searchCargo = value; NotifyPropertyChanged("searchCargo"); } }

        public Tienda _searchTienda;
        public Tienda searchTienda { get { return _searchTienda; } set { _searchTienda = value; NotifyPropertyChanged("searchTienda"); } }
        #endregion

        public IEnumerable<Cargo> listaCargos
        {
            get
            {
                //Creo una nueva secuencia
                var sequence = Enumerable.Empty<Cargo>();
                //Primero agrego un item de Todos para que salga al inicio
                //Pongo el ID en 0 para que al buscar, no filtre nada cuando se selecciona todos
                IEnumerable<Cargo> items = new Cargo[] { new Cargo { id = 0, nombre = "Todos" } };
                //Luego concateno el itemcon los elementos del combobox
                return items.Concat(MR_CargTieService.cargos);
            }
        }


        public IEnumerable<Tienda> listaTiendas
        {
            get
            {
                //Creo una nueva secuencia
                var sequence = Enumerable.Empty<Tienda>();
                //Primero agrego un item de Todos para que salga al inicio
                //Pongo el ID en 0 para que al buscar, no filtre nada cuando se selecciona todos
                IEnumerable<Tienda> items = new Tienda[] { new Tienda { id = 0, nombre = "Todos" } };
                //Luego concateno el itemcon los elementos del combobox
                return items.Concat(almacenes);
            }
        }

        public IEnumerable<GradoInstruccion> gradosInstruccion
        {
            get
            {
                return MR_ComunService.gradosInstruccion;
                
            }
        }

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
                    case Tab.BUSQUEDA: detallesTabHeader = "Agregar"; empleado = new Empleado(); break;//Si es agregar, creo un nuevo objeto Empleado
                    case Tab.AGREGAR: detallesTabHeader = "Agregar"; empleado = new Empleado(); break;//Si es agregar, creo un nuevo objeto Empleado
                    case Tab.MODIFICAR: detallesTabHeader = "Modificar"; break;
                    case Tab.DETALLES: detallesTabHeader = "Detalles"; break;
                    default: detallesTabHeader = "Agregar"; empleado = new Empleado(); break;//Si es agregar, creo un nuevo objeto Empleado
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

        #region Lista Empleados y Edicion de Empleado
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
                int cod = 0;
                if (searchCodigo.Length > 0)
                {
                    cod = int.Parse(searchCodigo);
                }
                _listaEmpleados = MR_EmpleadoService.buscarEmpleados(searchDNI, searchNombre, cod, searchCargo, searchTienda);

                return _listaEmpleados;
            }
            set
            {
                _listaEmpleados = value;
                NotifyPropertyChanged("listaEmpleados");
            }
        }
        #endregion

        #region RalayCommand
        RelayCommand _actualizarListaEmpleadosCommand;
        public ICommand actualizarListaEmpleadosCommand
        {
            get
            {
                if (_actualizarListaEmpleadosCommand == null)
                {
                    _actualizarListaEmpleadosCommand = new RelayCommand(param => NotifyPropertyChanged("listaEmpleados"));
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
        RelayCommand _cancelEmpleadoCommand;
        public ICommand cancelEmpleadoCommand
        {
            get
            {
                if (_cancelEmpleadoCommand == null)
                {
                    _cancelEmpleadoCommand = new RelayCommand(cancelEmpleado);
                }
                return _cancelEmpleadoCommand;
            }
        }
        #endregion

        #region Comandos

        public void viewEditEmpleado(Object codEmpleado)
        {
            try
            {
                this.empleado = listaEmpleados.Single(empleado => empleado.codEmpleado == (int)codEmpleado);
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
