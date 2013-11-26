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

namespace pe.edu.pucp.ferretin.view
{
    /// <summary>
    /// Interaction logic for ReportesWindow.xaml
    /// </summary>
    public partial class ReportesWindow : Window
    {
        public ReportesWindow()
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

        private void RepAlmBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ComunService.usuarioLpermisos[26].estado == true)
            {
                MAlmacen.MA_ReportesWindow RAW = new MAlmacen.MA_ReportesWindow();
                RAW.ShowDialog();
            }
            else
            {
                MessageBox.Show("Usted no cuenta con el permiso necesario.");
            }

            
        }

        private void RepVenBtn_Click(object sender, RoutedEventArgs e)
        {

            if (ComunService.usuarioLpermisos[13].estado == true)
            {
                MVentas.MV_ReportesVentasWindow RVW = new MVentas.MV_ReportesVentasWindow();
                RVW.ShowDialog();
            }
            else
            {
                MessageBox.Show("Usted no cuenta con el permiso necesario.");
            }

        }

        private void RepComBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ComunService.usuarioLpermisos[19].estado == true)
            {
                MCompras.MC_ReportesWindow RCW = new MCompras.MC_ReportesWindow();
                RCW.ShowDialog();
            }
            else
            {
                MessageBox.Show("Usted no cuenta con el permiso necesario.");
            }

        }

        private void RepSegBtn_Click(object sender, RoutedEventArgs e)
        {

            if (ComunService.usuarioLpermisos[32].estado == true)
            {
                MSeguridad.MS_ReportesWindow RSW = new MSeguridad.MS_ReportesWindow();
                RSW.ShowDialog();
            }
            else
            {
                MessageBox.Show("Usted no cuenta con el permiso necesario.");
            }
        }

    }
}
