using pe.edu.pucp.ferretin.viewmodel.MAlmacen;
using pe.edu.pucp.ferretin.controller.MAlmacen;
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
    /// Lógica de interacción para MA_AtencionSolAbastecimientoWindow.xaml
    /// </summary>
    public partial class MA_AtencionSolAbastecimientoWindow : Window
    {
        public MA_AtencionSolAbastecimientoWindow()
        {
            InitializeComponent();
        }

        private void atenderSolBtn_Click(object sender, RoutedEventArgs e)
        {
            MA_AtencionSolAuxWindow w = new MA_AtencionSolAuxWindow();
            w.Owner = this;
            MA_AtencionSolAbastecimientoViewModel vm = this.main.DataContext as MA_AtencionSolAbastecimientoViewModel;
            MA_AtencionSolAuxViewModel auxVM = w.main.DataContext as MA_AtencionSolAuxViewModel;
            List<MA_SolicitudAbastecimientoService.AtencionSolicitudProducto> listado = new List<MA_SolicitudAbastecimientoService.AtencionSolicitudProducto>();
            foreach (var item in vm.productosPorSolicitud)
            {
                listado.Add(new MA_SolicitudAbastecimientoService.AtencionSolicitudProducto(item));
            }
            auxVM.listadoProductos = listado;
            w.ShowDialog();
        }

        private void consolidarBtn_Click(object sender, RoutedEventArgs e)
        {
            MCompras.MC_ConsolidarSolicitudesWindow consoli = new MCompras.MC_ConsolidarSolicitudesWindow();
            consoli.Show();
        }
    }
}
