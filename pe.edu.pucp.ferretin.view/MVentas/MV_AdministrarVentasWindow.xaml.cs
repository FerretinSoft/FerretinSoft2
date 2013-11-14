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
                        padre_DataContext.searchNroDocCliente = my_DataContext.venta.Cliente.nroDoc;
                        padre_DataContext.searchVendedor = my_DataContext.venta.Usuario.Empleado.dni;
                        padre_DataContext.nombreVendedor = my_DataContext.venta.Usuario.Empleado.nombreCompleto;
                    }
                    else
                    {
                        padre_DataContext.devolucion.fecEmision = DateTime.Today;
                        padre_DataContext.devolucion.codigo = MV_DevolucionService.obtenerCodDevolucion();
                        padre_DataContext.loadNroDocumento = my_DataContext.venta.nroDocumento;
                        padre_DataContext.devolucion.Venta = my_DataContext.venta;
                        padre_DataContext.listaProductosComprados = MV_VentaService.obtenerProductosbyIdVenta(my_DataContext.venta.id);
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
            v.Show();
        }

        private void Button_Click_Cliente(object sender, RoutedEventArgs e)
        {
            MV_ClientesWindow v = new MV_ClientesWindow();
            v.Owner = this;
            var viewModel = v.DataContext as MV_ClientesViewModel;
            viewModel.soloSeleccionarCliente = true;
            v.Show();
        }
    }
}
