using pe.edu.pucp.ferretin.controller.MAlmacen;
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
    /// Lógica de interacción para MA_SolicitudAbastecimientoWindow.xaml
    /// </summary>
    public partial class MA_RegistroSolAbastecimientoWindow : Window
    {
        public MA_RegistroSolAbastecimientoWindow()
        {
            InitializeComponent();
            
        }

        private void atenderSolBtn_Click(object sender, RoutedEventArgs e)
        {
            solicitudesTab.SelectedIndex = 3;
        }

        private void buscarSolBtn_Click(object sender, RoutedEventArgs e)
        {
            solicitudesTab.SelectedIndex = 0;
        }

        private void consolidarBtn_Click(object sender, RoutedEventArgs e)
        {
            MCompras.MC_ConsolidarSolicitudesWindow consoli = new MCompras.MC_ConsolidarSolicitudesWindow();
            consoli.Show();
        }

        private void borrarProductosBtn_Click(object sender, RoutedEventArgs e)
        {
            MA_RegistroSolAbastecimientoViewModel vm  = (MA_RegistroSolAbastecimientoViewModel)this.main.DataContext;
                
            for (int i = 0; i < productosGrid.SelectedItems.Count; i++)
			{
                MA_SolicitudAbastecimientoService.ProductoPorSolicitudTienda current =
                        (MA_SolicitudAbastecimientoService.ProductoPorSolicitudTienda)productosGrid.SelectedItems[i];
                vm.solicitud.SolicitudAbastecimientoProducto.Remove(current.productoPorSolicitud);
                vm.productosPorSolicitud.Remove(current);
			}

            vm.NotifyPropertyChanged("productosPorSolicitud");
            vm.NotifyPropertyChanged("current");
        
        }

        
    }
}
