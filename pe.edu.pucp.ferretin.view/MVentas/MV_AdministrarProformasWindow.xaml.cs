using pe.edu.pucp.ferretin.controller;
using pe.edu.pucp.ferretin.controller.MSeguridad;
using pe.edu.pucp.ferretin.controller.MVentas;
using pe.edu.pucp.ferretin.model;
using pe.edu.pucp.ferretin.view.MRecursosHumanos;
using pe.edu.pucp.ferretin.viewmodel.MRecursosHumanos;
using pe.edu.pucp.ferretin.viewmodel.MVentas;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
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
using System.Windows.Xps;
using System.Windows.Xps.Packaging;

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
            var viewModel = v.DataContext as MV_ClientesViewModel;
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
                    MV_RegistrarVentaViewModel padreViewModel = padre.DataContext as MV_RegistrarVentaViewModel;

                    MV_AdministrarProformasViewModel miVM = this.DataContext as MV_AdministrarProformasViewModel;

                    if (miVM.proforma != null)
                    {
                        padreViewModel.venta.VentaProducto.Clear();
                        foreach (var vp in miVM.proforma.ProformaProducto)
                        {
                            VentaProducto ventaProducto = new VentaProducto();
                            ventaProducto.PromocionActual = MV_PromocionService.ultimaPromocionPorProducto(vp.Producto, MS_SharedService.usuarioL.Empleado.tiendaActual);
                            ventaProducto.canjeado = false;
                            ventaProducto.tipoCambio = (decimal)(MS_SharedService.obtenerTipodeCambio());
                            ventaProducto.montoParcial = vp.montoParcial;
                            ventaProducto.Venta = padreViewModel.venta;
                            ventaProducto.Producto = vp.Producto;
                            ventaProducto.cantidad = vp.cantidad;
                            ventaProducto.PropertyChanged += padreViewModel.actualizarMontosVenta;

                            padreViewModel.venta.Cliente = vp.Proforma.Cliente;

                            padreViewModel.venta.VentaProducto.Add(ventaProducto);
                            padreViewModel.NotifyPropertyChanged("venta");
                            padreViewModel.actualizarMontosVenta(null, null);
                        }
                        padreViewModel.venta.Proforma = miVM.proforma;
                       
                    }
                    this.Close();
                }
                catch {
                    try
                    {
                        MV_AdministrarProformasWindow padre = this.Owner as MV_AdministrarProformasWindow;
                        MV_AdministrarProformasViewModel padreViewModel = padre.DataContext as MV_AdministrarProformasViewModel;

                        MV_AdministrarProformasViewModel miVM = this.DataContext as MV_AdministrarProformasViewModel;

                        if (miVM.proforma != null)
                        {
                            padreViewModel.proforma.ProformaProducto.Clear();
                            foreach (var vp in miVM.proforma.ProformaProducto)
                            {
                                ProformaProducto proformaProducto = new ProformaProducto();

                                proformaProducto.tipoCambio = (decimal)(MS_SharedService.obtenerTipodeCambio());
                                proformaProducto.montoParcial = vp.montoParcial;
                                
                                proformaProducto.Producto = vp.Producto;
                                proformaProducto.cantidad = vp.cantidad;
                                proformaProducto.PromocionActual = MV_PromocionService.ultimaPromocionPorProducto(vp.Producto, MS_SharedService.usuarioL.Empleado.tiendaActual);
                                proformaProducto.PropertyChanged += padreViewModel.actualizarMontosProforma;
                                padreViewModel.proforma.Cliente = vp.Proforma.Cliente;
                                padreViewModel.proforma.ProformaProducto.Add(proformaProducto);
                                padreViewModel.NotifyPropertyChanged("proforma");
                            }


                        }
                        this.Close();
                    }
                    catch { }
                }

                
            }
        }

        private void buscarProformaBtn_Click_1(object sender, RoutedEventArgs e)
        {
            MV_AdministrarProformasWindow profWindow = new MV_AdministrarProformasWindow();
            profWindow.Owner = this;
            MV_AdministrarProformasViewModel prodViewModel = profWindow.DataContext as MV_AdministrarProformasViewModel;
            prodViewModel.soloSeleccionarProforma = true;
            profWindow.Show();
        }

        private void buscarClienteBtn_Click_1(object sender, RoutedEventArgs e)
        {
            //Buscar Cliente
            MV_ClientesWindow v = new MV_ClientesWindow();
            v.Owner = this;
            v.clienteSearch = true;
            var viewModel = v.DataContext as MV_ClientesViewModel;
            viewModel.soloSeleccionarCliente = true;
            v.Show();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var vm = DataContext as MV_AdministrarProformasViewModel;
            if (!vm.soloSeleccionarProforma)
            {
                ComunService.Clean();
            }
        }

        private void buscarVendedorBtn_Click_1(object sender, RoutedEventArgs e)
        {
            MR_AdministrarPersonalWindow v = new MR_AdministrarPersonalWindow();
            v.Owner = this;
            var viewModel = v.main.DataContext as MR_AdministrarPersonalViewModel;
            viewModel.soloSeleccionarVendedor = true;
            v.Show();
        }


        //Buscador de productos
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as MV_AdministrarProformasViewModel;
            var buscador = new MV_BuscadorProductos(this, vm.usuarioLogueado.Empleado.tiendaActual);

        }

        private void imprimirBtn_Click(object sender, RoutedEventArgs e)
        {
            var print = new MV_DocProforma();
            //print.Visibility = System.Windows.Visibility.Hidden;
            print.Show();
            
            print.imprimir();
        }

        private void enviarEmailBtn_Click(object sender, RoutedEventArgs e)
        {
            var print = new MV_DocProforma();
            MemoryStream lMemoryStream = new MemoryStream();
            Package package = Package.Open(lMemoryStream, FileMode.Create);
            XpsDocument doc = new XpsDocument(package);
            XpsDocumentWriter writer = XpsDocument.CreateXpsDocumentWriter(doc);
            writer.Write(print);
            doc.Close();
            package.Close();

            var pdfXpsDoc = PdfSharp.Xps.XpsModel.XpsDocument.Open(lMemoryStream);
            PdfSharp.Xps.XpsConverter.Convert(pdfXpsDoc, "proforma.pdf", 0);
        }

    }
}
