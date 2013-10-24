using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using System.ComponentModel;
using pe.edu.pucp.ferretin.controller;
using pe.edu.pucp.ferretin.model;
using System.Threading;
using System.Windows.Threading;

namespace pe.edu.pucp.ferretin.view.MSeguridad
{

    /**************************************************************************************************************/
    public class MS_UsuariosViewModel : INotifyPropertyChanged
    {
        #region Valores para el cuadro de Búsqueda
        public String searchCodigo { get; set; }
        public String searchNombreUsuario { get; set; }
        public int searchPerfil { get; set; }
        public String searchNombres { get; set; }
        public String searchApellidos { get; set; }
        public int searchEstado { get; set; }
        #endregion

        public enum tabs
        {
            //Pestañas virtuales:
            //0       1        2          3
            BUSQUEDA, AGREGAR, MODIFICAR, DETALLES
        }
        private int _statusTab = (int)tabs.BUSQUEDA; //pestaña default 
        public int statusTab
        {
            get
            {
                return _statusTab;
            }
            set
            {
                _statusTab = value == 0 ? 0 : 1;
                //Si cambió el estado de las pestañas también cambio los Header
                //Si la pestaña es para agregar nuevo, limpio los input
                switch (value)
                {
                    case (int)tabs.BUSQUEDA: detallesTabHeader = "Agregar"; usuario = new Usuario(); break;//Si es agregar, creo un nuevo objeto Cliente
                    case (int)tabs.AGREGAR: detallesTabHeader = "Agregar"; usuario = new Usuario(); break;//Si es agregar, creo un nuevo objeto Cliente
                    case (int)tabs.MODIFICAR: detallesTabHeader = "Modificar"; break;
                    case (int)tabs.DETALLES: detallesTabHeader = "Detalles"; break;
                    default: detallesTabHeader = "Agregar"; usuario = new Usuario(); break;//Si es agregar, creo un nuevo objeto Cliente
                }
                //Cuando se cambia el status, tambien se tiene que cambiar el currentIndex del tab
                //currentIndexTab = _statusTab == 0 ? 0 : 1;
                NotifyPropertyChanged("statusTab");
            }
        }

        //Usado para mover los tabs de acuerdo a las acciones realizadas
        public int currentIndexTab
        {
            get { return _statusTab == 0 ? 0 : 1; }
            set { statusTab = (int)tabs.AGREGAR; NotifyPropertyChanged("currentIndexTab"); }
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
                NotifyPropertyChanged("usuario");
            }
        }
        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        #endregion
    }


    /************************************************************************************************************/
    /// <summary>
    /// Lógica de interacción para MS_AdministrarUsuarios.xaml
    /// </summary>
    public partial class MS_AdministrarUsuarios : Window
    {
        MS_UsuariosViewModel MS_UsuariosViewModel = new MS_UsuariosViewModel();
        /******************************************************/
        public MS_AdministrarUsuarios()
        {
            InitializeComponent();
            Thread thread = new Thread(
              new ThreadStart(
                delegate()
                {
                    usuariosGrid.Dispatcher.Invoke(
                      DispatcherPriority.Normal,
                      new Action(
                        delegate()
                        {
                            usuariosGrid.ItemsSource = MS_UsuarioService.obtenerListaUsuarios();
                        }
                    ));
                }
            ));
            thread.Start();
            usuariosTabControl.DataContext = MS_UsuariosViewModel;
            //usuariosDg.ItemsSource = listUsuarios();
        }
        /******************************************************/
        private IEnumerable<Usuario> ListaUsuarios()
        {
            return MS_UsuarioService.obtenerListaUsuarios();
        }
        /******************************************************/
        public void codigo_Click(object sender, RoutedEventArgs e)
        {
            if (usuariosGrid.SelectedItem != null)
            {
                MS_UsuariosViewModel.usuario = (Usuario)usuariosGrid.SelectedItem;
                MS_UsuariosViewModel.statusTab = (int)MS_UsuariosViewModel.tabs.MODIFICAR;//Modificar
            }
        }
        /******************************************************
        public MS_AdministrarUsuarios(MV_AdministrarVentasWindow MV_AdministrarVentasWindow)
        {
            InitializeComponent();
            Show();
        }
        /******************************************************/
        private void nuevoUsuarioBtn_Click(object sender, RoutedEventArgs e)
        {
            MS_UsuariosViewModel.statusTab = (int)MS_UsuariosViewModel.tabs.AGREGAR;
        }
        /******************************************************/
        private void cancelarListaUsuarioBtn_Click(object sender, RoutedEventArgs e)
        {
            MS_UsuariosViewModel.statusTab = (int)MS_UsuariosViewModel.tabs.BUSQUEDA;
        }
        /******************************************************/
        private void buscarUsuarioBtn_Click(object sender, RoutedEventArgs e)
        {
            Usuario usuario = new Usuario();
            usuario.dni = (MS_UsuariosViewModel.searchCodigo == null) ? "" : MS_UsuariosViewModel.searchCodigo;
            usuario.nombre = (MS_UsuariosViewModel.searchNombreUsuario == null) ? "" : MS_UsuariosViewModel.searchNombreUsuario;
            usuario.id_perfil = MS_UsuariosViewModel.searchPerfil > 0 ? (MS_UsuariosViewModel.searchPerfil == 1 ? (short)MS_UsuariosViewModel.searchPerfil : (short)MS_UsuariosViewModel.searchPerfil) : (short)0;

            /*Empleado empleado = new Empleado();
            empleado.nombre = (MS_UsuariosViewModel.searchNombres == null) ? "" : MS_UsuariosViewModel.searchNombres;
            empleado.apPaterno = (MS_UsuariosViewModel.searchApellidos == null) ? "" : MS_UsuariosViewModel.searchApellidos;
            empleado.apMaterno = (MS_UsuariosViewModel.searchApellidos == null) ? "" : MS_UsuariosViewModel.searchApellidos;
            empleado.tipoDocumento = MS_UsuariosViewModel.searchPerfil > 0 ? (MS_UsuariosViewModel.searchPerfil == 1 ? "DNI" : "RUC") : "";
            usuariosGrid.ItemsSource = UsuarioService.obtenerListaUsuariosBy(usuario, empleado);*/
        }
        /******************************************************/
        private void guardarDetalleUsuarioBtn_Click(object sender, RoutedEventArgs e)
        {
            //Puede ser nuevo o modificar
            if (MS_UsuariosViewModel.usuario.dni != null)
            {
                MS_UsuarioService.actualizarUsuario(MS_UsuariosViewModel.usuario);
            }
            else
            {
                MS_UsuarioService.insertarUsuario(MS_UsuariosViewModel.usuario);
            }
            MS_UsuariosViewModel.statusTab = (int)MS_UsuariosViewModel.tabs.BUSQUEDA;
        }
        /******************************************************/

    }
}
