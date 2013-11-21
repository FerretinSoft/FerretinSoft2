using System;
using System.Windows;

namespace pe.edu.pucp.ferretin.view.MCompras
{
    /// <summary>
    /// Interaction logic for MC_ReportesWindow.xaml
    /// </summary>
    public partial class MC_ReportesWindow : Window
    {
        int estado = 0; //variable que representa el estado en que se encuentra la elaboración del reporte:
        // estado=0 > intro; estado=1 > configuración; estado=2 > fin

        int reporte = 0;
        /*
         * 1 - Reporte de estado de OC
         * 2 - Reporte de Productos Comprados
         * */

        public MC_ReportesWindow()
        {
            InitializeComponent();
            dpFechaDesde.SelectedDate = null;
            dpFechaHasta.SelectedDate = null;
        }

        private void btnGenerarReporte_Click(object sender, RoutedEventArgs e)
        {
            if (estado == 2)
            {
                MC_VisorReporteOC vroc = new MC_VisorReporteOC(txtRuc.Text, (DateTime)dpFechaDesde.SelectedDate,
                                                                (DateTime)dpFechaHasta.SelectedDate);
                vroc.Show();
            }
        }

        private void repSigBtn_Click(object sender, RoutedEventArgs e)
        {
            switch (estado)
            {
                case 0:
                    if (reporte > 0)
                    {
                        repIntroGrid.Visibility = System.Windows.Visibility.Collapsed;
                        //setConfValues();
                        repConfGrid.Visibility = System.Windows.Visibility.Visible;
                        estado = 1;
                        repAntBtn.IsEnabled = true;
                    }
                    else
                        MessageBox.Show("Debe seleccionar algún reporte");
                    break;
                case 1:
                    if (dpFechaDesde.SelectedDate == null || dpFechaHasta.SelectedDate == null || txtRuc.Text=="")
                        MessageBox.Show("Verifique sus datos de entrada");
                    else
                    {
                        repConfGrid.Visibility = System.Windows.Visibility.Collapsed;
                        repFinalGrid.Visibility = System.Windows.Visibility.Visible;
                        estado = 2;
                        repSigBtn.IsEnabled = false;
                    }
                    break;
            }
        }

        private void reportesBaseListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            reportesFavoritosListBox.SelectedIndex = -1;
            switch (reportesBaseListBox.SelectedIndex)
            {
                case 0://Reporte de estado de OC
                    reporte = 1;
                    break;
                case 1://Reporte de Productos Comprados
                    reporte = 2;
                    //MessageBox.Show("Kardex");
                    break;
                default:
                    reporte = 0;
                    break;
            }
        }

        private void reportesFavoritosListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            reportesBaseListBox.SelectedIndex = -1;
            switch (reportesFavoritosListBox.SelectedIndex)
            {
                //case 0: // kardex diario
                //    reporte = 3;
                //    //MessageBox.Show("kardex diario");
                //    break;
                //case 1: // kardex mensual
                //    reporte = 4;
                //    //MessageBox.Show("Kardex mensual");
                //    break;
                //default:
                //    reporte = 0;
                //    break;
            }
        }

        private void repAntBtn_Click(object sender, RoutedEventArgs e)
        {
            switch (estado)
            {
                case 1:
                    repConfGrid.Visibility = System.Windows.Visibility.Collapsed;
                    repIntroGrid.Visibility = System.Windows.Visibility.Visible;
                    estado = 0;
                    repAntBtn.IsEnabled = false;
                    break;
                case 2:
                    repFinalGrid.Visibility = System.Windows.Visibility.Collapsed;
                    repConfGrid.Visibility = System.Windows.Visibility.Visible;
                    estado = 1;
                    repSigBtn.IsEnabled = true;
                    break;
            }
        }

        private void txtRuc_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            int ascci = Convert.ToInt32(Convert.ToChar(e.Text));
            if (ascci >= 48 && ascci <= 57) e.Handled = false;
            else e.Handled = true;
        }

        private void txtRuc_PreviewTextInput_1(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            int ascci = Convert.ToInt32(Convert.ToChar(e.Text));
            if (ascci >= 48 && ascci <= 57) e.Handled = false;
            else e.Handled = true;
        }
    }
}
