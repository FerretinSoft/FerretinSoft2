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
        public MainWindow()
        {
            InitializeComponent();
        }

        private void comprasBtn_Click(object sender, RoutedEventArgs e)
        {
            //MCompras.MC_MainWindow MCWindow = MCompras.MC_MainWindow.instance;
            //MCWindow.Show();
            //MCWindow.Focus();
        }

        private void confBtn_Click(object sender, RoutedEventArgs e)
        {
            //MSeguridad.MS_MainWindow MSWindow = new MSeguridad.MS_MainWindow();
            //MSWindow.Show();
        }

        private void ventasBtn_Click(object sender, RoutedEventArgs e)
        {
            //MVentas.MV_MainWindow MVWindow = MVentas.MV_MainWindow.instance;
            //MVWindow.Show();
            //MVWindow.Focus();
        }

        private void rrhhBtn_Click(object sender, RoutedEventArgs e)
        {
            //MRecursosHumanos.MR_MainWindow MRWindow = new MRecursosHumanos.MR_MainWindow();
            //MRWindow.Show();
        }

        private void almacenBtn_Click(object sender, RoutedEventArgs e)
        {
            //MAlmacen.MA_MainWindow maMain = new MAlmacen.MA_MainWindow();
            //maMain.Show();
        }

        private void cambiarPasswMenuItem_Click(object sender, RoutedEventArgs e)
        {
            //MSeguridad.MS_CambiarContraseñaUsuario wCambiar = new MSeguridad.MS_CambiarContraseñaUsuario();
            //wCambiar.Show();
        }
    }
}
