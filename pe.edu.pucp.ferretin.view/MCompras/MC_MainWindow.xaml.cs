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

namespace pe.edu.pucp.ferretin.view.MCompras
{
    /// <summary>
    /// Interaction logic for MC_MainWindow.xaml
    /// </summary>
    public partial class MC_MainWindow : Window
    {
        public MC_MainWindow()
        {
            InitializeComponent();
        }

        private void adminProveedoresBtn_Click(object sender, RoutedEventArgs e)
        {
            MC_AdministrarProveedorWindow prove = new MC_AdministrarProveedorWindow();
            prove.Show();
        }

        private void solicitudAbastBtn_Click(object sender, RoutedEventArgs e)
        {
            MAlmacen.MA_RegistroSolAbastecimientoWindow solabas = new MAlmacen.MA_RegistroSolAbastecimientoWindow();
            solabas.consolidarBtn.Visibility = System.Windows.Visibility.Visible;
            //solabas.atenderSolTab.Visibility = System.Windows.Visibility.Hidden;
            solabas.generarSolTab.Visibility = System.Windows.Visibility.Hidden;
            solabas.ShowDialog();
        }

        private void ordCompraBtn_Click(object sender, RoutedEventArgs e)
        {
            MC_AdministrarOCCotizacionWindow orden = new MC_AdministrarOCCotizacionWindow();
            orden.Show();
        }

        private void gRemisioinBtn_Click(object sender, RoutedEventArgs e)
        {
            MC_AdministrarGuiaRemiWindow guia = new MC_AdministrarGuiaRemiWindow();
            guia.Show();
        }

        private void repComprasBtn_Click(object sender, RoutedEventArgs e)
        {
            MC_ReportesWindow reporte = new MC_ReportesWindow();
            reporte.Show();
        }

        private void homeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.OwnedWindows.Count == 0)
            {
                this.Close();
            }
        }
    }
}
