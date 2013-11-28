using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using pe.edu.pucp.ferretin.view.MRecursosHumanos;
using pe.edu.pucp.ferretin.viewmodel.MRecursosHumanos;
using pe.edu.pucp.ferretin.viewmodel.MVentas;

namespace pe.edu.pucp.ferretin.view.MVentas
{
    /// <summary>
    /// Lógica de interacción para MV_AdministrarVentasWindow.xaml
    /// </summary>
    public partial class MV_AdministrarVentasWindow : Window
    {
        

        public int selectedCodVenta = 0;

        public MV_AdministrarVentasWindow()
        {
            InitializeComponent();
            rowSelectVentaLista.Height = new GridLength(0);
            rowSelectVentaDetalle.Height = new GridLength(0);
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.Owner != null)//O sea que proviene de un padre
            {
                try
                {
                    MV_DevolucionesWindow padre = this.Owner as MV_DevolucionesWindow;
                    MV_VentasViewModel my_DataContext = this.main.DataContext as MV_VentasViewModel;
                    MV_DevolucionesViewModel padre_DataContext = padre.main.DataContext as MV_DevolucionesViewModel;
                    if (my_DataContext.soloSeleccionarVenta == true)
                    {
                        padre_DataContext.searchnombreCliente = my_DataContext.venta.nombreCompletoCliente;
                        padre_DataContext.searchNroDocumento = my_DataContext.venta.nroDocumento;
                        padre_DataContext.searchNroDocCliente = Convert.ToString(my_DataContext.venta.Cliente.nroDoc);
                        padre_DataContext.searchVendedor = my_DataContext.venta.Usuario.Empleado.dni;
                        padre_DataContext.nombreVendedor = my_DataContext.venta.Usuario.Empleado.nombreCompleto;
                    }
                    else
                    {
                        NotaCredito nota = MV_NotaCreditoService.obtenerNotaCreditoByIdVenta(my_DataContext.venta.id);
                        if (nota == null)
                        {
                            padre_DataContext.devolucion.fecEmision = DateTime.Today;
                            padre_DataContext.devolucion.DevolucionProducto = new System.Data.Linq.EntitySet<DevolucionProducto>();
                            padre_DataContext.devolucion.codigo = MV_DevolucionService.obtenerCodDevolucion();
                            padre_DataContext.loadNroDocumento = my_DataContext.venta.nroDocumento;
                            padre_DataContext.devolucion.Venta = my_DataContext.venta;
                            padre_DataContext.listaProductosComprados = MV_VentaService.obtenerProductosSinPuntosbyIdVenta(my_DataContext.venta.id);
                            padre_DataContext.NotifyPropertyChanged("devolucion");
                        }
                        else
                        {
                            MessageBox.Show("La venta seleccionada ya ha generado una nota de crédito", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }

                    this.Close();
                }
                
                catch { }
                
            }
        }
        private void Button_Click_Vendedor(object sender, RoutedEventArgs e)
        {
            MR_AdministrarPersonalWindow v = new MR_AdministrarPersonalWindow();
            v.Owner = this;
            var viewModel = v.main.DataContext as MR_AdministrarPersonalViewModel;
            viewModel.soloSeleccionarVendedor = true;
            v.ShowDialog();
        }

        private void Button_Click_NotaCredito(object sender, RoutedEventArgs e)
        {
            MV_DocNotaCredito p = new MV_DocNotaCredito();
            MV_VentasViewModel actual = this.main.DataContext as MV_VentasViewModel;
            MV_DocNotaCreditoViewModel padre = p.main.DataContext as MV_DocNotaCreditoViewModel;
            padre.notaCredito = MV_NotaCreditoService.obtenerNotaCreditoByIdVenta(actual.venta.id);
            padre.listaProductos = MV_DevolucionService.obtenerProductosbyIdDevolucion(padre.notaCredito.Devolucion.id);
            padre.NotifyPropertyChanged("listaProductos");
            padre.NotifyPropertyChanged("notaCredito");
            p.ShowDialog();
        }
        

        private void Button_Click_Cliente(object sender, RoutedEventArgs e)
        {
            MV_ClientesWindow v = new MV_ClientesWindow();
            v.Owner = this;
            var viewModel = v.DataContext as MV_ClientesViewModel;
            viewModel.soloSeleccionarCliente = true;
            v.ShowDialog();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            MV_ComunService.Clean();

        }


        private void validarCodVenta(object sender, TextCompositionEventArgs e)
        {
            if (Regex.IsMatch(e.Text, "[0-9]"))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void validarCodVenta_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Space)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void validarCodCliente(object sender, TextCompositionEventArgs e)
        {
            if (Regex.IsMatch(e.Text, "[0-9]"))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void validarCodCliente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Space)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void validarCodVendedor(object sender, TextCompositionEventArgs e)
        {
            if (Regex.IsMatch(e.Text, "[0-9]"))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void validarCodVendedor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Space)
                e.Handled = false;
            else
                e.Handled = true;
        }
    }
}
