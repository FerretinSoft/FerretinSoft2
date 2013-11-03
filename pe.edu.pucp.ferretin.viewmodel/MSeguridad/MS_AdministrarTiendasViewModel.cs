using pe.edu.pucp.ferretin.controller;
using pe.edu.pucp.ferretin.controller.MRecursosHumanos;
using pe.edu.pucp.ferretin.controller.MSeguridad;
using pe.edu.pucp.ferretin.model;
using pe.edu.pucp.ferretin.viewmodel.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace pe.edu.pucp.ferretin.viewmodel.MSeguridad
{
    public class MS_AdministrarTiendasViewModel : ViewModelBase
    {
        #region Valores para el cuadro de Búsqueda

        public String _dniJefe = "";
        public String dniJefe { get { return _dniJefe; } set { _dniJefe = value; NotifyPropertyChanged("dniJefe"); } }
        private String _searchCodTienda = "";
        public String searchCodTienda { get { return _searchCodTienda; } set { _searchCodTienda = value; } }
        public String _searchNombre = "";
        public String searchNombre { get { return _searchNombre; } set { _searchNombre = value; } }
        private int _searchEstado = 0;
        public int searchEstado { get { return _searchEstado; } set { _searchEstado = value; NotifyPropertyChanged("searchEstado"); } }
        public UbigeoDepartamento searchDepartamento
        {
            get 
            {
                return ComunService.departamentos.Single(d => d.nombre.Equals("LIMA"));
            }
        }
        
        private UbigeoProvincia _searchProvincia;
        public UbigeoProvincia searchProvincia
        {
            get
            {
                return _searchProvincia;
            }
            set
            {
                _searchProvincia = value;
                NotifyPropertyChanged("searchProvincia");
                distritos = from d in ComunService.distritos where d.id_ubig_provincia == value.id select d;
            }
        }
        private int _searchTipo = 0;
        public int searchTipo { get { return _searchTipo; } set { _searchTipo = value; NotifyPropertyChanged("searchTipo"); } }
        public UbigeoDistrito _searchDistrito;
        public UbigeoDistrito searchDistrito
        {
            get
            {
                return _searchDistrito;
            }
            set
            {
                _searchDistrito = value;
                NotifyPropertyChanged("searchDistrito");
                NotifyPropertyChanged("distritos");
            }
        }
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
                    case Tab.BUSQUEDA: detallesTabHeader = "Agregar"; almacen = new Tienda(); break;//Si es agregar, creo un nuevo objeto Almacen
                    case Tab.AGREGAR: detallesTabHeader = "Agregar"; almacen = new Tienda(); break;//Si es agregar, creo un nuevo objeto Almacen
                    case Tab.MODIFICAR: detallesTabHeader = "Modificar"; break;
                    case Tab.DETALLES: detallesTabHeader = "Detalles"; break;
                    default: detallesTabHeader = "Agregar"; almacen = new Tienda(); break;//Si es agregar, creo un nuevo objeto Almacen
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

        #region Lista de Tiendas y Edición de Tiendas
        private Tienda _almacen;
        public Tienda almacen
        {
            get
            {
                return _almacen;
            }
            set
            {
                _almacen = value;
                if (value.id_ubigeo != null)
                {
                    String id_distrito = value.id_ubigeo;
                    String id_provincia = value.UbigeoDistrito.id_ubig_provincia;
                    String id_departamento = value.UbigeoDistrito.UbigeoProvincia.id_ubig_departamento;
                    distritos = MS_TiendaService.distritos.Where(distrito => distrito.id_ubig_provincia.Equals(id_provincia));
                }
                if (almacen.Empleado != null)
                {
                    almacenEmpleadoDni = almacen.Empleado.dni;
                }
                NotifyPropertyChanged("almacen");
            }
        }        

        private IEnumerable<Tienda> _listaAlmacenes;
        public IEnumerable<Tienda> listaAlmacenes
        {
            get
            {
                _listaAlmacenes = MS_TiendaService.buscar(searchCodTienda, searchNombre,searchDistrito,searchTipo,searchEstado);
                return _listaAlmacenes;
            }
            set
            {
                _listaAlmacenes = value;
                NotifyPropertyChanged("listaAlmacenes");
            }
        }
        #endregion

        #region Para la seleccion del Jefe de Tienda
        private String _almacenEmpleadoDni = "";
        public String almacenEmpleadoDni
        {
            get
            {
                return _almacenEmpleadoDni;
            }
            set
            {
                _almacenEmpleadoDni = value;
                if (value.Length > 0) //Se puede hacer una validacion para que sea igual a 8
                {
                    Empleado empleado = MR_SharedService.obtenerEmpleadoPorDNI(value);;
                    if (empleado == null)
                    {
                        MessageBox.Show("No existe un empleado con el DNI " + value + ".");
                    }
                    else
                    {
                        almacen.Empleado = empleado;
                    }
                }
                NotifyPropertyChanged("almacenEmpleadoDni");
                NotifyPropertyChanged("almacen");
            }
        }
        #endregion

        #region RalayCommand
        RelayCommand _actualizarListaAlmacenesCommand;
        public ICommand actualizarListaAlmacenesCommand
        {
            get
            {
                if (_actualizarListaAlmacenesCommand == null)
                {
                    _actualizarListaAlmacenesCommand = new RelayCommand(param => NotifyPropertyChanged("listaAlmacenes"));
                }
                return _actualizarListaAlmacenesCommand;
            }
        }
        RelayCommand _agregarAlmacenCommand;
        public ICommand agregarAlmacenCommand
        {
            get
            {
                if (_agregarAlmacenCommand == null)
                {
                    _agregarAlmacenCommand = new RelayCommand(p=>statusTab=Tab.AGREGAR);
                }
                return _agregarAlmacenCommand;
            }
        }
        RelayCommand _viewEditAlmacenCommand;
        public ICommand viewEditAlmacenCommand
        {
            get
            {
                if (_viewEditAlmacenCommand == null)
                {
                    _viewEditAlmacenCommand = new RelayCommand(viewEditAlmacen);
                }
                return _viewEditAlmacenCommand;
            }
        }
        RelayCommand _saveAlmacenCommand;
        public ICommand saveAlmacenCommand
        {
            get
            {
                if (_saveAlmacenCommand == null)
                {
                    _saveAlmacenCommand = new RelayCommand(saveAlmacen);
                }
                return _saveAlmacenCommand;
            }
        }
        RelayCommand _cancelAlmacenCommand;
        public ICommand cancelAlmacenCommand
        {
            get
            {
                if (_cancelAlmacenCommand == null)
                {
                    _cancelAlmacenCommand = new RelayCommand(cancelAlmacen);
                }
                return _cancelAlmacenCommand;
            }
        }
        /**************************************************/
        RelayCommand _buscarJefeCommand;
        public ICommand buscarJefeCommand
        {
            get
            {
                if (_buscarJefeCommand == null)
                {
                    _buscarJefeCommand = new RelayCommand(buscarJefe);
                }
                return _buscarJefeCommand;
            }
        }        
        #endregion

        #region Comandos

        public void viewEditAlmacen(Object id)
        {
            try
            {
                this.almacen = listaAlmacenes.Single(almacen => almacen.id == (int)id);
                if (this.almacen.id_ubigeo != null)
                {
                    selectedProvincia = this.almacen.UbigeoDistrito.UbigeoProvincia;
                    selectedDepartamento = selectedProvincia.UbigeoDepartamento;
                }
                this.statusTab = Tab.MODIFICAR;                
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        public void saveAlmacen(Object obj)
        {            
            if (almacen.id > 0)//Si existe
            {
                if (VerificaHorario(almacen))
                {
                    if (!MS_TiendaService.enviarCambios())
                    {
                        MessageBox.Show("No se pudo actualizar la tienda");
                    }
                    else
                    {
                        MessageBox.Show("La tienda fue guardada con éxito");
                    }
                }
            }
            else
            {
                if (VerificaHorario(almacen))
                {
                    if (!MS_TiendaService.insertarAlmacen(almacen))
                    {
                        MessageBox.Show("No se pudo agregar la nueva tienda porque esta ya existe");
                    }
                    else
                    {
                        MessageBox.Show("La Tienda fue agregada con éxito");
                    }
                }
            }
            NotifyPropertyChanged("listaAlmacenes");
        }

        public void cancelAlmacen(Object obj)
        {
            this.statusTab = Tab.BUSQUEDA;
            listaAlmacenes = MS_TiendaService.listaTiendas;
        }
        #endregion

        void buscarJefe(object var)
        {
            if (dniJefe.Trim().Length > 0)
            {                
                Empleado empleado = MR_SharedService.obtenerEmpleadoPorDNI(dniJefe);
                if (empleado != null)
                {
                    
                    MessageBox.Show("El DNI ingresado no le pertenece a un Jefe");
                
                    almacen.Empleado = empleado;
                }
                else
                {
                    MessageBox.Show("No se encontro un Jefe con el DNI ingresado");
                }
            }
            else
            {
                MessageBox.Show("Debe ingresar el DNI de algún Jefe");                
            }
        }

        private Tienda _selectedTienda;
        public Tienda selectedTienda
        {
            get
            {                   
                _selectedTienda = almacenes.Single(t => t.id==this.almacen.id_abastecedor);
                return _selectedTienda;
            }
            set
            {
                _selectedTienda = value;
                NotifyPropertyChanged("selectedTienda");
                NotifyPropertyChanged("almacenes");
                this.almacen.id_abastecedor = _selectedTienda.id;
            }
        }

        public bool VerificaHorario(Tienda tienda)
        {
            if (tienda.lunesHoraInicio > tienda.lunesHoraFin ||
                tienda.martesHoraInicio > tienda.martesHoraFin ||
                tienda.miercolesHoraInicio > tienda.miercolesHoraFin ||
                tienda.juevesHoraInicio > tienda.juevesHoraFin ||
                tienda.viernesHoraInicio > tienda.viernesHoraFin ||
                tienda.sabadoHoraInicio > tienda.sabadoHoraFin ||
                tienda.domingoHoraInicio > tienda.domingoHoraFin)
            {
                MessageBox.Show("La hora Fin debe ser menor a la hora Inicio");
                return false;
            }
            return true;
        }


    }
}
