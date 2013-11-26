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
    /// Lógica de interacción para MA_BuscadorProductosWindow.xaml
    /// </summary>
    public partial class MA_BuscadorProductosWindow : Window
    {
        public MA_BuscadorProductosWindow(Window padre)
        {
            this.Owner = padre;
            InitializeComponent();
            var myDC = DataContext as MA_BuscadorProductosViewModel;
            myDC.buscarProductos(null);
            this.ShowDialog();
            InitializeComponent();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void seleccionarButton_Click(object sender, RoutedEventArgs e)
        {
            //Código para la ventana de registrar movimiento
            if (this.Owner is MA_MovimientosWindow)
            {
                MA_MovimientosWindow window = this.Owner as MA_MovimientosWindow;
                var vmpadre = window.main.DataContext as MA_MovimientosViewModel;
                var seleccionados = listaProductos.SelectedItems;
                foreach (Producto item in seleccionados)
                {
                    vmpadre.codigoNuevoProducto = item.codigo;
                    vmpadre.agregarNuevoProducto(null);
                }
                vmpadre.codigoNuevoProducto = "";
            }

            this.Owner.Focus();
            this.Close();
        }
    }
}
