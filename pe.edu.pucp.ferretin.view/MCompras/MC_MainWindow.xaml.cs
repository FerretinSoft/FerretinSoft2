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
            if (ComunService.usuarioLpermisos[15].estado == true)
            {
                MC_AdministrarProveedorWindow prove = new MC_AdministrarProveedorWindow();
                prove.Show();
            }
            else
            {
                MessageBox.Show("Usted no cuenta con el permiso necesario.");
            }
        }

        private void solicitudAbastBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ComunService.usuarioLpermisos[16].estado == true)
            {
                MAlmacen.MA_RegistroSolAbastecimientoWindow solabas = new MAlmacen.MA_RegistroSolAbastecimientoWindow();
                solabas.consolidarBtn.Visibility = System.Windows.Visibility.Visible;
                //solabas.atenderSolTab.Visibility = System.Windows.Visibility.Hidden;
                solabas.generarSolTab.Visibility = System.Windows.Visibility.Hidden;
                solabas.ShowDialog();
            }
            else
            {
                MessageBox.Show("Usted no cuenta con el permiso necesario.");
            }
        }

        private void ordCompraBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ComunService.usuarioLpermisos[17].estado == true)
            {
                MC_AdministrarOCCotizacionWindow orden = new MC_AdministrarOCCotizacionWindow();
                orden.Show();
            }
            else
            {
                MessageBox.Show("Usted no cuenta con el permiso necesario.");
            }
        }

        private void gRemisioinBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ComunService.usuarioLpermisos[18].estado == true)
            {
                MC_AdministrarGuiaRemiWindow guia = new MC_AdministrarGuiaRemiWindow();
                guia.Show();
            }
            else
            {
                MessageBox.Show("Usted no cuenta con el permiso necesario.");
            }
        }

        private void repComprasBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ComunService.usuarioLpermisos[19].estado == true)
            {
                MC_ReportesWindow reporte = new MC_ReportesWindow();
                reporte.Show();
            }
            else
            {
                MessageBox.Show("Usted no cuenta con el permiso necesario.");
            }
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
