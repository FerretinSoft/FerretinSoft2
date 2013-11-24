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
using pe.edu.pucp.ferretin.controller.MAlmacen;
using pe.edu.pucp.ferretin.model;
using pe.edu.pucp.ferretin.view.MCompras;
using pe.edu.pucp.ferretin.viewmodel.MCompras;
using pe.edu.pucp.ferretin.viewmodel.MVentas;

namespace pe.edu.pucp.ferretin.view.MVentas
{
    /// <summary>
    /// Lógica de interacción para MV_BuscarProdReporte.xaml
    /// </summary>
    public partial class MV_BuscarProdReporte : Window
    {
         public MV_BuscarProdReporte(Window padre)
        {
            this.Owner = padre;
            InitializeComponent();
            var myDC = DataContext as MV_ReportesViewModel;
            myDC.buscarProductos(null);
            this.ShowDialog();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void seleccionarButton_Click(object sender, RoutedEventArgs e)
        {

            //Código para la ventana de registrar venta
            if (this.Owner is MV_ReportesVentasWindow)
            {
                var vmpadre = this.Owner.DataContext as MV_ReportesViewModel;
                var seleccionados = listaProductos.SelectedItems;
                
                foreach (Producto seleccionado in seleccionados)
                {

                    vmpadre.searchProducto = seleccionado.codigo;
                    vmpadre.nombreProducto = seleccionado.nombre;
                }
                
               
            }

            this.Owner.Focus();
            this.Close();
        }
        
    }
    }

