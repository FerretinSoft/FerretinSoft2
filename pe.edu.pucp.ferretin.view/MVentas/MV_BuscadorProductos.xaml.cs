using pe.edu.pucp.ferretin.model;
using pe.edu.pucp.ferretin.view.MCompras;
using pe.edu.pucp.ferretin.viewmodel.MCompras;
using pe.edu.pucp.ferretin.viewmodel.MVentas;
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

namespace pe.edu.pucp.ferretin.view.MVentas
{
    /// <summary>
    /// Lógica de interacción para MV_BuscadorProducto.xaml
    /// </summary>
    public partial class MV_BuscadorProductos : Window
    {
        
        public MV_BuscadorProductos(Window padre, Tienda tienda)
        {
            this.Owner = padre;
            InitializeComponent();
            var myDC = DataContext as MV_BuscadorProductosViewModel;
            myDC.tienda = tienda;
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
            if( this.Owner is MV_RegistrarVentaWindow){
                var vmpadre = this.Owner.DataContext as MV_RegistrarVentaViewModel;
                var seleccionados = listaProductos.SelectedItems;
                foreach(ProductoAlmacen seleccionado in seleccionados){
                    vmpadre.codProdAgregar = seleccionado.Producto.codigo;
                    vmpadre.agregarProducto(seleccionado);
                }
                vmpadre.codProdAgregar = "";
            }
            //Código para la ventana de proformas
            else if (this.Owner is MV_AdministrarProformasWindow)
            {
                var vmpadre = this.Owner.DataContext as MV_AdministrarProformasViewModel;
                var seleccionados = listaProductos.SelectedItems;
                foreach (ProductoAlmacen seleccionado in seleccionados)
                {
                    vmpadre.codProdAgregar = seleccionado.Producto.codigo;
                    vmpadre.agregarProducto(seleccionado);
                }
                vmpadre.codProdAgregar = "";
            }
            //Código para la ventana de promociones
            else if (this.Owner is MV_AdministrarPromocionesWindow)
            {
                var vmpadre = this.Owner.DataContext as MV_PromocionesViewModel;
                var seleccionados = listaProductos.SelectedItems;
                foreach (ProductoAlmacen seleccionado in seleccionados)
                {
                    vmpadre.codProdAgregar = seleccionado.Producto.codigo;
                    vmpadre.agregarProducto(seleccionado);
                }
                vmpadre.codProdAgregar = "";
            }


            //Poner aqui tu codigo para tu ventana 
            else if (this.Owner is MC_AdministrarProveedorWindow)
            {
                var vmpadre = this.Owner.DataContext as MC_ProveedoresViewModel;
                var seleccionados = listaProductos.SelectedItems;
                foreach (ProductoAlmacen seleccionado in seleccionados)
                {
                    vmpadre.codProdAgregar = seleccionado.Producto.codigo;
                    vmpadre.agregarProducto(null);
                }
                vmpadre.codProdAgregar = "";

            }

            this.Owner.Focus();
            this.Close();
        }
        
    }
}
