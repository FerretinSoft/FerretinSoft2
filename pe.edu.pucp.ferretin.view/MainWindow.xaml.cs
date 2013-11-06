using pe.edu.pucp.ferretin.model;
using pe.edu.pucp.ferretin.controller;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace pe.edu.pucp.ferretin.view
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Variables
        Usuario usuarioLog;
        #endregion 

        #region Constructor
        public MainWindow(Usuario usuario)
        {
            InitializeComponent();
            usuarioLog = usuario;
            ComunService.usuarioLo(usuario);
            ComunService.obtenerPermisos(usuario);
            usuarioMenu.Header = "Tienda ABC, " + ComunService.usuarioL.nombre;

        }
        #endregion

        #region Boton Compras
        private void comprasBtn_Click(object sender, RoutedEventArgs e)
        {
            
                MCompras.MC_MainWindow Mainw = new MCompras.MC_MainWindow();
                Mainw.Show();
            
                
        }
        #endregion

        #region Boton Seguridad
        private void confBtn_Click(object sender, RoutedEventArgs e)
        {

            
                MSeguridad.MS_MainWindow MSWindow = new MSeguridad.MS_MainWindow();
                MSWindow.Show();
            
                
        }
        #endregion

        #region Boton Ventas
        private void ventasBtn_Click(object sender, RoutedEventArgs e)
        {

           
            MVentas.MV_MainWindow MVWindow = MVentas.MV_MainWindow.instance;
            MVWindow.Show();
            MVWindow.Focus();
           
        }
        #endregion

        #region Boton RRHH
        private void rrhhBtn_Click(object sender, RoutedEventArgs e)
        {
            
                MRecursosHumanos.MR_MainWindow MRWindow = new MRecursosHumanos.MR_MainWindow();
                MRWindow.Show();
            
            
              
            
        }
        #endregion

        #region Boton Almacen
        private void almacenBtn_Click(object sender, RoutedEventArgs e)
        {

            
                MAlmacen.MA_MainWindow maMain = new MAlmacen.MA_MainWindow();
                maMain.Show();
            
        }
        #endregion

        #region Botones Cambiar Contraseña y Cerrar Sesion
        private void cambiarPasswMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MSeguridad.MS_CambiarContraseñaUsuario wCambiar = new MSeguridad.MS_CambiarContraseñaUsuario(usuarioLog);
            wCambiar.Show();
        }

        private void cerrarSesionMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MSeguridad.MS_LoginWindow lw = new MSeguridad.MS_LoginWindow();
            lw.Show();
            this.Close();
        }
        #endregion
    }
}
