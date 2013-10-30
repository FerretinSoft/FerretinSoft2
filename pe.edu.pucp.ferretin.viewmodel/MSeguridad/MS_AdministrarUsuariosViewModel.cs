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
    public class MS_AdministrarUsuariosViewModel : ViewModelBase
    {
        #region Valores para el cuadro de Búsqueda
        private String _searchCodigo = "";
        public String searchCodigo { get { return _searchCodigo; } set { _searchCodigo = value; } }

        private String _searchNombreUsuario = "";
        public String searchNombreUsuario { get { return _searchNombreUsuario; } set { _searchNombreUsuario = value; } }

        private int _searchPerfil = 0;
        public int searchPerfil { get { return _searchPerfil; } set { _searchPerfil = value; NotifyPropertyChanged("searchPerfil"); } }

        private String _searchNombres = "";
        public String searchNombres { get { return _searchNombres; } set { _searchNombres = value; } }

        private String _searchApellidos = "";
        public String searchApellidos { get { return _searchApellidos; } set { _searchApellidos = value; } }

        private int _searchEstado = 0;
        public int searchEstado { get { return _searchEstado; } set { _searchEstado = value; NotifyPropertyChanged("searchEstado"); } }
        #endregion

        public String _dniEmpleado = "";
        public String dniEmpleado
        {
            get { return _dniEmpleado; }
            set { _dniEmpleado = value; NotifyPropertyChanged("dniEmpleado"); }
        }

        public bool editEmpleadoEnabled
        {
            get
            {
                return statusTab == tabs.AGREGAR ? true : false;
            }
        }

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
                    case tabs.BUSQUEDA: detallesTabHeader = "Agregar"; usuario = new Usuario(); break;//Si es agregar, creo un nuevo objeto Usuario
                    case tabs.AGREGAR: detallesTabHeader = "Agregar"; usuario = new Usuario(); break;//Si es agregar, creo un nuevo objeto Usuario
                    case tabs.MODIFICAR: detallesTabHeader = "Modificar"; break;
                    case tabs.DETALLES: detallesTabHeader = "Detalles"; break;
                    default: detallesTabHeader = "Agregar"; usuario = new Usuario(); break;//Si es agregar, creo un nuevo objeto Cliente
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

        #region Lista de Usuarios y Edición de Usuarios
        /**************************************************/
        private Usuario _usuario = new Usuario();
        public Usuario usuario
        {
            get
            {
                return _usuario;
            }
            set
            {
                _usuario = value;
                if (value != null && value.Empleado != null)
                {
                    dniEmpleado = value.Empleado.dni;
                }
                else
                {
                    dniEmpleado = "";
                }
                NotifyPropertyChanged("usuario");
            }
        }
        /**************************************************/
        public IEnumerable<Perfil> perfiles
        {
            get
            {
                return MS_UsuarioService.obtenerPerfiles();
            }
        }
        /**************************************************/
        private IEnumerable<Usuario> _listaUsuarios;
        public IEnumerable<Usuario> listaUsuarios
        {
            get
            {
                _listaUsuarios = MS_UsuarioService.buscar(searchCodigo, searchNombreUsuario, searchPerfil, searchNombres, searchApellidos, searchEstado);
                return _listaUsuarios;
            }
            set
            {
                _listaUsuarios = value;
                NotifyPropertyChanged("listaUsuarios");
            }
        }
        /**************************************************/
        #endregion
        
        #region RalayCommand
        /**************************************************/
        RelayCommand _actualizarListaUsuariosCommand;
        public ICommand actualizarListaUsuariosCommand
        {
            get
            {
                if (_actualizarListaUsuariosCommand == null)
                {
                    _actualizarListaUsuariosCommand = new RelayCommand(param => NotifyPropertyChanged("listaUsuarios"));
                }
                return _actualizarListaUsuariosCommand;
            }
        }
        /**************************************************/
        RelayCommand _agregarUsuarioCommand;
        public ICommand agregarUsuarioCommand
        {
            get
            {
                if (_agregarUsuarioCommand == null)
                {
                    _agregarUsuarioCommand = new RelayCommand(p => statusTab = tabs.AGREGAR);
                }
                return _agregarUsuarioCommand;
            }
        }
        /**************************************************/
        RelayCommand _viewEditUsuarioCommand;
        public ICommand viewEditUsuarioCommand
        {
            get
            {
                if (_viewEditUsuarioCommand == null)
                {
                    _viewEditUsuarioCommand = new RelayCommand(viewEditUsuario);
                }
                return _viewEditUsuarioCommand;
            }
        }
        /**************************************************/
        RelayCommand _saveUsuarioCommand;
        public ICommand saveUsuarioCommand
        {
            get
            {
                if (_saveUsuarioCommand == null)
                {
                    _saveUsuarioCommand = new RelayCommand(saveUsuario);
                }
                return _saveUsuarioCommand;
            }
        }
        /**************************************************/
        RelayCommand _cancelUsuarioCommand;
        public ICommand cancelUsuarioCommand
        {
            get
            {
                if (_cancelUsuarioCommand == null)
                {
                    _cancelUsuarioCommand = new RelayCommand(cancelUsuario);
                }
                return _cancelUsuarioCommand;
            }
        }
        /**************************************************/
        RelayCommand _buscarClienteCommand;
        public ICommand buscarClienteCommand
        {
            get
            {
                if (_buscarClienteCommand == null)
                {
                    _buscarClienteCommand = new RelayCommand(buscarCliente);
                }
                return _buscarClienteCommand;
            }
        }
        #endregion

        #region Comandos
        /**************************************************/
        public void viewEditUsuario(Object id)
        {
            try
            {
                this.usuario = listaUsuarios.Single(usuario => usuario.id == (int)id);
                //if (this.almacen.id_ubigeo != null)
                //{
                //    selectedProvincia = this.almacen.UbigeoDistrito.UbigeoProvincia;
                //    selectedDepartamento = selectedProvincia.UbigeoDepartamento;
                //}
                this.statusTab = tabs.MODIFICAR;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        /**************************************************/
        public void saveUsuario(Object obj)
        {
            /*Para actualizar un usuario existente*/
            if (usuario.id > 0)//Si existe
            {
                if (!MS_UsuarioService.enviarCambios())
                {
                    MessageBox.Show("No se pudo actualizar el usuario");
                }
                else
                {
                    MessageBox.Show("El usuario fue actualizado con éxito");
                }
            }
            /*Para agregar un usuario nuevo*/
            else
            {
                if (!MS_UsuarioService.insertarUsuario(usuario))
                {
                    MessageBox.Show("No se pudo agregar el nuevo usuario");
                }
                else
                {
                    MessageBox.Show("El usuario fue agregado con éxito");
                }
            }
            NotifyPropertyChanged("listaUsuarios");
        }
        /**************************************************/
        public void cancelUsuario(Object obj)
        {
            this.statusTab = tabs.BUSQUEDA;
            listaUsuarios = MS_UsuarioService.listaUsuarios;
        }
        /**************************************************/
        #endregion

        void buscarCliente(object var)
        {
            if (dniEmpleado.Trim().Length > 0)
            {
                Empleado empleado = MR_SharedService.obtenerEmpleadoPorDNI(dniEmpleado);
                if (empleado != null)
                {
                    usuario.Empleado = empleado; 
                }
                else
                {
                    MessageBox.Show("No se encontro un cliente con el DNI ingresado");
                }
            }
            else
            {
                MessageBox.Show("Debe ingresar el DNI de algún empleado");
            }
        }

    }
}

