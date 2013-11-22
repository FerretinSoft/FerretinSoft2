using pe.edu.pucp.ferretin.controller;
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
    /// Lógica de interacción para MA_AtencionSolAuxWindow.xaml
    /// </summary>
    public partial class MA_AtencionSolAuxWindow : Window
    {
        public MA_AtencionSolAuxWindow()
        {
            InitializeComponent();
        }

        private void aceptarBtn_Click(object sender, RoutedEventArgs e)
        {
            MA_AtencionSolAuxViewModel thisVM = this.main.DataContext as MA_AtencionSolAuxViewModel;
            List<MA_SolicitudAbastecimientoService.AtencionSolicitudProducto> lista = thisVM.listadoProductos;
            foreach (var item in lista)
            {
                if (item.cantidad > item.producto.cantidadRestante)
                {
                    MessageBox.Show("La cantidad especificada para el producto "
                        + item.producto.Producto.nombre
                        + " es mayor que la cantidad que debe abastecerse ("
                        + item.producto.cantidadRestante + ").");
                    return;
                }                
            }

            if (MessageBox.Show("Esta seguro que desea atender la solicitud?", "Confirmación de Atención", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                MA_AtencionSolAbastecimientoWindow parent = this.Owner as MA_AtencionSolAbastecimientoWindow;
                MA_AtencionSolAbastecimientoViewModel parentVM = parent.main.DataContext as MA_AtencionSolAbastecimientoViewModel;
                parentVM.listaAtencion = thisVM.listadoProductos;
                this.Close();
            }
            else
            {
                
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ComunService.Clean();
        }

        private void DataGrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            DataGrid grid = (DataGrid)sender;
            if (grid.CurrentCell.Column.DisplayIndex == 3)
            {
                //Validaciones para que acepte solo numeros
                if (((e.Key >= Key.D0 && e.Key <= Key.D9) || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) || e.Key == Key.Back || e.Key == Key.Tab))
                    e.Handled = false;
                else
                    e.Handled = true;
            }
        }
    }
}
