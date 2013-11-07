using pe.edu.pucp.ferretin.controller.MSeguridad;
using pe.edu.pucp.ferretin.controller.MVentas;
using pe.edu.pucp.ferretin.model;
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
    /// Lógica de interacción para MV_AdministrarProformasWindow.xaml
    /// </summary>
    public partial class MV_AdministrarProformasWindow : Window
    {
        public MV_AdministrarProformasWindow()
        {
            InitializeComponent();
        }

        private void detalleProformaBtn_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void buscarClienteBtn_Click(object sender, RoutedEventArgs e)
        {
            //ClientesWindow pw = new ClientesWindow();
            //pw.Show();
        }

        private void buscarVendedorBtn_Click(object sender, RoutedEventArgs e)
        {
            //PersonalAdminWindow pw = new PersonalAdminWindow();
            //pw.Show();
        }

        private void nuevaProformaBtn_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void registrarProformaBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cancelarBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void buscarProformaBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //Buscar cliente

            MV_ClientesWindow v = new MV_ClientesWindow();
            v.Owner = this;
            var viewModel = v.main.DataContext as MV_ClientesViewModel;
            viewModel.soloSeleccionarCliente = true;
            v.Show();
        }

        private void registrarBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.Owner!=null)//Si tiene un padre
            {
                try
                {
                    MV_RegistrarVentaWindow padre = this.Owner as MV_RegistrarVentaWindow;
                    MV_RegistrarVentaViewModel padreViewModel = padre.main.DataContext as MV_RegistrarVentaViewModel;

                    MV_AdministrarProformasViewModel miVM = this.DataContext as MV_AdministrarProformasViewModel;

                    if (miVM.proforma != null)
                    {
                        foreach (var vp in miVM.proforma.ProformaProducto)
                        {
                            VentaProducto ventaProducto = new VentaProducto();
                            ventaProducto.canjeado = false;
                            ventaProducto.tipoCambio = (decimal)(MS_SharedService.obtenerTipodeCambio());
                            ventaProducto.montoParcial = vp.montoParcial;
                            ventaProducto.Venta = padreViewModel.venta;
                            ventaProducto.Producto = vp.Producto;
                            ventaProducto.cantidad = vp.cantidad;
                            ventaProducto.PromocionActual = MV_PromocionService.ultimaPromocionPorProducto(vp.Producto);
                            ventaProducto.PropertyChanged += padreViewModel.actualizarMontosVenta;

                            padreViewModel.venta.VentaProducto.Add(ventaProducto);
                            padreViewModel.NotifyPropertyChanged("venta");
                        }
                        
                       
                    }
                    this.Close();
                }
                catch(Exception ex) {
                    //MessageBox.Show(ex.Message);
                }

                
            }
        }
    }
}
