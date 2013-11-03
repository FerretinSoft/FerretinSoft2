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
using pe.edu.pucp.ferretin.controller.MVentas;
using pe.edu.pucp.ferretin.model;

namespace pe.edu.pucp.ferretin.view.MVentas
{
    /// <summary>
    /// Lógica de interacción para MV_DevolucionesWindow.xaml
    /// </summary>
    public partial class MV_DevolucionesWindow : Window
    {
        public MV_DevolucionesWindow()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void nuevaDevolucionBtn_Click(object sender, RoutedEventArgs e)
        {
            DevolucionesTab.SelectedIndex = 2;
        }

        private void editarDevolucionBtn_Click(object sender, RoutedEventArgs e)
        {
            DevolucionesTab.SelectedIndex = 1;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DevolucionesTab.SelectedIndex = 0;
        }

        private void DatePicker_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }



        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            Cliente cliente = MV_ClienteService.obtenerClienteByNroDoc(searchNroDoc.Text);
            if (cliente != null)
                nombreCliente.Text = cliente.nombreCompleto;
            else
                nombreCliente.Text = "";
        }

        private void TextBox_SelectionChanged_1(object sender, RoutedEventArgs e)
        {
            Venta venta = MV_VentaService.obtenerVentaByCodVenta(codVenta.Text);
            if (venta != null)
            {
                RegNombreCliente.Text = venta.Cliente.nombreCompleto;
                RegCodCliente.Text = venta.Cliente.nroDoc;
                productosCompGrid.ItemsSource = MV_VentaService.obtenerProductosbyIdVenta(venta.id);
                totalComprado.Text = Convert.ToString(venta.total);

            }
            else
            {
                RegNombreCliente.Text = "";
                RegCodCliente.Text = "";
            }

        }

        private void DatePicker_LostFocus(object sender, RoutedEventArgs e)
        {
            prodDevolverGrid.Items.Refresh();
        }

        private void prodDevolverGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
                VentaProducto row = (VentaProducto)prodDevolverGrid.CurrentCell.Item;
                row.montoParcial = row.Producto.precioLista * row.cantidad;
        }
    }
}
