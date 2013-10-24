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
            MSeguridad.MS_AdministrarUsuarios userAdminW = new MSeguridad.MS_AdministrarUsuarios();
            userAdminW.ShowDialog();
        }

        private void adminPerfilesBtn_Click(object sender, RoutedEventArgs e)
        {
            MSeguridad.MS_AdministrarPerfiles perW = new MSeguridad.MS_AdministrarPerfiles();
            perW.Show();
        }

        private void adminTiendasBtn_Click(object sender, RoutedEventArgs e)
        {
            MSeguridad.MS_AdministrarTiendas tw = new MSeguridad.MS_AdministrarTiendas();
            tw.Show();
        }

        private void parametrosBtn_Click(object sender, RoutedEventArgs e)
        {
            MSeguridad.MS_ParametrosWindow paramW = new MSeguridad.MS_ParametrosWindow();
            paramW.ShowDialog();
        }

        private void auditTransaccionesBtn_Click(object sender, RoutedEventArgs e)
        {
            MSeguridad.MS_AuditoriaWindow tw = new MSeguridad.MS_AuditoriaWindow();
            tw.ShowDialog();
        }
    }
}
