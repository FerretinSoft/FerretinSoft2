using pe.edu.pucp.ferretin.model;
using pe.edu.pucp.ferretin.viewmodel.MAlmacen;
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

namespace pe.edu.pucp.ferretin.view.MAlmacen
{
    /// <summary>
    /// Lógica de interacción para MA_ReportesWindow.xaml
    /// </summary>
    public partial class MA_ReportesWindow : Window
    {
        int estado = 0; //variable que representa el estado en que se encuentra la elaboración del reporte:
        // estado=0 > intro; estado=1 > configuración; estado=2 > fin

        public MA_ReportesWindow()
        {
            InitializeComponent();
        }

        private void repSigBtn_Click(object sender, RoutedEventArgs e)
        {
            switch (estado)
            {
                case 0:
                    repIntroGrid.Visibility = System.Windows.Visibility.Collapsed;
                    repConfGrid.Visibility = System.Windows.Visibility.Visible;
                    estado = 1;
                    repAntBtn.IsEnabled = true;
                    break;
                case 1:
                    repConfGrid.Visibility = System.Windows.Visibility.Collapsed;
                    repFinalGrid.Visibility = System.Windows.Visibility.Visible;
                    estado = 2;
                    repSigBtn.IsEnabled = false;
                    break;


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
                    repConfGrid.Visibility = System.Windows.Visibility.Visible;
                    repFinalGrid.Visibility = System.Windows.Visibility.Collapsed;
                    estado = 1;
                    repSigBtn.IsEnabled = true;
                    break;


            }
        }

        private void btnViewReport_Click(object sender, RoutedEventArgs e)
        {
            MA_ReportesStockPorTiendaWindow v = new MA_ReportesStockPorTiendaWindow();
            v.Owner = this;
            var viewModel = v.main.DataContext as MA_ReportesStockPorTiendaViewModel;
            Tienda tiendaSelected = (Tienda)this.cmbTienda.SelectedItem;
            
            viewModel.tiendaSeleccionada = tiendaSelected;
            v.Show();
        }
    }
}
