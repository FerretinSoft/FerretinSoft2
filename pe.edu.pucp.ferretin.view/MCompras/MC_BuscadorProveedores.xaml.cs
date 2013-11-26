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
using pe.edu.pucp.ferretin.model;
using pe.edu.pucp.ferretin.viewmodel.MCompras;

namespace pe.edu.pucp.ferretin.view.MCompras
{
    /// <summary>
    /// Interaction logic for MC_BuscadorProveedores.xaml
    /// </summary>
    public partial class MC_BuscadorProveedores : Window
    {
        public MC_BuscadorProveedores(Window padre)
        {
            this.Owner = padre;
            InitializeComponent();
            var myDC = DataContext as MC_BuscadorProveedoresViewModel;
            myDC.buscarProveedores(null);
            this.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.Owner is MC_AdministrarOCCotizacionWindow)
            {
                var vmpadre = this.Owner.DataContext as MC_CotizacionesOCViewModel;
                var seleccionados = listaProveedores.SelectedItems;
                foreach (Proveedor seleccionado in seleccionados)
                {
                    vmpadre.documentoCompra.Proveedor = seleccionado;
                    break;
                }
                vmpadre.proveedorNombre=vmpadre.documentoCompra.Proveedor.razonSoc;
            }

            this.Owner.Focus();
            this.Close();
        }
    }
}
