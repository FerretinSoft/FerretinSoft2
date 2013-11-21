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
using System.Windows.Shapes;

namespace pe.edu.pucp.ferretin.view.MSeguridad
{
    /// <summary>
    /// Lógica de interacción para MS_MainWindow.xaml
    /// </summary>
    public partial class MS_MainWindow : Window
    {
        public MS_MainWindow()
        {
            InitializeComponent();
        }

        private void homeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.OwnedWindows.Count == 0)
            {
                this.Close();
            }
        }

        private void adminUsuariosBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ComunService.usuarioLpermisos[28].estado == true)
            {
                MSeguridad.MS_AdministrarUsuarios userAdminW = new MSeguridad.MS_AdministrarUsuarios();
                userAdminW.ShowDialog();
            }
            else 
            {
                MessageBox.Show("Usted no cuenta con el permiso necesario.");
            }
        }

        private void adminPerfilesBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ComunService.usuarioLpermisos[29].estado == true)
            {
                MSeguridad.MS_AdministrarPerfiles perW = new MSeguridad.MS_AdministrarPerfiles();
                perW.ShowDialog();
            }
            else
            {
                MessageBox.Show("Usted no cuenta con el permiso necesario.");
            }
        }

        private void adminTiendasBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ComunService.usuarioLpermisos[30].estado == true)
            {
                MSeguridad.MS_AdministrarTiendas tw = new MSeguridad.MS_AdministrarTiendas();
                tw.ShowDialog();
            }
            else
            {
                MessageBox.Show("Usted no cuenta con el permiso necesario.");
            }
        }

        private void parametrosBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ComunService.usuarioLpermisos[31].estado == true)
            {
                MSeguridad.MS_ParametrosWindow paramW = new MSeguridad.MS_ParametrosWindow();
                paramW.ShowDialog();
            }
            else
            {
                MessageBox.Show("Usted no cuenta con el permiso necesario.");
            }
        }

        private void auditTransaccionesBtn_Click(object sender, RoutedEventArgs e)
        {

            
            if (ComunService.usuarioLpermisos[32].estado == true)
            {
                MSeguridad.MS_AuditoriaWindow tw = new MSeguridad.MS_AuditoriaWindow();
                tw.ShowDialog();
            }
            else
            {
                MessageBox.Show("Usted no cuenta con el permiso necesario.");
            }
              
            
        }
    }
}
