using Microsoft.Win32;
using pe.edu.pucp.ferretin.controller.MAlmacen;
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
    public partial class MA_MantenimientoProductosWindow : Window
    {
        //MA_MantinimientoProductosViewModel pvm=new MA_MantinimientoProductosViewModel();
    
        public MA_MantenimientoProductosWindow()
        {
            InitializeComponent();
            //this.txtCodigo.IsEnabled = false;
        }

        private void nuevoProductoBtn_Click(object sender, RoutedEventArgs e)
        {
            //Click en icono +
            //this.txtCodigo.IsEnabled = true;
            this.cmbTienda.IsEnabled = false;
            productoTabControl.SelectedIndex = 1;

        }

        private void TextBlock_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //Click directo en agregar producto
            //this.txtCodigo.IsEnabled = true;
            this.cmbTienda.IsEnabled = false;
        }

        private void TabItem_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //Editar producto
            //this.txtCodigo.IsEnabled = false;
            this.cmbTienda.IsEnabled = true;
        }

        private void txtCodigo_KeyDown(object sender, KeyEventArgs e)
        {
            //Validaciones para textbox de solo letras

            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
            {
                e.Handled = false;
                return;
            }

            if (!(e.Key >= Key.A && e.Key <= Key.Z)) 
                e.Handled = true;
        }

        private void txtPrecio_KeyDown(object sender, KeyEventArgs e)
        {
            //Validaciones para textbox tipo precio
            if (e.Key == Key.OemComma || e.Key == Key.OemPeriod)
            {
                Console.WriteLine(txtPrecio.Text.Contains("."));
                if (txtPrecio.Text.Contains(".") || txtPrecio.Text.Contains(",")) e.Handled = true;

            }
            else
            {
                if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
                    e.Handled = false;
                else
                    e.Handled = true;
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {

            if (MessageBox.Show("¿Está seguro que desea cerrar esta ventana?", "Confirmación",
                                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                this.productoTabControl.SelectedIndex = 0;
        }

        private void checkTree(object sender, RoutedEventArgs e)
        {
            
                
        }

        private void btnAddColor_Click(object sender, RoutedEventArgs e)
        {
            //MA_ColoresProductosWindow cpw = new MA_ColoresProductosWindow(MA_ProductoService.obtenerIDProducto(txtCodigo.Text));
            //cpw.Show();
        }

      

        public void guardarBtn(Producto producto)
        {
            //Validaciones

            String ultimoCodigo = MA_ProductoService.obtenerUltimoCodigo();
            int cod = Convert.ToInt32(ultimoCodigo) + 1;
            String newCod=cod.ToString();

            for (int i = 0; i < 10 - (cod.ToString()).Length; i++)
                newCod = "0" + newCod;
            
            producto.codigo = newCod;
            
            
            
            if (MA_ProductoService.agregarNuevoProducto(producto))
            {
                MessageBox.Show("El producto fue agregado con éxito");
                //producto = new Producto();
            }
            else
            {
                MessageBox.Show("No se pudo agregar el producto");
            }
        }


        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if ((cmbUnidadMed.SelectedItem == null) || (cmbMatBase.SelectedItem==null) || (cmbMatSec.SelectedItem==null) || 
                (txtNombreDet.Text=="") || (txtPrecio.Text==""))
            {
                MessageBox.Show("Verifique los datos ingresados");
                return;
            }
            var viewModelThis = this.main.DataContext as MA_MantenimientoProductosViewModel;
            //Edición
            
            if (productoTabControl.SelectedValue.ToString().Contains("Edición de Producto"))
            {
                MA_ProductoService.actualizarProducto(viewModelThis.listaCategorias,viewModelThis.producto);
                MessageBox.Show("El producto fue modificado con éxito");
                productoTabControl.SelectedIndex = 0;
            }
            else //Nuevo producto
            {

                MA_MantenimientoProductosEdicionStockWindow v = new MA_MantenimientoProductosEdicionStockWindow();
                v.Owner = this;
                guardarBtn(viewModelThis.producto);
                viewModelThis.guardarCategoriasProducto();
                var viewModel = v.main.DataContext as MA_MantenimientoProductosEdicionStockViewModel;
                viewModel.producto = viewModelThis.producto;
                productoTabControl.SelectedIndex = 0;
                v.Show();
            }
        }
    }
}
