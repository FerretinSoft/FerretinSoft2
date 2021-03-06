﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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
        private String _searchApellidosMat = "";
        public String searchApellidosMat { get { return _searchApellidosMat; } set { _searchApellidosMat = value; } }

        private String _searchNombreUsuario = null;
        public String searchNombreUsuario { get { return _searchNombreUsuario; } set { _searchNombreUsuario = value; } }

        private Perfil _searchPerfil = null;
        public Perfil searchPerfil { get { return _searchPerfil; } set { _searchPerfil = value; NotifyPropertyChanged("searchPerfil"); } }

        private String _searchNombres = "";
        public String searchNombres { get { return _searchNombres; } set { _searchNombres = value; } }

        private String _searchApellidos = "";
        public String searchApellidos { get { return _searchApellidos; } set { _searchApellidos = value; } }

        private int _searchEstado = 0;
        public int searchEstado { get { return _searchEstado; } set { _searchEstado = value; NotifyPropertyChanged("searchEstado"); } }
        #endregion
        /*************************************************/
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
                    case tabs.BUSQUEDA: detallesTabHeader = "Agregar"; usuario = new Usuario(); usuarioImagen = null; break;//Si es agregar, creo un nuevo objeto Usuario
                    case tabs.AGREGAR: detallesTabHeader = "Agregar"; usuario = new Usuario(); break; //Si es agregar, creo un nuevo objeto Usuario
                    case tabs.MODIFICAR: detallesTabHeader = "Modificar"; usuarioImagen = null; break;
                    case tabs.DETALLES: detallesTabHeader = "Detalles"; break;
                    default: detallesTabHeader = "Agregar"; usuario = new Usuario(); break;//Si es agregar, creo un nuevo objeto Cliente
                }
                //Cuando se cambia el status, tambien se tiene que cambiar el currentIndex del tab
                //currentIndexTab = _statusTab == 0 ? 0 : 1;
                NotifyPropertyChanged("statusTab");
                //Cuando se cambia el status, tambien se tiene que actualizar el currentIndex del tab
                NotifyPropertyChanged("currentIndexTab"); //Hace que cambie el tab automaticamente
                NotifyPropertyChanged("editEmpleadoEnabled");
                NotifyPropertyChanged("editEmpleadoEnabled2");
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
        /*************************************************/
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
                NotifyPropertyChanged("usuarioImagen");
            }
        }
        /**************************************************/
        public IEnumerable<Perfil> perfiles
        {
            get
            {
                //Creo una nueva secuencia
                var sequence = Enumerable.Empty<Perfil>();
                //Primero agrego un item de Todos para que salga al inicio
                //Pongo el ID en 0 para que al buscar, no filtre nada cuando se selecciona todos
                IEnumerable<Perfil> items = new Perfil[] { new Perfil { id = 0, nombre = "Todos" } };
                //Luego concateno el itemcon los elementos del combobox
                return items.Concat(MS_UsuarioService.obtenerPerfiles());
            }
        }

        /**************************************************/
        public IEnumerable<Perfil> perfilesSinTodos
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
                _listaUsuarios = MS_UsuarioService.buscar(searchNombreUsuario, searchPerfil, searchNombres, searchApellidos, searchApellidosMat, searchEstado);
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
        /*************************************************/
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
        /*************************************************/
        RelayCommand _buscarNombreUsuarioCommand;
        public ICommand buscarNombreUsuarioCommand
        {
            get
            {
                if (_buscarNombreUsuarioCommand == null)
                {
                    _buscarNombreUsuarioCommand = new RelayCommand(buscarNombreUsuario);
                }
                return _buscarNombreUsuarioCommand;
            }
        }
        /*************************************************/
        RelayCommand _restbContrasenaCommand;
        public ICommand restbContrasenaCommand
        {
            get
            {
                if (_restbContrasenaCommand == null)
                {
                    _restbContrasenaCommand = new RelayCommand(restbContrasena);
                }
                return _restbContrasenaCommand;
            }
        }

        #endregion
        /*************************************************/
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

            /********************************************************/
            /*Para actualizar un usuario existente*/
            if (usuario.id > 0)//Si existe
            {
                ComunService.idVentana(5);
                /***************************************/
                List<Parametro> listaParametros;
                listaParametros = MS_ParametroService.obtenerListaParametros().ToList();
                usuario.intentosCon = Convert.ToInt16(listaParametros[0].valor);
                /***************************************/

                if (!MS_UsuarioService.enviarCambios())
                {
                    MessageBox.Show("No se pudo actualizar el usuario revise los campos");
                }
                else
                {
                    //ComunService.idVentana(5);
                    MessageBox.Show("El usuario fue actualizado con éxito");
                    this.statusTab = tabs.BUSQUEDA;
                    listaUsuarios = MS_UsuarioService.listaUsuarios;
                }
            }
            /********************************************************/
            /*Para agregar un usuario nuevo*/
            else
            {
                ComunService.idVentana(4);
                /***************************************/
                /********************** Validar cuando no se ingresa ningun dato obligatorio **********************************/
                if ((this._dniEmpleado == "" || this._dniEmpleado == null))
                {
                    MessageBox.Show("Ingrese datos en los campos obligatorios, por favor");
                    return;
                }
                else
                {
                    /************************ Validar cuando no se ingresa dni correcto  ********************************/
                    if (this._dniEmpleado == "")
                    {
                        MessageBox.Show("Ingrese dni de usuario correcto, por favor");
                        return;
                    }
                    /********************* Validar cuando se ingresa estado null **********************/
                    if (usuario.estado == null)
                    {
                        MessageBox.Show("Ingrese estado de usuario correcto, por favor");
                        return;
                    }
                    /********************* Validar cuando no se ingresa nombre de usuario correcto **********************/
                    if (usuario.Perfil == null)
                    {
                        MessageBox.Show("Ingrese perfil de usuario correcto, por favor");
                        return;
                    }
                    ///********************* Validar cuando se ingresa nombre de usuario y no se dio el boton de verificar **********************/
                    //if (!MS_UsuarioService.validarUserName(usuario))
                    //{
                    //    MessageBox.Show("Verifique, el usuario ya existe");
                    //    return;
                    //}
                    /* Validar cuando no se ha realizado la busqueda de empleado o la verificacion de nombre de usuario mediante los botones */
                    if (this._busquedaExitosa < 1)
                    {
                        MessageBox.Show("Realize la busqueda de empleado, por favor");
                        return;
                    }

                }
                ///***************************************/
                //if (this._dniEmpleado == "" || usuario.nombre == null || (this._busquedaExitosa < 2 ))
                //    MessageBox.Show("Ingrese dni de usuario, correcto, por favor");
                ///***************************************/
                //else
                //{
                /*valores por defecto */
                usuario.contrasena = MS_UsuarioService.encrypt("ferretinSoft");
                List<Parametro> listaParametros;
                listaParametros = MS_ParametroService.obtenerListaParametros().ToList();
                usuario.intentosCon = Convert.ToInt16(listaParametros[0].valor);
                usuario.ultimoCambioContrasena = DateTime.Now;
                /**********************/

                if (!MS_UsuarioService.insertarUsuario(usuario))
                {
                    MessageBox.Show("No se pudo agregar, el usuario ya existe");
                }
                else
                {
                    //ComunService.idVentana(4);
                    MessageBox.Show("El usuario fue agregado con éxito");
                    this.statusTab = tabs.BUSQUEDA;
                    listaUsuarios = MS_UsuarioService.listaUsuarios;
                }
                //}
            }
            //NotifyPropertyChanged("listaUsuarios");
        }
        /**************************************************/
        public void cancelUsuario(Object obj)
        {
             MessageBoxResult result =MessageBox.Show("Al salir, perderá todos los datos ingresados. ¿Desea continuar?",
            "ATENCIÓN", MessageBoxButton.OKCancel, MessageBoxImage.Warning, MessageBoxResult.Cancel);
             if (result == MessageBoxResult.OK)
             {
                 this.statusTab = tabs.BUSQUEDA;
                 listaUsuarios = MS_UsuarioService.listaUsuarios;
             }
        }
        /**************************************************/
        public void buscarNombreUsuario(Object obj)
        {
            if (usuario.nombre == null || usuario.nombre == "")
            {
                MessageBox.Show("Usuario no puede ser vacio");
                busquedaExitosa = busquedaExitosa + 1;
            }
            else
            {
                if (!MS_UsuarioService.validarUserName(usuario))
                {
                    MessageBox.Show("Usuario Existente");
                }
                else
                {
                    MessageBox.Show("Usuario Disponible");
                    busquedaExitosa = busquedaExitosa + 1;
                }
            }

            //NotifyPropertyChanged("listaUsuarios");       
        }
        /**************************************************/

        public void restbContrasena(Object obj)
        {
            List<Parametro> listaParametros;
            listaParametros = MS_ParametroService.obtenerListaParametros().ToList();

            if (usuario.id > 0)//Si existe
            {
                usuario.contrasena = MS_UsuarioService.encrypt("ferretinSoft");
                usuario.intentosCon = Convert.ToInt16(listaParametros[0].valor);
                usuario.ultimoCambioContrasena = DateTime.Now;
                ComunService.idVentana(39);

                if (!MS_UsuarioService.enviarCambios())
                {
                    MessageBox.Show("No se puedo cambiar la contraseña");
                }
                else
                {
                    MessageBox.Show("La contraseña se restablecio con éxito");
                }
            }
        }
        #endregion
        /*************************************************/      
        public String _dniEmpleado = "";
        public String dniEmpleado
        {
            get { return _dniEmpleado; }
            set { _dniEmpleado = value; NotifyPropertyChanged("dniEmpleado"); }
        }
        /************************************************/
        public bool editEmpleadoEnabled
        {
            get
            {
                return statusTab == tabs.AGREGAR ? true : false;
            }
        }
        /************************************************/
        public bool editEmpleadoEnabled2
        {
            get
            {
                return statusTab == tabs.AGREGAR ? false : true;
            }
        }
        /************************************************/
        public int _busquedaExitosa = 0;
        public int busquedaExitosa
        {
            get { return _busquedaExitosa; }
            set { _busquedaExitosa = value; NotifyPropertyChanged("busquedaExitosa"); }
        }
        /*************************************************/        
        private ImageSource _usuarioImagen;
        public ImageSource usuarioImagen
        {
            get
            {
                try
                {
                    if (this.usuario.Empleado.foto != null)
                    {
                        MemoryStream strm = new MemoryStream();
                        strm.Write(usuario.Empleado.foto.ToArray(), 0, usuario.Empleado.foto.Length);
                        strm.Position = 0;
                        System.Drawing.Image img = System.Drawing.Image.FromStream(strm);

                        BitmapImage bitmapImage = new BitmapImage();
                        bitmapImage.BeginInit();
                        MemoryStream memoryStream = new MemoryStream();
                        img.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Bmp);
                        memoryStream.Seek(0, SeekOrigin.Begin);
                        bitmapImage.StreamSource = memoryStream;
                        bitmapImage.EndInit();

                        _usuarioImagen = bitmapImage;
                    }
                }
                catch (Exception)
                {

                }
                return _usuarioImagen;
            }
            set
            {
                _usuarioImagen = value;
                NotifyPropertyChanged("usuarioImagen");
            }
        }
        /*************************************************/
        void buscarCliente(object var)
        {
            if (dniEmpleado.Trim().Length > 0)
            {
                Empleado empleado = MR_SharedService.obtenerEmpleadoPorDNI(dniEmpleado);
                if (empleado != null)
                {
                    usuario.Empleado = empleado;
                    busquedaExitosa = busquedaExitosa + 1;
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

