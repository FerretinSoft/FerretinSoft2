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
    public class MS_PerfilesViewModel : INotifyPropertyChanged
    {
        #region Valores para el cuadro de Búsqueda
        public int searchPerfil { get; set; }        
        public int searchModulo { get; set; }
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
                    case (int)tabs.BUSQUEDA: detallesTabHeader = "Agregar"; perfil = new Perfil(); break;//Si es agregar, creo un nuevo objeto Cliente
                    case (int)tabs.AGREGAR: detallesTabHeader = "Agregar"; perfil = new Perfil(); break;//Si es agregar, creo un nuevo objeto Cliente
                    case (int)tabs.MODIFICAR: detallesTabHeader = "Modificar"; break;
                    case (int)tabs.DETALLES: detallesTabHeader = "Detalles"; break;
                    default: detallesTabHeader = "Agregar"; perfil = new Perfil(); break;//Si es agregar, creo un nuevo objeto Cliente
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

        private Perfil _perfil = new Perfil();
        public Perfil perfil
        {
            get
            {
                return _perfil;
            }
            set
            {
                _perfil = value;
                NotifyPropertyChanged("perfil");
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

    /**************************************************************************************************************/
    /// <summary>
    /// Lógica de interacción para MS_AdministrarPerfiles.xaml
    /// </summary>
    public partial class MS_AdministrarPerfiles : Window
    {
        MS_PerfilesViewModel MS_PerfilesViewModel = new MS_PerfilesViewModel();
        /******************************************************/
        public MS_AdministrarPerfiles()
        {
            InitializeComponent();            
            Thread thread = new Thread(
              new ThreadStart(
                delegate()
                {
                    perfilesGrid.Dispatcher.Invoke(
                      DispatcherPriority.Normal,
                      new Action(
                        delegate()
                        {
                            perfilesGrid.ItemsSource = MS_PerfilService.obtenerListaPerfiles();
                        }
                    ));
                }
            ));
            thread.Start();
            perfilesTabControl.DataContext = MS_PerfilesViewModel;
            //usuariosDg.ItemsSource = listUsuarios();
        }
        /******************************************************/
        private IEnumerable<Perfil> ListaPerfiles()
        {
            return MS_PerfilService.obtenerListaPerfiles();
        }
        /******************************************************/
        public void perfil_Click(object sender, RoutedEventArgs e)
        {
            if (perfilesGrid.SelectedItem != null)
            {
                MS_PerfilesViewModel.perfil = (Perfil)perfilesGrid.SelectedItem;
                MS_PerfilesViewModel.statusTab = (int)MS_PerfilesViewModel.tabs.MODIFICAR;//Modificar
            }
        }
        /******************************************************/
        private void nuevoPerfilBtn_Click(object sender, RoutedEventArgs e)
        {
            MS_PerfilesViewModel.statusTab = (int)MS_PerfilesViewModel.tabs.AGREGAR;
        }
        /******************************************************/
        private void cancelarListaPerfilBtn_Click(object sender, RoutedEventArgs e)
        {
            MS_PerfilesViewModel.statusTab = (int)MS_PerfilesViewModel.tabs.BUSQUEDA;
        }
        
        /******************************************************/
        private void buscarPerfilBtn_Click(object sender, RoutedEventArgs e)
        {
            /*
            Perfil perfil = new Perfil();
            perfil.id = (MS_PerfilesViewModel.searchCodigo == null) ? "" : MS_PerfilesViewModel.searchCodigo;
            perfil.nombre = (MS_PerfilesViewModel.searchNombreUsuario == null) ? "" : MS_PerfilesViewModel.searchNombreUsuario;
            perfil.id_perfil = MS_PerfilesViewModel.searchPerfil > 0 ? (MS_PerfilesViewModel.searchPerfil == 1 ? (short)MS_PerfilesViewModel.searchPerfil : (short)MS_PerfilesViewModel.searchPerfil) : (short)0;
             usuariosGrid.ItemsSource = UsuarioService.obtenerListaUsuariosBy(usuario, empleado);
            */

            /*Empleado empleado = new Empleado();
            empleado.nombre = (MS_UsuariosViewModel.searchNombres == null) ? "" : MS_UsuariosViewModel.searchNombres;
            empleado.apPaterno = (MS_UsuariosViewModel.searchApellidos == null) ? "" : MS_UsuariosViewModel.searchApellidos;
            empleado.apMaterno = (MS_UsuariosViewModel.searchApellidos == null) ? "" : MS_UsuariosViewModel.searchApellidos;
            empleado.tipoDocumento = MS_UsuariosViewModel.searchPerfil > 0 ? (MS_UsuariosViewModel.searchPerfil == 1 ? "DNI" : "RUC") : "";
            usuariosGrid.ItemsSource = UsuarioService.obtenerListaUsuariosBy(usuario, empleado);*/
        }
        /******************************************************/
        private void guardarDetallePerfilBtn_Click(object sender, RoutedEventArgs e)
        {
            //Puede ser nuevo o modificar
            if (MS_PerfilesViewModel.perfil.id > 0)
            {
                MS_PerfilService.actualizarPerfil(MS_PerfilesViewModel.perfil);
            }
            else
            {
                MS_PerfilService.insertarPerfil(MS_PerfilesViewModel.perfil);
            }
            MS_PerfilesViewModel.statusTab = (int)MS_PerfilesViewModel.tabs.BUSQUEDA;
        }
        /******************************************************/

    }
}
