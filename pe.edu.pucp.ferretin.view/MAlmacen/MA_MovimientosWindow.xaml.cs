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
using System.ComponentModel;
using pe.edu.pucp.ferretin.model;
using pe.edu.pucp.ferretin.controller;
using System.Threading;
using System.Windows.Threading;
using pe.edu.pucp.ferretin.controller.MSeguridad;
using pe.edu.pucp.ferretin.controller.MAlmacen;
using pe.edu.pucp.ferretin.viewmodel;
using pe.edu.pucp.ferretin.viewmodel.MAlmacen;
using pe.edu.pucp.ferretin.view.MVentas;

namespace pe.edu.pucp.ferretin.view.MAlmacen
{
    /// <summary>
    /// Lógica de interacción para MA_MovimientosWindow.xaml
    /// </summary>
    public partial class MA_MovimientosWindow : Window
    {
        
        public MA_MovimientosWindow()
        {
            InitializeComponent();            
        }

        private void DataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            if ((string)e.Column.Header != "Estado")
            {
                // If this is the new row, entity is not yet attached to the context.
                if (((MovimientoTipo)e.Row.Item).id >0)
                {
                    e.Cancel = true;
                }
            }
        }

        private void visualizarDocumentoBtn_Click(object sender, RoutedEventArgs e)
        {
            MA_DocumentoMovimiento w = new MA_DocumentoMovimiento();
            MA_MovimientosViewModel movVM = this.main.DataContext as MA_MovimientosViewModel;
            MA_DocumentoMovimientoViewModel docVM = w.main.DataContext as MA_DocumentoMovimientoViewModel;
            docVM.movimiento = movVM.movimiento;
            w.Show();
        }

        

        private void productosGrid_KeyDown(object sender, KeyEventArgs e)
        {
            /*MA_MovimientosViewModel vm = (MA_MovimientosViewModel)this.DataContext;
            var grid = (DataGrid)sender;
            if (e.Key == Key.Delete)
            {
                foreach (var row in grid.SelectedItems)
                {
                    MA_MovimientosService.MovimientoProductoTienda current = row as MA_MovimientosService.MovimientoProductoTienda;
                    vm.movimiento.MovimientoProducto.Remove(current.movimientoProducto);
                    vm.NotifyPropertyChanged("movimiento");

                }
            }*/
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            //productosGrid_KeyDown(productosGrid, e);
        }

        private void borrarProductosBtn_Click(object sender, RoutedEventArgs e)
        {
            MA_MovimientosViewModel vm = (MA_MovimientosViewModel)this.main.DataContext;
                
            for (int i = 0; i < productosGrid.SelectedItems.Count; i++)
			{
                MA_MovimientosService.MovimientoProductoTienda current = 
                        (MA_MovimientosService.MovimientoProductoTienda)productosGrid.SelectedItems[i];
                vm.movimiento.MovimientoProducto.Remove(current.movimientoProducto);
                vm.productosPorMovimiento.Remove(current);
                
			 
			}

            vm.NotifyPropertyChanged("productosPorMovimiento");
            vm.NotifyPropertyChanged("movimiento");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //var vm = DataContext as MA_MovimientosViewModel;
            //var buscador = new MV_BuscadorProductos(this, vm.usuarioLogueado.Empleado.tiendaActual);
        }

        private void productosGrid_PreviewKeyDown(object sender, KeyEventArgs e)
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

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            ComunService.Clean();
        }        


        
    }
}
